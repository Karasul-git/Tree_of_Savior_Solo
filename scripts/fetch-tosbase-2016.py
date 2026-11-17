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
import gzip, hashlib, json, os, sys, time, urllib.error, urllib.parse, urllib.request
from datetime import datetime
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
OUT = ROOT / 'data/tosbase-2016'
RAW = OUT / 'raw'
WINDOW = ('20160315', '20160630')   # janela de lançamento
TARGET = '20160415'                  # data preferida
DELAY = float(os.environ.get('FETCH_DELAY', '0.8'))
UA = 'Mozilla/5.0 (compatible; wiki-archive-research/1.0)'


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
    """Captura mais próxima de TARGET dentro da janela, para o caminho em tosbase.com."""
    # Busca exata: o Internet Archive normaliza www, porta 80 e http/https para a mesma chave.
    rows = cdx('www.tosbase.com' + path) or []
    rows = [r for r in rows if urllib.parse.urlparse(r['original']).path.rstrip('/') == path.rstrip('/')]
    return min(rows, key=lambda r: distance(r['timestamp'])) if rows else None


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
    if not inv_path.exists():
        print('Inventário do domínio…', flush=True)
        rows = cdx('tosbase.com', matchType='domain', collapse='urlkey', limit='400000')
        if rows is None:
            print('Inventário indisponível (seguindo sem ele).', flush=True)
        else:
            inv_path.write_bytes(gzip.compress(json.dumps(rows, ensure_ascii=False).encode(), mtime=0))
            print(f'  {len(rows)} páginas capturadas na janela.', flush=True)

    # 2) o que coletar: habilidades (382) e páginas de classe já citadas no acervo
    catalog = json.loads((ROOT / 'data/catalog/classes-2016.json').read_text())
    jobs = []
    known = {}
    for snap in json.loads((ROOT / 'data/research/swordsman-snapshots.json').read_text()):
        if snap.get('tosbase_skill_id'):
            known[str(snap['tosbase_skill_id'])] = snap
    for c in catalog['classes']:
        for s in c['skills']:
            jobs.append(('skills', str(s['tosbase_id']), f"/database/skills/{s['tosbase_id']}/", s['name']))
        if c.get('archive_url'):
            path = urllib.parse.urlparse(c['archive_url'].split('id_/', 1)[-1]).path
            jobs.append(('classes', c['id'], path, c['name']))

    done = failed = 0
    for kind, key, path, name in jobs:
        mid = f'{kind}/{key}'
        if mid in captures and captures[mid].get('file') and (ROOT / captures[mid]['file']).exists():
            continue
        cap = best_capture(path)
        time.sleep(DELAY)
        if not cap:
            captures[mid] = {'name': name, 'path': path, 'status': 'sem-captura-na-janela'}
            failed += 1
            print(f'- {mid} {name}: sem captura na janela', flush=True)
            continue
        original = cap['original']
        url = f"https://web.archive.org/web/{cap['timestamp']}id_/{original}"
        status, body = get(url)
        time.sleep(DELAY)
        if status != 200 or not body:
            captures[mid] = {'name': name, 'path': path, 'status': f'erro-{status}', 'timestamp': cap['timestamp']}
            failed += 1
            print(f'- {mid} {name}: erro {status}', flush=True)
            continue
        sha = hashlib.sha256(body).hexdigest()
        entry = {'name': name, 'path': path, 'status': 'ok', 'timestamp': cap['timestamp'], 'original': original,
                 'archive_url': url, 'sha256': sha, 'bytes': len(body),
                 'file': save(kind, key, cap['timestamp'], original, body)}
        ref = known.get(key) if kind == 'skills' else None
        if ref and ref['captured_at'].replace('-', '').replace(':', '').replace('T', '').replace('Z', '') == cap['timestamp']:
            entry['confere_com_acervo'] = (ref['html_sha256'] == sha)
        captures[mid] = entry
        done += 1
        if done % 20 == 0:
            manifest_path.write_text(json.dumps(manifest, ensure_ascii=False, indent=1))
            print(f'  {done} coletadas…', flush=True)

    manifest['updated_at'] = datetime.utcnow().strftime('%Y-%m-%dT%H:%M:%SZ')
    manifest['window'] = WINDOW
    manifest_path.write_text(json.dumps(manifest, ensure_ascii=False, indent=1))
    ok = sum(1 for c in captures.values() if c['status'] == 'ok')
    checked = [c for c in captures.values() if 'confere_com_acervo' in c]
    summary = (f'Coletadas nesta rodada: {done}. Falhas: {failed}. Total ok: {ok} de {len(jobs)}. '
               f'Conferência de hash com o acervo: {sum(c["confere_com_acervo"] for c in checked)} de {len(checked)}.')
    print(summary, flush=True)
    if os.environ.get('GITHUB_STEP_SUMMARY'):
        Path(os.environ['GITHUB_STEP_SUMMARY']).write_text('## Coleta do ToSBase de 2016\n\n' + summary + '\n')
    return 0


if __name__ == '__main__':
    sys.exit(main())
