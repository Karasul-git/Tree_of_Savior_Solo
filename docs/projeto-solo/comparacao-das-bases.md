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

## O que a comparação mostra

- **O dano está equilibrado.** As quatro configurações derrubam um monstro comum sozinhas com 5 a 6 habilidades, dentro da meta de 4 a 8. Archer e Scout ficam praticamente iguais; o Warrior de duas mãos bate um pouco mais.
- **O Warrior de escudo e placa fica imune.** No Melia, a defesa é **subtraída** do ataque do monstro. Como a defesa dele passa do ataque dos monstros, ele recebe 1 de dano por golpe. É um problema do modelo de defesa, não do Warrior.
- **Archer e Scout recebem ~10% por golpe**, no limite de cima da meta, porque têm menos CON e menos HP.
- **A esquiva não funciona ainda.** Archer e Scout têm mais esquiva, mas, nos níveis médios, ela fica abaixo da precisão dos monstros do Melia. A precisão dos monstros ainda não foi ajustada.
- **O crítico pesa pouco.** O ataque crítico de Archer e Scout é maior, mas é somado ao dano, então muda pouco o resultado.

## Decisões necessárias

| # | Tema | Proposta |
| --- | --- | --- |
| 1 | **Modelo de defesa** | Trocar a subtração por uma **redução percentual com limite**: redução = defesa ÷ (defesa + ataque do golpe), com máximo de 75%. No nível 120, a placa com escudo reduz cerca de 55% do golpe, e o couro, cerca de 38%. O tanque continua recebendo dano, e a defesa nunca zera um golpe. Se aprovado, o ataque dos monstros na tabela de ajustes precisa ser recalculado para o novo modelo. |
| 2 | Precisão dos monstros | Incluir a precisão na tabela de ajustes, para que a esquiva de Archer e Scout evite cerca de 10% a 20% dos golpes. |
| 3 | Peso do crítico | Revisar depois de pesquisar o efeito da DEX na chance de crítico em 2016. |

**Próximo passo recomendado:** decidir o modelo de defesa (proposta 1). É o que mais muda o equilíbrio entre as bases.
