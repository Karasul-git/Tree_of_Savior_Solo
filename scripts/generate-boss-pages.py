"""Gera as páginas dos chefes a partir de data/bosses.json.

Uso: python3 scripts/generate-boss-pages.py
"""
import json
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
data = json.loads((ROOT / 'data/bosses.json').read_text())
story = json.loads((ROOT / 'data/proposals/story-bosses.json').read_text())
cats = {c['id']: c for c in story['visual_categories']}
order = ['humans', 'mutated_humans', 'mutated_beasts', 'tree', 'fire', 'constructs', 'hooded', 'supernatural', 'no_image']
status_label = {'pendente': 'Pendente', 'aprovado': 'Aprovado', 'rejeitado': 'Rejeitado'}
out = ROOT / 'docs/projeto-solo/chefes'
out.mkdir(parents=True, exist_ok=True)

def levels(b):
    lv = [l for l in b['levels'] if l > 1]
    return ', '.join(map(str, lv)) if lv else '—'

for b in data['bosses']:
    c = cats[b['category']]
    lines = [f"# {b['name']}", '',
             '[6.3 Catálogo de chefes](README.md) · [6.2 Chefes da história](../chefes-da-historia.md) · [Revisar na página inicial](../../../README.md#revisao-chefes)', '',
             f"![Retrato de {b['name']}](../../assets/bosses/{Path(b['portrait']).name})", '',
             '| Campo | Valor |', '| --- | --- |',
             f"| Nomes nos dados | {', '.join(b['names'])} |",
             f"| Nível | {levels(b)} |",
             '| Raça em 2016 | Demônio (Velnias) |',
             f"| Grupo proposto | {c['label']} |",
             f"| Revisão do proprietário | **{status_label[b['review_status']]}** |", '',
             '## Proposta para o projeto', '',
             (f"**General de Giltine.** {b['general_proposal']}" if b.get('general_proposal') else c['proposal']), '']
    if b.get('original_role'):
        lines += ['## Na história de 2016', '', b['original_role'] + ' [F140](../../fontes.md#f140)', '']
    if b['quotes_2016']:
        lines += ['## Textos de 2016', '']
        lines += [f"> {q}" for q in b['quotes_2016']]
        lines += ['', 'Trechos da tradução oficial de 15/04/2016. [F140](../../fontes.md#f140)', '']
    lines += ['## Imagem', '',
              'Retrato do jogo atual, coletado da base de fãs tos.guru. A identidade com 2016 não foi verificada, e o modelo 3D pode mostrar detalhes que o retrato não mostra. [F141](../../fontes.md#f141) Procedência em `data/boss-assets.json`.', '']
    (out / f"{b['slug']}.md").write_text('\n'.join(lines))

rows = []
for cid in order:
    group = [b for b in data['bosses'] if b['category'] == cid]
    if not group:
        continue
    rows += [f"## {cats[cid]['label']}", '', cats[cid]['proposal'], '', '| Retrato | Chefe | Nível | Revisão |', '| --- | --- | --- | --- |']
    for b in group:
        rows.append(f"| ![{b['name']}](../../assets/bosses/{Path(b['portrait']).name}) | [{b['name']}]({b['slug']}.md) | {levels(b)} | {status_label[b['review_status']]} |")
    rows.append('')
counts = {s: sum(1 for b in data['bosses'] if b['review_status'] == s) for s in status_label}
catalog = ['# Catálogo de chefes', '',
           '[Índice do projeto](../indice.md) · [6.2 Chefes da história](../chefes-da-historia.md) · [Revisar na página inicial](../../../README.md#revisao-chefes)', '',
           f"Os **{len(data['bosses'])} chefes** que eram demônios em 2016, agrupados pelo retrato e organizados pela proposta de papel na história sem sobrenatural. Cada chefe tem uma página com retrato, proposta e textos de 2016.", '',
           f"**Revisão:** {counts['aprovado']} aprovados, {counts['rejeitado']} rejeitados, {counts['pendente']} pendentes. Para aprovar ou rejeitar, use a [revisão dos chefes](../../../README.md#revisao-chefes) na página inicial do site.", '',
           'Retratos do jogo atual coletados da base de fãs tos.guru, sem alteração; identidade com 2016 não verificada. [F141](../../fontes.md#f141)', ''] + rows
(out / 'README.md').write_text('\n'.join(catalog))
print(f"{len(data['bosses'])} páginas de chefes geradas.")
