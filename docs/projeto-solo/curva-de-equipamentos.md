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

Para cumprir as metas (**1 a 2 habilidades de cada membro** do grupo de 4, meta revista pelo proprietário; cada golpe tirando 5% a 10% do HP), usei duas hipóteses:

- **A defesa do monstro absorve cerca de 25% do ataque do personagem.** Com as defesas atuais do Melia, ela absorvia quase tudo nos níveis médios.
- **O golpe do monstro tira 7,5% do HP**, o meio da meta.

O ponto decisivo é a **escala das habilidades**. No Melia, a mediana do fator das 154 habilidades de ataque da árvore solo é **~1.500%** no nível 1 e **~2.800%** no nível 5. Essa escala é do jogo atual, em que um golpe derruba um monstro comum.

| Nível | HP do monstro no Melia | Multiplicador de HP com a escala atual (~2.000%) | Multiplicador de HP com escala de 150% a 200% | Ataque do monstro → alvo | Defesa do monstro → alvo |
| --- | --- | --- | --- | --- | --- |
| 15 | 515 | 36.82× | 2.76× – 3.68× | 174 → 396 (2.28×) | 82 → 52 (0.64×) |
| 40 | 1.621 | 25.69× | 1.93× – 2.57× | 325 → 885 (2.72×) | 202 → 115 (0.57×) |
| 75 | 5.242 | 14.06× | 1.05× – 1.41× | 973 → 1.592 (1.64×) | 588 → 205 (0.35×) |
| 120 | 14.797 | 7.92× | 0.59× – 0.79× | 2.154 → 2.548 (1.18×) | 1.191 → 325 (0.27×) |
| 170 | 35.721 | 4.67× | 0.35× – 0.47× | 3.449 → 3.662 (1.06×) | 1.950 → 463 (0.24×) |
| 220 | 49.981 | 4.35× | 0.33× – 0.44× | 4.668 → 4.847 (1.04×) | 2.168 → 604 (0.28×) |
| 270 | 84.025 | 3.22× | 0.24× – 0.32× | 4.459 → 6.078 (1.36×) | 2.738 → 752 (0.27×) |

**Leitura:**

- **Com a escala atual**, os monstros precisariam de **3 a 37 vezes** mais HP, com um salto grande entre os níveis.
- **Com uma escala de 150% a 200%**, mais próxima da ideia de 2016 (em que a habilidade somava um valor fixo ao ataque), os monstros até o nível 40 precisam de 2 a 4 vezes mais HP; no nível 75 ficam perto do atual; dali em diante, precisam de **menos HP do que hoje** (um quarto a quatro quintos).
- **Sozinho**, o jogador derruba um monstro comum com 4 a 8 habilidades; com o grupo, cada um usa 1 ou 2.
- O **ataque dos monstros** precisa de 1 a 2,7 vezes o atual, e a **defesa**, de um quarto a dois terços.

## Decisão: reduzir a escala das habilidades (opção A)

**15/11/2026 — decisão do proprietário.** Os fatores das habilidades do Melia serão **divididos por 10**. A mediana das habilidades de ataque da árvore solo passa de ~1.506% para **~151%** no nível 1 e de ~2.819% para **~282%** no nível 5. A opção B (multiplicar o HP dos monstros por 3 a 37) foi descartada.

<a id="ajustes-monstros"></a>

## Tabela de ajustes dos monstros

Calculada com fator de referência de **175%** (meio da faixa de 150% a 200%) e com o **modelo de defesa percentual** decidido depois (redução = defesa ÷ (defesa + ataque), máximo de 75%). Defesa: um terço do ataque do personagem de referência (reduz ~25%). Ataque: o que faz o golpe tirar 7,5% do HP do personagem de referência. Precisão: a esquiva do Warrior de referência menos 12. Os multiplicadores se aplicam aos valores atuais de cada monstro, pelas funções `SCR_Get_MON_*` do Melia. Entre os níveis da tabela, o ajuste é interpolado em linha reta pelo nível do monstro; abaixo de 15 usa o valor de 15, e acima de 270, o de 270.

| Nível | HP | Ataque | Defesa | Precisão | HP alvo | Ataque alvo | Defesa alvo | Precisão alvo |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| 15 | 3.22× | 1.44× | 0.85× | 0.0× | 1.659 | 251 | 70 | 0 |
| 40 | 2.25× | 1.8× | 0.76× | 0.02× | 3.644 | 584 | 154 | 1 |
| 75 | 1.23× | 1.11× | 0.46× | 0.12× | 6.447 | 1.083 | 273 | 10 |
| 120 | 0.69× | 0.83× | 0.36× | 0.16× | 10.248 | 1.777 | 434 | 21 |
| 170 | 0.41× | 0.76× | 0.32× | 0.19× | 14.595 | 2.617 | 618 | 34 |
| 220 | 0.38× | 0.76× | 0.37× | 0.21× | 19.026 | 3.544 | 805 | 46 |
| 270 | 0.28× | 1.02× | 0.37× | 0.22× | 23.678 | 4.527 | 1.002 | 58 |

Os valores-alvo são a mediana esperada para um monstro comum daquele nível. Cada monstro mantém suas diferenças em relação aos outros, porque o ajuste é um multiplicador.

## Outras armas

Separando por tipo, grau e nível, todas as armas do Melia caem em **duas curvas**. [F124](../fontes.md#f124)

| Ataque médio (grau 01, salvo indicação) | 15 | 40 | 75 | 120 | 170 | 220 | 270 |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Uma mão (espada, lança, rapieira, maça, besta, adaga, pistola) | 141 | 327 | 586 | 920 | 1.292 | 1.663 | 2.034 |
| Duas mãos (espada grande, lança, maça, arco, mosquete, canhão) | 166 | 386 | 694 | 1.088 | 1.526 | 1.966 | 2.404 |
| Uma mão, grau 02 | 156 | 363 | 652 | 1.023 | 1.436 | 1.848 | 2.260 |
| Duas mãos, grau 02 | 185 | 429 | 770 | 1.209 | 1.696 | 2.184 | 2.672 |

- **Uma mão:** espada, lança, rapieira, maça, besta (`Bow`), adaga e pistola têm a mesma curva.
- **Duas mãos:** espada grande, lança de duas mãos, maça de duas mãos, arco (`THBow`), mosquete e canhão dão **cerca de 18% a mais**.
- Mosquete e canhão só aparecem a partir dos níveis 120 e 170; algumas armas não têm item comum em todos os níveis. Os vazios estão nos dados.

### Mão secundária: o ponto que desequilibra

No Melia, o ataque de **todos os itens equipados é somado**. O cálculo próprio da arma secundária (`MINPATK_SUB`) ainda está marcado como pendente no código. Por isso:

| Nível | Uma mão + escudo | Duas mãos | Uma mão + adaga ou pistola (como o Melia soma hoje) |
| --- | --- | --- | --- |
| 40 | 327 | 386 (1,18×) | 654 (2×) |
| 120 | 921 | 1.088 (1,18×) | 1.841 (2×) |
| 220 | 1.663 | 1.966 (1,18×) | 3.326 (2×) |

Ataque da arma, sem atributos. O Scout, cuja árvore usa espada de uma mão com adaga ou pistola, teria o dobro do ataque de arma do Warrior com escudo.

**Decidido em 15/11/2026 pelo proprietário:** a arma secundária soma só **20% do próprio ataque** ao ataque geral, e o valor inteiro vale nas habilidades que usam a arma secundária (cortes de adaga, tiros de pistola). Assim, espada + adaga fica em torno de 1,2×, perto de uma arma de duas mãos, e o escudo continua trocando ataque por defesa.

## Pendências

- Aplicar a divisão dos fatores das habilidades no servidor (`skills.txt` ou função de cálculo).
- Metas para elites, chefes e masmorras.
- Conferir os valores de armas e armaduras de 2016.
- Fechar a tabela de multiplicadores por faixa de nível e testar no servidor.

A [comparação das três bases](comparacao-das-bases.md) usa estas curvas.

**Próximo passo recomendado:** definir as metas de elites, chefes e masmorras.
