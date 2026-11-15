# Comparação das três bases

[Curva de equipamentos](curva-de-equipamentos.md) · [Fórmulas de dano](formulas-de-dano.md) · [Calibragem dos monstros](calibragem-monstros.md)

**Comparação preliminar de 15/11/2026.** Warrior, Archer e Scout no mesmo nível, cada um com o seu equipamento comum, contra o monstro-alvo da [tabela de ajustes](curva-de-equipamentos.md#ajustes-monstros). É uma conta à mão com as fórmulas do Melia e as decisões do projeto; nada foi testado no servidor. Os dados estão em [base-comparison.json](../../data/proposals/base-comparison.json). [F124](../fontes.md#f124)

## Regra da arma secundária (decidida)

**15/11/2026 — decisão do proprietário.** Adaga ou pistola na mão secundária somam **20% do próprio ataque** ao ataque geral; o valor inteiro vale nas habilidades que usam a arma secundária.

## Configurações comparadas

| Configuração | Distribuição de pontos | Equipamento |
| --- | --- | --- |
| Warrior defensivo | 60% STR, 40% CON | Espada + escudo, armadura de placa |
| Warrior ofensivo | 60% STR, 40% CON | Espada de duas mãos, couro |
| Archer | 50% STR, 20% CON, 30% DEX | Arco de duas mãos, couro |
| Scout | 50% STR, 20% CON, 30% DEX | Espada + adaga (20%), couro |

## Resultado

| Configuração | Nível | Ataque | Defesa | HP | Esquiva | Ataque crítico | Habilidades para derrubar sozinho | Dano por golpe recebido (% do HP) |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Warrior (espada + escudo, placa) | 40 | 462 | 1.145 | 3.861 | 13 | 24 | 6.0 | ~0% |
| Warrior (espada + escudo, placa) | 120 | 1.301 | 3.147 | 12.408 | 33 | 24 | 6.0 | ~0% |
| Warrior (espada + escudo, placa) | 220 | 2.416 | 5.651 | 25.998 | 58 | 24 | 6.0 | ~0% |
| Warrior (duas mãos, couro) | 40 | 521 | 595 | 3.861 | 13 | 24 | 5.1 | 7,5% |
| Warrior (duas mãos, couro) | 120 | 1.469 | 1.617 | 12.408 | 33 | 24 | 5.1 | 7,5% |
| Warrior (duas mãos, couro) | 220 | 2.718 | 2.897 | 25.998 | 58 | 24 | 5.1 | 7,5% |
| Archer (arco, couro) | 40 | 513 | 570 | 3.321 | 24 | 107 | 5.2 | 9,5% |
| Archer (arco, couro) | 120 | 1.429 | 1.554 | 10.048 | 64 | 253 | 5.3 | 9,9% |
| Archer (arco, couro) | 220 | 2.642 | 2.782 | 19.634 | 113 | 427 | 5.3 | 10,5% |
| Scout (espada + adaga 20%, couro) | 40 | 513 | 572 | 3.331 | 24 | 111 | 5.2 | 9,4% |
| Scout (espada + adaga 20%, couro) | 120 | 1.442 | 1.556 | 10.074 | 65 | 257 | 5.2 | 9,9% |
| Scout (espada + adaga 20%, couro) | 220 | 2.658 | 2.784 | 19.683 | 114 | 431 | 5.3 | 10,5% |

## Modelo de defesa (decidido)

**15/11/2026 — decisão do proprietário.** A defesa deixa de ser subtraída do ataque. Agora ela **reduz uma fração do golpe**: redução = defesa ÷ (defesa + ataque do golpe), com **máximo de 75%**. A regra vale para jogadores e monstros. O ataque, a defesa e a precisão dos monstros foram recalculados para esse modelo (ver a [tabela de ajustes](curva-de-equipamentos.md#ajustes-monstros)).

## Resultado com o novo modelo

| Configuração | Nível | Defesa | Redução do golpe | Dano por golpe (% do HP) | Chance de desviar | Golpes para cair | Habilidades para derrubar sozinho |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Warrior (espada + escudo, placa) | 40 | 1.145 | 66% | 5,1% | 5,0% | 19,6 | 6,0 |
| Warrior (espada + escudo, placa) | 120 | 3.147 | 64% | 5,2% | 5,0% | 19,3 | 6,0 |
| Warrior (espada + escudo, placa) | 220 | 5.651 | 61% | 5,3% | 5,0% | 19,0 | 6,0 |
| Warrior (duas mãos, couro) | 40 | 595 | 50% | 7,5% | 5,0% | 13,3 | 5,2 |
| Warrior (duas mãos, couro) | 120 | 1.617 | 48% | 7,5% | 5,0% | 13,3 | 5,2 |
| Warrior (duas mãos, couro) | 220 | 2.897 | 45% | 7,5% | 5,0% | 13,3 | 5,2 |
| Archer (arco, couro) | 40 | 570 | 49% | 8,9% | 7,7% | 11,2 | 5,3 |
| Archer (arco, couro) | 120 | 1.554 | 47% | 9,4% | 11,5% | 10,6 | 5,3 |
| Archer (arco, couro) | 220 | 2.782 | 44% | 10,1% | 15,4% | 9,9 | 5,4 |
| Scout (espada + adaga 20%, couro) | 40 | 572 | 49% | 8,9% | 7,7% | 11,3 | 5,3 |
| Scout (espada + adaga 20%, couro) | 120 | 1.556 | 47% | 9,4% | 11,7% | 10,6 | 5,3 |
| Scout (espada + adaga 20%, couro) | 220 | 2.784 | 44% | 10,1% | 15,5% | 9,9 | 5,3 |

## O que a comparação mostra

- **O tanque funciona sem ficar imune.** O Warrior de escudo e placa reduz 61% a 66% de cada golpe, leva ~5% do HP e aguenta ~19 golpes.
- **O Warrior de duas mãos** leva 7,5% por golpe e aguenta ~13.
- **Archer e Scout** levam 9% a 10% por golpe, mas desviam de 8% a 15% dos golpes conforme o nível. Na média, ficam perto de 8% a 9% por golpe.
- **O dano continua equilibrado:** todas as configurações derrubam um monstro comum sozinhas com 5 a 6 habilidades.
- **Tudo dentro das metas:** monstro comum com 4 a 8 habilidades sozinho, e golpes entre 5% e 10% do HP.

O resultado anterior, com a defesa subtraída, continua guardado nos dados para comparação.

## Pendências

- Pesquisar o efeito da DEX na chance de crítico em 2016 e revisar o peso do crítico.
- Testar no servidor depois da fase 1.

As metas de chefes estão em [Elites, chefes e masmorras](chefes-elites-masmorras.md).

**Próximo passo recomendado:** montar a curva de experiência.
