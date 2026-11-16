# Cura e poções

[Resumo do balanceamento](resumo-do-balanceamento.md) · [Magia e INT](habilidades-magicas-e-int.md) · [Comparação das bases](comparacao-das-bases.md)

**Estudo de 16/11/2026.** Levantamento das poções originais nos dados do Melia e conta de sustentação com as metas do projeto. Os dados estão em [healing-potions.json](../../data/proposals/healing-potions.json). Nada foi testado no jogo. [F124](../fontes.md#f124)

## Decisões do proprietário

| Tema | Decisão |
| --- | --- |
| Sustentação | **Poções como base, no estilo clássico.** O dano dos monstros continua em 5% a 10% por golpe, e caçar sozinho consome poções com frequência. |
| Cura de preparados com INT | **Cura = valor base × (1 + INT ÷ 100)**, ou seja, +1% por ponto de INT. Um especialista cura ~74% a mais no nível 120 e ~136% a mais no 220. Vale para Sprinkle HP/SP Potion e Healing Factor. |

## Como as poções funcionam no Melia

- A poção de HP comum cura **valor do item × 7 a cada 2 segundos, por 15 segundos** (8 curas), e só pode ser usada de novo depois de **25 segundos**.
- A regeneração natural devolve **1% do HP a cada 20 segundos** (3% por minuto); sentado, o intervalo cai pela metade.
- As poções de HP, de SP e de recuperação (HP e SP juntos) têm recargas separadas.

| Poção de HP | Valor | Cura total | Preço no Melia |
| --- | --- | --- | --- |
| Small HP Potion | 39 | 2.184 | 80 |
| HP Potion | 66 | 3.696 | 160 |
| Large HP Potion | 132 | 7.392 | 360 |
| Large HP Potion (2) | 210 | 11.760 | 460 |
| Huge HP Potion | 315 | 17.640 | 720 |

Há também versões de missão ("Basic") que curam 20% do HP máximo por vez, e poções especiais de conteúdos posteriores, fora desta conta.

## Sustentação caçando sozinho

Hipótese: 4 monstros por minuto, luta de ~12 segundos, um golpe de monstro a cada ~3 segundos, personagem de referência Archer (9% a 10% por golpe, com esquiva).

| Nível | HP | Dano recebido por minuto | Regeneração natural | HP Potion | Large HP Potion | Huge HP Potion |
| --- | --- | --- | --- | --- | --- | --- |
| 40 | 3.321 | ~131% | 3% | 267% | 534% | 1275% |
| 120 | 10.048 | ~133% | 3% | 88% | 177% | 421% |
| 220 | 19.634 | ~137% | 3% | 45% | 90% | 216% |

As três últimas colunas mostram quanto cada poção devolve por minuto, em % do HP, se usada a cada recarga.

**Leitura:**

- Caçando sozinho sem parar, o personagem perde **~130% do HP por minuto**. A regeneração natural quase não ajuda.
- A **poção certa para o nível** cobre esse dano: HP Potion nos níveis baixos, Large HP Potion por volta do 120 e Huge HP Potion perto do 220. Isso significa usar **2 a 3 poções por minuto** sozinho.
- **Em grupo**, a maior parte dos golpes vai para o tanque, e as curas de preparados (Sprinkle HP Potion) ajudam a distribuir o consumo.

## Pendências

- **Economia:** com 2 a 3 poções por minuto sozinho, o preço das poções e o silver ganho por hora precisam fechar a conta.
- Definir qual poção é vendida em cada faixa de nível.
- Medir no jogo o intervalo real dos golpes dos monstros, que muda toda a conta.
- Avaliar as poções de SP com o custo de SP das habilidades.

**Próximo passo recomendado:** fechar a **economia das poções**: quanto silver o jogador ganha por hora e quanto gasta em poções, para que a caça sozinho se pague.
