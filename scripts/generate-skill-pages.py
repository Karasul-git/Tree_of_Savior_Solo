#!/usr/bin/env python3
"""Gera o catálogo de habilidades da wiki: dados e uma página por habilidade.

Fontes: catálogos de classes (2016 e posteriores), a posição solo de cada classe,
o kit do Scout, os valores do Melia (data/skills/melia-skills.json) e as tabelas
de 2016 do Swordsman. A pontuação do jogo é provisória e vem da regra de escala do
projeto (fator do Melia ÷ 10, mais 10% do valor do nível 1 por nível).
Uso: python3 scripts/generate-skill-pages.py
"""
import json, re, shutil, unicodedata
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
read = lambda f: json.loads((ROOT / f).read_text(encoding='utf-8'))

solo = read('data/catalog/classes-solo.json')
catalog = read('data/catalog/classes-2016.json')['classes'] + read('data/catalog/classes-later.json')['classes']
classes = {c['id']: c for c in catalog}
six = {c['class_id']: c for c in read('data/proposals/six-classes-solo.json')['classes']}
review = {c['class_id']: c for c in read('data/proposals/remaining-classes-review.json')['classes']}
kit = read('data/proposals/scout-base-kit.json')['proposed_kit']
melia_file = read('data/skills/melia-skills.json')
melia = melia_file['skills']
sword_2016 = {s['name']: s for s in read('data/classes/swordsman.json')['skills']}
scale = read('data/simulator.json')['constants']

KIT_MELIA = {'Dagger Slash': 'Scout_DaggerSlash', 'Oblique Fire': 'Scout_ObliqueFire', 'Cloaking': 'Scout_Cloaking',
             'Free Step': 'Scout_FreeStep', 'Double Attack': 'Scout_DoubleAttack', 'Camouflage': 'Scout_Camouflage',
             'Perspective Distortion': 'Scout_Undistance', 'Scan': 'Scout_Scan'}
KIT_SUMMARY = {
    'Dagger Slash': 'Cortes rápidos de adaga, com mais de um golpe.',
    'Oblique Fire': 'Disparo de pistola que ricocheteia.',
    'Cloaking': 'Esconde o personagem dos inimigos por um tempo.',
    'Free Step': 'Movimento ágil que facilita a fuga e o reposicionamento.',
    'Double Attack': 'Prepara um ataque duplo para os próximos golpes.',
    'Camouflage': 'Camuflagem que reduz a chance de ser notado.',
    'Perspective Distortion': 'Distorce a distância percebida pelos inimigos.',
    'Scan': 'Revela inimigos e armadilhas escondidos.',
}
WEAPONS = {'UseOneHandSwordSkill': 'Espada de uma mão', 'UseDaggerSkill': 'Adaga', 'UsePistolSkill': 'Pistola',
           'UseBowSkill': 'Arco', 'UseCrossBowSkill': 'Besta', 'UseSpearSkill': 'Lança', 'UseMaceSkill': 'Maça',
           'UseMusketSkill': 'Mosquete', 'UseCannonSkill': 'Canhão', 'UseShieldSkill': 'Escudo'}
BASE_NAMES = {'warrior': 'Warrior', 'archer': 'Archer', 'scout': 'Scout'}


def slugify(text):
    text = unicodedata.normalize('NFD', text).encode('ascii', 'ignore').decode()
    return re.sub(r'[^a-z0-9]+', '-', text.lower()).strip('-')


def fmt(value, digits=0):
    s = f'{value:,.{digits}f}'
    return s.replace(',', 'X').replace('.', ',').replace('X', '.')


def find_melia(class_id, skill):
    """Acha a habilidade no Melia: id do ToSBase, nome do kit ou nome único."""
    if skill.get('melia_class') and skill['melia_class'] in melia:
        return skill['melia_class']
    tid = skill.get('tosbase_id')
    if tid:
        for key, value in melia.items():
            if value['id'] == tid:
                return key
    candidates = [k for k, v in melia.items() if v['name'] == skill['name']]
    if len(candidates) == 1:
        return candidates[0]
    prefix = classes[class_id]['name'].replace(' ', '').replace('ö', 'o').lower()
    narrowed = [k for k in candidates if k.lower().startswith(prefix[:5])]
    return narrowed[0] if len(narrowed) == 1 else None


def kind_of(m):
    """dano: tem fator próprio; dano-pendente: ataca, mas o Melia só traz 100% provisório; apoio: sem ataque."""
    if not m:
        return None
    if m['attack_type'] in (None, 'None'):
        return 'apoio'
    if (m['factor_pct'] or 100) != 100 or (m['factor_by_level_pct'] or 0) != 0:
        return 'dano'
    return 'dano-pendente'


def status_of(class_id, skill):
    if skill.get('kit'):
        return 'no-jogo'
    if class_id in six:
        found = next((s for s in six[class_id]['skills'] if s['name'] == skill['name']), None)
        return 'no-jogo' if found and found['assessment'] == 'reuse' else 'em-revisao'
    found = next((s for s in review.get(class_id, {'skills': []})['skills'] if s['name'] == skill['name']), None)
    return 'no-jogo' if found and found['assessment'] == 'compatible' else 'em-revisao'


def circle_of(class_id, skill):
    if skill.get('kit'):
        return skill['circle']
    if skill.get('circle'):
        return skill['circle']
    if class_id in six:
        found = next((s for s in six[class_id]['skills'] if s['name'] == skill['name']), None)
        return found['circle_proposed'] if found else None
    return None


# ---------------------------------------------------------------- dados
STATUS_LABEL = {'no-jogo': 'No jogo', 'em-revisao': 'Em revisão'}
all_classes = []
for base in solo['bases']:
    entries = [{'class_id': base['source_class_id'], 'solo_rank': 1}] + base['classes']
    for position in entries:
        cid = position['class_id']
        entry = classes[cid]
        if cid == 'scout':
            raw = [{'name': k['name'], 'summary': KIT_SUMMARY[k['name']], 'circle': k['proposed_circle'], 'kit': True,
                    'melia_class': KIT_MELIA[k['name']]} for k in kit]
        else:
            raw = [dict(s) for s in entry['skills']]
        used = set()
        skills = []
        for s in raw:
            slug = slugify(s['name'])
            while slug in used:
                slug += '-2'
            used.add(slug)
            key = find_melia(cid, s)
            m = melia.get(key) if key else None
            skills.append({
                'slug': slug, 'name': s['name'], 'summary': s['summary'], 'circle': circle_of(cid, s),
                'status': status_of(cid, s), 'melia_class': key,
                'kind': kind_of(m),
            })
        all_classes.append({'class_id': cid, 'name': entry['name'], 'base': base['id'], 'rank': position['solo_rank'],
                            'summary': entry['summary'], 'skills': skills})

(ROOT / 'data/skills/skills.json').write_text(json.dumps({
    'note': 'Catálogo de habilidades do jogo (39 classes). Gerado por scripts/generate-skill-pages.py; não editar à mão.',
    'classes': all_classes}, ensure_ascii=False, indent=1) + '\n', encoding='utf-8')

# ---------------------------------------------------------------- páginas
out = ROOT / 'docs/habilidades'
if out.exists():
    shutil.rmtree(out)
out.mkdir(parents=True)
KIND_LABEL = {'dano': 'Dano', 'dano-pendente': 'Dano (valores pendentes)', 'apoio': 'Apoio', None: 'Sem dados ainda'}


def weapons_of(m):
    found = [WEAPONS[t] for t in m['tags'] if t in WEAPONS]
    return ', '.join(dict.fromkeys(found)) if found else None


def skill_page(c, s):
    m = melia.get(s['melia_class']) if s['melia_class'] else None
    circ = f"C{s['circle']}" if s['circle'] else 'A definir'
    lines = [f"# {s['name']}", '',
             f"[{c['name']}](../../classes/catalogo/{c['class_id']}.md) · [Habilidades de {c['name']}](README.md) · [Catálogo de habilidades](../README.md)", '',
             '| Campo | Valor |', '| --- | --- |',
             f"| Classe | [{c['name']}](../../classes/catalogo/{c['class_id']}.md) |",
             f"| Base | {BASE_NAMES[c['base']]} |",
             f"| Rank da classe | R{c['rank']} |",
             f"| Círculo | {circ} |",
             f"| Tipo | {KIND_LABEL[s['kind']]} |"]
    if m:
        w = weapons_of(m)
        if w:
            lines.append(f"| Arma | {w} |")
        if m['range']:
            lines.append(f"| Alcance | {fmt(m['range'])} |")
        if m['sp']:
            lines.append(f"| SP base | {fmt(m['sp'])} |")
        if m['cooldown_s'] is not None:
            lines.append(f"| Recarga | {fmt(m['cooldown_s'], 1) + ' s' if m['cooldown_s'] else 'Sem recarga'} |")
        if s['kind'] == 'dano' and m['hits'] > 1:
            lines.append(f"| Golpes por uso | {m['hits']} |")
    lines += [f"| Situação | **{STATUS_LABEL[s['status']]}** |", '| Pontuação | Provisória |', '',
              '## O que faz', '', s['summary'], '']
    if s['status'] == 'em-revisao':
        lines += ['Esta habilidade ainda está em revisão e pode mudar ou sair da classe antes de entrar no jogo.', '']
    if s['kind'] == 'dano' and m:
        f1 = m['factor_pct'] / 10
        lines += ['## Dano por nível', '',
                  'Pontuação provisória do nosso jogo: o fator do jogo atual dividido por 10, somando 10% do valor do nível 1 a cada nível. O fator é multiplicado pelo ataque do personagem.', '',
                  '| Nível | Fator no nosso jogo | Fator no jogo atual (Re:Build) |' if m['hits'] == 1 else '| Nível | Fator por golpe no nosso jogo | Fator total do uso | Fator por golpe no jogo atual (Re:Build) |',
                  '| --- | --- | --- |' if m['hits'] == 1 else '| --- | --- | --- | --- |']
        for n in range(1, scale['skill_max_level'] + 1):
            ours = f1 * (1 + scale['skill_factor_growth_per_level'] * (n - 1))
            now = m['factor_pct'] + (m['factor_by_level_pct'] or 0) * (n - 1)
            if m['hits'] == 1:
                lines.append(f"| {n} | {fmt(ours)}% | {fmt(now)}% |")
            else:
                lines.append(f"| {n} | {fmt(ours)}% | {fmt(ours * m['hits'])}% | {fmt(now)}% |")
        lines.append('')
    elif m and s['kind'] == 'dano-pendente':
        lines += ['## Valores', '', 'Esta habilidade causa dano, mas o fator por nível ainda não foi reunido.', '']
    elif m:
        lines += ['## Valores', '', 'Esta habilidade não causa dano direto. Os efeitos por nível ainda não foram reunidos.', '']
    elif s['status']:
        lines += ['## Valores', '', 'Os valores desta habilidade ainda não foram reunidos.', '']
    old = sword_2016.get(s['name']) if c['class_id'] == 'swordsman' else None
    if old:
        keys = sorted({k for lv in old['archived_levels'] for k in lv['effects']})
        label = {'ataque_exibido': 'Ataque exibido', 'bonus_ataque_fisico': 'Bônus de ataque físico', 'modificador_defesa_fisica': 'Defesa',
                 'duracao_segundos': 'Duração (s)', 'dano_adicional': 'Dano adicional', 'limite_acertos': 'Limite de acertos',
                 'chance_atordoamento_percentual': 'Atordoar (%)', 'modificador_hp_maximo': 'HP máximo', 'dano_atributo_percentual': None,
                 'razao_ataque_aoe': None, 'defesa_ignorada': 'Defesa ignorada (%)'}
        shown = [k for k in keys if label.get(k)]
        lines += ['## No lançamento de 2016', '',
                  'Valores exibidos pelo ToSBase em capturas de março e abril de 2016. O "ataque exibido" é um número fixo por nível, não um percentual.', '',
                  '| Nível | ' + ' | '.join(label[k] for k in shown) + ' | SP | Recarga (s) |',
                  '| --- | ' + ' | '.join('---' for _ in shown) + ' | --- | --- |']
        for lv in old['archived_levels']:
            lines.append(f"| {lv['level']} | " + ' | '.join(str(lv['effects'].get(k, '–')) for k in shown) + f" | {lv['sp_cost']} | {lv['cooldown_seconds']} |")
        lines.append('')
    lines += ['## Fontes', '', 'Resumo: catálogo de classes. Valores do jogo atual: servidor Melia ([F124](../../fontes.md#f124)).' + (' Tabelas de 2016: ToSBase.' if old else ''), '']
    return '\n'.join(lines)


def class_page(c):
    lines = [f"# Habilidades de {c['name']}", '',
             f"[Catálogo de habilidades](../README.md) · [{c['name']}](../../classes/catalogo/{c['class_id']}.md)", '',
             f"{c['name']} · {BASE_NAMES[c['base']]} · R{c['rank']}. {c['summary']}", '',
             '| Habilidade | Círculo | Tipo | Situação |', '| --- | --- | --- | --- |']
    for s in sorted(c['skills'], key=lambda s: (s['circle'] or 9, s['name'])):
        circ = f"C{s['circle']}" if s['circle'] else 'A definir'
        lines.append(f"| [{s['name']}]({s['slug']}.md) | {circ} | {KIND_LABEL[s['kind']]} | {STATUS_LABEL[s['status']]} |")
    lines += ['']
    return '\n'.join(lines)


total = sum(len(c['skills']) for c in all_classes)
index = ['# Habilidades', '',
         f"As habilidades das **{len(all_classes)} classes** do jogo, nas três bases (Warrior, Archer e Scout): **{total} habilidades**, cada uma com a sua página. Escolha uma classe para ver as habilidades dela.", '',
         '**Situação das habilidades:** *No jogo* são as que entram na classe; *Em revisão* ainda podem mudar ou sair. **A pontuação (dano, custo e recarga) é provisória** e vai mudar na calibragem.', '']
for base in ('warrior', 'archer', 'scout'):
    index += [f"## {BASE_NAMES[base]}", '', '| Classe | Rank | Habilidades |', '| --- | --- | --- |']
    for c in sorted([c for c in all_classes if c['base'] == base], key=lambda c: (c['rank'], c['name'])):
        names = ', '.join(s['name'] for s in sorted(c['skills'], key=lambda s: (s['circle'] or 9, s['name']))[:4])
        more = f" e mais {len(c['skills']) - 4}" if len(c['skills']) > 4 else ''
        index.append(f"| [{c['name']}]({c['class_id']}/README.md) | R{c['rank']} | {len(c['skills'])}: {names}{more} |")
    index.append('')
index += ['## Todas as habilidades, em ordem alfabética', '', '| Habilidade | Classe | Círculo | Tipo | Situação |', '| --- | --- | --- | --- | --- |']
for c, s in sorted(((c, s) for c in all_classes for s in c['skills']), key=lambda cs: (cs[1]['name'].lower(), cs[0]['name'])):
    circ = f"C{s['circle']}" if s['circle'] else 'A definir'
    index.append(f"| [{s['name']}]({c['class_id']}/{s['slug']}.md) | {c['name']} | {circ} | {KIND_LABEL[s['kind']]} | {STATUS_LABEL[s['status']]} |")
index.append('')
(out / 'README.md').write_text('\n'.join(index), encoding='utf-8')
for c in all_classes:
    d = out / c['class_id']
    d.mkdir()
    (d / 'README.md').write_text(class_page(c), encoding='utf-8')
    for s in c['skills']:
        (d / f"{s['slug']}.md").write_text(skill_page(c, s), encoding='utf-8')
print(f"{len(all_classes)} classes, {total} habilidades.")
