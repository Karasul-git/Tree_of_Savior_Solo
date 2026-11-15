# Curva de equipamentos

[Calibragem dos monstros](calibragem-monstros.md) · [Fórmulas de dano](formulas-de-dano.md) · [Pontos de atributo e de habilidade](pontos-por-nivel.md) · [Fontes](../fontes.md)

**Estudo de 15/11/2026.** Esta página define quanto ataque e defesa o equipamento comum dá em cada nível e usa essa curva para calcular os multiplicadores dos monstros. Os dados estão em [equipment-curve.json](../../data/proposals/equipment-curve.json).

## De onde vem a curva

Separando os itens do Melia pelo **grau** (o número no código do item: `SWD01` é comum, `SWD02` é o grau seguinte, e assim por diante), as armas e armaduras formam escadas limpas por nível de item: 1, 15, 40, 75, 120, 170, 220, 270 e 315. A tentativa anterior falhou porque misturava graus, itens de evento e conjuntos especiais. [F124](../fontes.md#f124)

**Limites:** os valores são do jogo atual. Os níveis de item batem com 2016 (a espada Artie Saif, de nível 120, aparece numa lista de abril de 2016 [F31](../fontes.md#f31)), mas não foi conferido se o ataque e a defesa eram os mesmos naquele ano.

### Armas (espada de uma mão, ataque médio)

| Nível do item | 1 | 15 | 40 | 75 | 120 | 170 | 220 | 270 |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Comum (grau 01) | 37 | 141 | 327 | 587 | 921 | 1.292 | 1.663 | ~2.034* |
| Grau 02 | 41 | 157 | 363 | 652 | 1.023 | 1.436 | 1.848 | 2.261 |

\* Não há espada comum de nível 270; o valor foi estimado pela proporção entre os graus 01 e 02.

### Armaduras (defesa, grau 01)

| Peça e material | 15 | 40 | 75 | 120 | 170 | 220 | 270 |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Peito ou calça, tecido | 43 | 97 | 172 | 270 | 378 | 486 | 594 |
| Peito ou calça, couro | 64 | 145 | 259 | 405 | 567 | 729 | 891 |
| Peito ou calça, placa | 86 | 194 | 345 | 540 | 756 | 972 | 1.188 |
| Luvas ou botas, couro | 43 | 97 | 172 | 270 | 378 | 486 | 594 |
| Escudo | 172 | 388 | 691 | 1.080 | 1.512 | 1.944 | 2.376 |

Placa dá o dobro da defesa do tecido, e couro fica no meio. O grau 02 dá cerca de 11% a mais.

## Personagem de referência

Swordsman com 60% dos pontos em STR e 40% em CON (com o bônus por investimento), espada comum e armadura de couro comum nas quatro partes, sem escudo. A defesa já inclui a decisão de **CON somar +2 de defesa por ponto**.

| Nível | Arma | Armadura | Ataque | Defesa | HP |
| --- | --- | --- | --- | --- | --- |
| 15 | 141 | 214 | 210 | 276 | 1.597 |
| 40 | 327 | 484 | 462 | 595 | 3.861 |
| 75 | 586 | 862 | 819 | 1.040 | 7.362 |
| 120 | 920 | 1.350 | 1.301 | 1.617 | 12.408 |
| 170 | 1.292 | 1.890 | 1.853 | 2.257 | 18.734 |
| 220 | 1.663 | 2.430 | 2.416 | 2.897 | 25.998 |
| 270 | 2.034 | 2.970 | 3.007 | 3.537 | 33.884 |

## O que a calibragem mostrou

Para cumprir as metas (3 a 4 habilidades de cada membro do grupo de 4; cada golpe tirando 5% a 10% do HP), usei duas hipóteses:

- **A defesa do monstro absorve cerca de 25% do ataque do personagem.** Com as defesas atuais do Melia, ela absorvia quase tudo nos níveis médios.
- **O golpe do monstro tira 7,5% do HP**, o meio da meta.

O ponto decisivo é a **escala das habilidades**. No Melia, a mediana do fator das 154 habilidades de ataque da árvore solo é **~1.500%** no nível 1 e **~2.800%** no nível 5. Essa escala é do jogo atual, em que um golpe derruba um monstro comum.

| Nível | HP do monstro no Melia | Multiplicador de HP com a escala atual (~2.000%) | Multiplicador de HP com escala de 150% a 200% | Ataque do monstro → alvo | Defesa do monstro → alvo |
| --- | --- | --- | --- | --- | --- |
| 15 | 515 | 85.45× | 6.44× – 8.59× | 174 → 396 (2.28×) | 82 → 52 (0.64×) |
| 40 | 1.621 | 59.83× | 4.5× – 5.99× | 325 → 885 (2.72×) | 202 → 115 (0.57×) |
| 75 | 5.242 | 32.82× | 2.46× – 3.28× | 973 → 1.592 (1.64×) | 588 → 205 (0.35×) |
| 120 | 14.797 | 18.47× | 1.39× – 1.85× | 2.154 → 2.548 (1.18×) | 1.191 → 325 (0.27×) |
| 170 | 35.721 | 10.9× | 0.82× – 1.09× | 3.449 → 3.662 (1.06×) | 1.950 → 463 (0.24×) |
| 220 | 49.981 | 10.15× | 0.76× – 1.02× | 4.668 → 4.847 (1.04×) | 2.168 → 604 (0.28×) |
| 270 | 84.025 | 7.52× | 0.56× – 0.75× | 4.459 → 6.078 (1.36×) | 2.738 → 752 (0.27×) |

**Leitura:**

- **Com a escala atual**, os monstros precisariam de **7 a 85 vezes** mais HP, com um salto enorme entre os níveis. É difícil de equilibrar e de testar.
- **Com uma escala de 150% a 200%**, mais próxima da ideia de 2016 (em que a habilidade somava um valor fixo ao ataque), o HP dos monstros fica **perto do atual entre os níveis 120 e 220** e um pouco abaixo no 270. Os níveis até 75 precisam de 2,5 a 9 vezes mais HP, porque hoje os monstros iniciais foram feitos para cair num golpe.
- O **ataque dos monstros** precisa de 1 a 2,7 vezes o atual, e a **defesa**, de um quarto a dois terços.

## Decisão pendente

| Opção | O que muda | Efeito |
| --- | --- | --- |
| **A · Reduzir a escala das habilidades (recomendado)** | Dividir o fator das habilidades do Melia por cerca de 10, para a faixa de 150% a 200%. | Monstros com multiplicadores pequenos e estáveis; mais perto do ritmo de 2016. |
| B · Manter a escala atual | Multiplicar o HP dos monstros por 7 a 85, conforme o nível. | Muda menos as habilidades, mas os monstros ficam com números enormes e desiguais. |

## Pendências

- Decidir a escala das habilidades.
- Conferir os valores de armas e armaduras de 2016.
- Definir as curvas das outras armas (arco, besta, pistola, adaga, lança).
- Fechar a tabela de multiplicadores por faixa de nível e testar no servidor.

**Próximo passo recomendado:** decidir a escala das habilidades. A recomendação é a opção A.
