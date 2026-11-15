# Fórmulas de dano

[Pontos de atributo e de habilidade](pontos-por-nivel.md) · [Habilidades mágicas e o novo INT](habilidades-magicas-e-int.md) · [Estrutura do servidor](servidor-estrutura.md) · [Fontes](../fontes.md)

**Estudo de 15/11/2026.** Esta página registra as fórmulas que o Melia usa hoje e aponta o que precisa mudar para o projeto. As fórmulas estão em `system/scripts/zone/core/calc_combat.cs` e `calc_character.cs`. [F124](../fontes.md#f124)

**Limites:** o Melia reconstrói o jogo **posterior ao Re:Build**, e parte das fórmulas é preliminar, baseada em testes de jogadores, como o próprio código avisa. Não são as fórmulas de 2016. Nada foi executado; os números abaixo são cálculos feitos à mão com essas fórmulas.

## Como um golpe vira dano

Esta é a ordem real do cálculo no Melia:

1. **Esquiva.** Chance de errar = (Esquiva do alvo − Precisão do atacante)<sup>0,65</sup>, limitada a 80%. Habilidades mágicas nunca erram.
2. **Ataque base.** Um valor sorteado entre o ataque mínimo e o máximo. Habilidades físicas usam o ataque físico (STR); as mágicas usam o ataque mágico (INT).
3. **Bônus fixos** da habilidade e de efeitos.
4. **Defesa.** Subtrai a defesa física ou mágica do alvo. O dano nunca fica abaixo de 1.
5. **Fator da habilidade.** Multiplica pelo percentual da habilidade (por exemplo, 250%).
6. **Bloqueio.** Chance = (Bloqueio − Quebra de bloqueio)<sup>0,7</sup>. No modo atual, um bloqueio corta 50% do dano em vez de anular.
7. **Crítico.** Chance = (Taxa de crítico − Resistência a crítico)<sup>0,6</sup>. Se acertar, soma o ataque crítico e, no fim, multiplica por 1,5.
8. **Multiplicadores:** tamanho do alvo e arma; elemento, tipo de ataque e raça (estes só para habilidades mágicas).
9. **Limite máximo de dano** do servidor.

Habilidades mágicas não sofrem esquiva, bloqueio nem crítico, e usam a defesa mágica.

## O que cada atributo faz hoje no Melia

| Atributo | Propriedade | Fórmula atual |
| --- | --- | --- |
| STR | Ataque físico | 20 + nível + 2 × STR + ⌊STR ÷ 10⌋ × (nível × 0,05) + itens |
| STR | Precisão | nível ÷ 4 + STR ÷ 2 + ⌊STR ÷ 15⌋ × 3 + itens |
| CON | HP máximo | (400 + (nível − 1) × 80) × taxa da classe × (1 + 0,003 × CON + 0,01 × ⌊CON ÷ 10⌋) |
| CON | Defesa | **Não afeta.** Defesa = 20 + nível + itens. |
| INT | Ataque mágico | Mesma fórmula do ataque físico, com INT no lugar de STR. |
| SPR (Sabedoria) | SP máximo | (200 + (nível − 1) × 18) × taxa da classe × (1 + 0,005 × SPR + 0,015 × ⌊SPR ÷ 10⌋) |
| SPR (Sabedoria) | Recuperação de SP | 3% do SP máximo por ciclo, ou seja, sobe junto com o SP. |
| SPR (Sabedoria) | Poder de cura | 40% × (20 + 2,5 × nível + SPR) + 60% × (maior ataque médio ÷ 4) |
| DEX | Ataque crítico | 4 × DEX + ⌊DEX ÷ 10⌋ × 10 |
| DEX | Esquiva | nível ÷ 4 + DEX ÷ 2 + ⌊DEX ÷ 15⌋ × 3 + itens |
| DEX | Taxa de crítico | **Não afeta.** Taxa = nível ÷ 2 + itens. |
| — | Recuperação de HP | 1% do HP máximo por ciclo. |

Os termos com ⌊ ÷ 10⌋ e ⌊ ÷ 15⌋ dão um extra a cada 10 ou 15 pontos. Eles se somam ao [bônus por investimento](pontos-por-nivel.md) decidido para a distribuição de pontos.

### Exemplo

Personagem de nível 100 com 150 de STR:

- Ataque físico = 20 + 100 + 300 + 15 × 5 = **495**, antes dos itens.
- Precisão = 25 + 75 + 30 = **130**.

Contra um alvo com 150 de esquiva, a chance de errar fica em (150 − 130)<sup>0,65</sup> ≈ **7%**.

## Propostas para o projeto

| # | Tema | Proposta | Situação |
| --- | --- | --- | --- |
| 1 | INT de preparados | Manter o caminho mágico do Melia como o **caminho dos preparados**: o ataque vem do INT, ignora esquiva, bloqueio e crítico e usa a defesa mágica. As 26 habilidades levantadas já usam esse caminho. | Proposta |
| 2 | Cura de preparados | A cura de poções e medicina (Sprinkle HP/SP Potion, Healing Factor) **escala com INT**. A Sabedoria continua cuidando do SP e da recuperação natural. | **Decidido em 15/11/2026.** Fórmula a definir. |
| 3 | CON e defesa | **CON soma defesa física e mágica**, como diz o guia oficial atual. Valor inicial: +2 de cada defesa por ponto de CON. | **Decidido em 15/11/2026.** Valor sujeito a balanceamento. |
| 4 | DEX e crítico | Hoje a DEX só aumenta o dano do crítico, não a chance. Pesquisar como era em 2016 antes de mudar. | Pendente de fonte |
| 5 | Elementos | Os multiplicadores de elemento só valem para habilidades mágicas. Fogo e veneno de preparados continuam usando essa tabela. | Proposta |
| 6 | Grupo de 4 | O lado dos monstros (HP, ataque, defesa e esquiva) precisa ser calibrado para 1 jogador + 3 bots. | Próxima etapa |

Descrição dos atributos no guia oficial atual: [F113](../fontes.md#f113).

## Pendências

- Escrever a fórmula da cura de preparados com INT.
- Pesquisar o efeito da DEX na chance de crítico em 2016.
- Ler as fórmulas de monstros do Melia (`calc_monster.cs`) e propor a calibragem para grupos de 4.
- Conferir no servidor se as fórmulas preliminares (esquiva, crítico, bloqueio) se comportam bem nos níveis do projeto, até 280.

As decisões estão em [damage-formulas.json](../../data/proposals/damage-formulas.json).

A calibragem está em [Calibragem dos monstros](calibragem-monstros.md).

**Próximo passo recomendado:** definir a curva de equipamentos, que fecha a calibragem dos monstros.
