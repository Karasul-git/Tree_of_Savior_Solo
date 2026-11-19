#!/usr/bin/env python3
"""Coleta as capturas do ToSBase de 2016 guardadas no Internet Archive.

Roda no GitHub Actions (o ambiente de desenvolvimento não alcança web.archive.org).
Grava em data/tosbase-2016/:
  inventario.json.gz  lista das páginas do ToSBase capturadas na janela de 2016
  raw/...html.gz      HTML original (id_) de cada página de habilidade e de classe
  manifesto.json      endereço, data, hash e situação de cada captura
O HTML bruto fica só no branch de dados (não vai para main nem para o site).
Só biblioteca padrão. Pode ser interrompido e rodado de novo: pula o que já foi coletado.
"""
import gzip, hashlib, json, os, re, sys, time, urllib.error, urllib.parse, urllib.request
from concurrent.futures import ThreadPoolExecutor
from datetime import datetime
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
OUT = ROOT / 'data/tosbase-2016'
RAW = OUT / 'raw'
WINDOW = ('20160315', '20160630')   # janela de lançamento
TARGET = '20160415'                  # data preferida
DELAY = float(os.environ.get('FETCH_DELAY', '0.8'))
UA = 'Mozilla/5.0 (compatible; wiki-archive-research/1.0)'
WORKERS = int(os.environ.get('WORKERS', '5'))
SCOPE = [s.strip() for s in os.environ.get('SCOPE', 'skills,classes').split(',') if s.strip()]
MAX_MINUTES = float(os.environ.get('MAX_MINUTES', '300'))  # para de começar páginas novas depois disso


def get(url, timeout=90, tries=4):
    last = None
    for attempt in range(tries):
        try:
            req = urllib.request.Request(url, headers={'User-Agent': UA})
            with urllib.request.urlopen(req, timeout=timeout) as resp:
                return resp.status, resp.read()
        except urllib.error.HTTPError as e:
            if e.code in (404, 403):
                return e.code, b''
            last = f'HTTP {e.code}'
        except Exception as e:  # rede instável
            last = repr(e)
        time.sleep(2 ** attempt * 2)
    return 0, (last or 'erro').encode()


def cdx(url, **extra):
    params = {'url': url, 'output': 'json', 'filter': 'statuscode:200', 'from': WINDOW[0], 'to': WINDOW[1],
              'fl': 'timestamp,original,statuscode,digest'}
    params.update(extra)
    status, body = get('https://web.archive.org/cdx/search/cdx?' + urllib.parse.urlencode(params), timeout=300)
    if status != 200:
        return None
    try:
        rows = json.loads(body.decode('utf-8') or '[]')
    except ValueError:
        return None
    return [dict(zip(rows[0], r)) for r in rows[1:]] if rows else []


def distance(ts):
    a = datetime.strptime(ts[:8], '%Y%m%d')
    b = datetime.strptime(TARGET, '%Y%m%d')
    return abs((a - b).days)


def best_capture(path):
    """Captura mais próxima de TARGET dentro da janela; se não houver, a mais próxima num período maior."""
    # Busca exata: o Internet Archive normaliza www, porta 80 e http/https para a mesma chave.
    def pick(rows):
        rows = [r for r in rows or [] if urllib.parse.urlparse(r['original']).path.rstrip('/') == path.rstrip('/')]
        return min(rows, key=lambda r: distance(r['timestamp'])) if rows else None
    cap = pick(cdx('www.tosbase.com' + path))
    if cap:
        return cap
    cap = pick(cdx('www.tosbase.com' + path, **{'from': '20151001', 'to': '20170630'}))
    if cap:
        cap = dict(cap, fora_da_janela=True)
    return cap


def save(kind, key, ts, original, html):
    folder = RAW / kind
    folder.mkdir(parents=True, exist_ok=True)
    target = folder / f'{key}.{ts}.html.gz'
    target.write_bytes(gzip.compress(html, mtime=0))
    return str(target.relative_to(ROOT))


def main():
    OUT.mkdir(parents=True, exist_ok=True)
    manifest_path = OUT / 'manifesto.json'
    manifest = json.loads(manifest_path.read_text()) if manifest_path.exists() else {'captures': {}}
    captures = manifest['captures']

    # 1) inventário das páginas do ToSBase capturadas em 2016
    inv_path = OUT / 'inventario.json.gz'
    inventory = []
    if not inv_path.exists():
        print('Inventário do domínio…', flush=True)
        rows = cdx('tosbase.com', matchType='domain', collapse='urlkey', limit='400000')
        if rows is None:
            print('Inventário indisponível (seguindo sem ele).', flush=True)
        else:
            inv_path.write_bytes(gzip.compress(json.dumps(rows, ensure_ascii=False).encode(), mtime=0))
            print(f'  {len(rows)} páginas capturadas na janela.', flush=True)
            inventory = rows
    if not inventory and inv_path.exists():
        inventory = json.loads(gzip.decompress(inv_path.read_bytes()).decode())

    # 2) o que coletar, conforme SCOPE (variável de ambiente, lista separada por vírgulas)
    catalog = json.loads((ROOT / 'data/catalog/classes-2016.json').read_text())
    jobs = []
    known = {}
    for snap in json.loads((ROOT / 'data/research/swordsman-snapshots.json').read_text()):
        if snap.get('tosbase_skill_id'):
            known[str(snap['tosbase_skill_id'])] = snap
    if 'skills' in SCOPE:
        for c in catalog['classes']:
            for s in c['skills']:
                jobs.append(('skills', str(s['tosbase_id']), f"/database/skills/{s['tosbase_id']}/", s['name'], None))
    if 'classes' in SCOPE:
        for c in catalog['classes']:
            if c.get('archive_url'):
                path = urllib.parse.urlparse(c['archive_url'].split('id_/', 1)[-1]).path
                jobs.append(('classes', c['id'], path, c['name'], None))
    # Páginas descobertas pelo inventário: usam a captura listada, sem nova consulta ao índice.
    DATABASE = {'monsters': 'database/monsters', 'items': 'database/items', 'quests': 'database/quests', 'attributes': 'database/attributes',
                'npcs': 'database/npcs', 'maps': 'database/maps', 'collections': 'database/collections', 'statuseffects': 'database/status-effects',
                'recipes': 'database/items/recipes', 'sets': 'database/items/sets', 'equipment': 'database/items/equipment',
                'worldmap': 'game/world-map'}
    for scope, prefix in DATABASE.items():
        if scope not in SCOPE:
            continue
        pattern = re.compile(r'^/%s/([^/?]+)/?$' % re.escape(prefix))
        seen = set()
        for r in inventory:
            path = urllib.parse.urlparse(r['original']).path
            m = pattern.match(path)
            if m and m.group(1) not in seen and m.group(1) not in ('recipes', 'sets', 'equipment', 'gems', 'boss-cards'):
                seen.add(m.group(1))
                jobs.append((scope, m.group(1), path, m.group(1), r))
        index = [r for r in inventory if urllib.parse.urlparse(r['original']).path.rstrip('/') == '/' + prefix]
        if index:
            jobs.append((scope, '_lista', f'/{prefix}/', 'lista', index[0]))
    # Páginas do jogo (tabelas de experiência, elementos, PvP) e artigos do wiki da comunidade.
    extra = {'gamepages': re.compile(r'^/game/(exp-tables(/[\w-]+)?|elements|pvp(/[\w-]+)*|classes[^/]*rank[^/]*)/?$'),
             'wiki': re.compile(r'^/wiki/[^:/?]+$')}
    for scope, pattern in extra.items():
        if scope not in SCOPE:
            continue
        seen = set()
        for r in inventory:
            path = urllib.parse.urlparse(r['original']).path
            if pattern.match(path) and path not in seen:
                seen.add(path)
                jobs.append((scope, urllib.parse.unquote(path.strip('/')).replace('/', '_')[:120], path, path, r))
    if 'formulas' in SCOPE:
        pattern = re.compile(r'^/(tools/(stat-calculator|class-builder|simulator|skill-simulator)/?|js/(tosbase|skillsim)[^/]*\.js)$')
        seen = set()
        for r in inventory:
            path = urllib.parse.urlparse(r['original']).path
            if pattern.match(path) and path not in seen:
                seen.add(path)
                jobs.append(('formulas', path.strip('/').replace('/', '_'), path, path, r))

    ref_hashes = {k: v for k, v in known.items()}

    def work(job):
        kind, key, path, name, given = job
        mid = f'{kind}/{key}'
        cap = given or best_capture(path)
        if not given:
            time.sleep(DELAY)
        if not cap:
            return mid, {'name': name, 'path': path, 'status': 'sem-captura-na-janela'}
        original = cap['original']
        url = f"https://web.archive.org/web/{cap['timestamp']}id_/{original}"
        status, body = get(url)
        time.sleep(DELAY)
        if status != 200 or not body:
            return mid, {'name': name, 'path': path, 'status': f'erro-{status}', 'timestamp': cap['timestamp']}
        sha = hashlib.sha256(body).hexdigest()
        entry = {'name': name, 'path': path, 'status': 'ok', 'timestamp': cap['timestamp'], 'original': original,
                 'archive_url': url, 'sha256': sha, 'bytes': len(body),
                 'file': save(kind, key, cap['timestamp'], original, body)}
        if cap.get('fora_da_janela'):
            entry['fora_da_janela'] = True
        ref = ref_hashes.get(key) if kind == 'skills' else None
        if ref and ref['captured_at'].replace('-', '').replace(':', '').replace('T', '').replace('Z', '') == cap['timestamp']:
            entry['confere_com_acervo'] = (ref['html_sha256'] == sha)
        return mid, entry

    pending = [j for j in jobs
               if not (f'{j[0]}/{j[1]}' in captures and captures[f'{j[0]}/{j[1]}'].get('file')
                       and (ROOT / captures[f'{j[0]}/{j[1]}']['file']).exists())]
    print(f'{len(pending)} páginas por coletar, {WORKERS} em paralelo.', flush=True)
    started = time.time()
    done = failed = 0
    stopped = False
    with ThreadPoolExecutor(max_workers=WORKERS) as pool:
        for i in range(0, len(pending), WORKERS * 4):
            if time.time() - started > MAX_MINUTES * 60:
                stopped = True
                print('Tempo máximo atingido: parando; rode de novo para continuar.', flush=True)
                break
            for mid, entry in pool.map(work, pending[i:i + WORKERS * 4]):
                captures[mid] = entry
                if entry['status'] == 'ok':
                    done += 1
                else:
                    failed += 1
                    print(f"- {mid} {entry['name']}: {entry['status']}", flush=True)
            manifest_path.write_text(json.dumps(manifest, ensure_ascii=False, indent=1))
            print(f'  {done} coletadas, {failed} falhas…', flush=True)

    manifest['updated_at'] = datetime.utcnow().strftime('%Y-%m-%dT%H:%M:%SZ')
    manifest['window'] = WINDOW
    manifest_path.write_text(json.dumps(manifest, ensure_ascii=False, indent=1))
    ok = sum(1 for c in captures.values() if c['status'] == 'ok')
    checked = [c for c in captures.values() if 'confere_com_acervo' in c]
    summary = (f'Coletadas nesta rodada: {done}. Falhas: {failed}. Total ok: {ok} de {len(jobs)}.' + (' Parou pelo tempo máximo; rode de novo.' if stopped else '') + ' '
               f'Conferência de hash com o acervo: {sum(c["confere_com_acervo"] for c in checked)} de {len(checked)}.')
    print(summary, flush=True)
    if os.environ.get('GITHUB_STEP_SUMMARY'):
        Path(os.environ['GITHUB_STEP_SUMMARY']).write_text('## Coleta do ToSBase de 2016\n\n' + summary + '\n')
    return 0


if __name__ == '__main__':
    sys.exit(main())
