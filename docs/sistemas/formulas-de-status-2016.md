# Fórmulas de status em 2016

[Início](../../README.md) · [Ranks e círculos](ranks-e-circulos.md) · [Fontes](../fontes.md)

**Fonte:** o código da calculadora de status do ToSBase, capturado em 15/03/2016 ([F142](../fontes.md#f142)). É a melhor fonte que temos das contas de status da época, mas é uma ferramenta de fãs, marcada como beta. Não é código do jogo, não traz a fórmula de dano nem os efeitos dos equipamentos, e pode ter sido ajustada depois da captura.

## Os atributos

O valor final de cada atributo soma o valor da classe base com os pontos gastos, mais um **bônus por investimento** calculado sobre essa soma. Para **STR** e **INT** há ainda um bônus de rank.

**Bônus por investimento** (sobre o valor total do atributo, arredondado para baixo):

| Valor do atributo | Bônus |
| --- | --- |
| Até 50 | valor ÷ 5 |
| De 51 a 150 | 10 + (valor − 50) ÷ 4 |
| De 151 a 300 | 35 + (valor − 150) ÷ 3 |
| De 301 a 500 | 85 + (valor − 300) ÷ 2 |
| Acima de 500 | 185 + (valor − 500) |

**Bônus de rank:** STR e INT ganham mais ⌊valor × (rank − 1) × 0,1⌋, ou seja, 10% do valor por rank acima do primeiro. CON, SPR e DEX não ganham.

**Pontos disponíveis:** nível − 1, mais os pontos de bônus (história, revelações e estátuas).

## As classes base

Valores iniciais de cada atributo e as taxas da classe:

| Base | STR | CON | SPR | INT | DEX | Taxa de HP | Taxa de SP |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Swordsman | 7 | 8 | 3 | 2 | 5 | 3,3 | 0,8 |
| Archer | 6 | 4 | 4 | 3 | 8 | 1,4 | 0,9 |
| Cleric | 5 | 6 | 7 | 4 | 3 | 1,5 | 1,2 |
| Wizard | 3 | 3 | 7 | 8 | 4 | 1,1 | 1,2 |

## As contas

Nas fórmulas, *N* é o nível do personagem e os atributos já incluem os bônus acima.

| Estatística | Fórmula |
| --- | --- |
| HP máximo | ⌊taxa de HP × ⌊(N − 1) × 17⌋ + ⌊CON × 85⌋⌋ |
| SP máximo | ⌊taxa de SP × ⌊(N − 1) × 6,7⌋ + ⌊SPR × 13⌋⌋, mais N × 1,675 no Cleric |
| Recuperação de HP | ⌊taxa de HP × N × 0,5 + CON⌋ |
| Recuperação de SP | ⌊taxa de SP × N × 0,5 + SPR⌋, mais N ÷ 4 no Cleric |
| Ataque físico (mínimo e máximo) | N + STR |
| Ataque mágico (mínimo e máximo) | N + INT |
| Razão de ataque em área | 4 no Swordsman, 0 no Archer, 3 nas outras |
| Precisão | ⌊DEX + N⌋, mais (N + 4) ÷ 4 no Archer |
| Penetração de bloqueio | ⌊N × 0,5 + SPR⌋ |
| Ataque crítico | STR |
| Taxa de crítico | DEX, mais N ÷ 5 no Archer |
| Defesa física | ⌊N ÷ 2⌋, mais N ÷ 4 no Swordsman |
| Defesa mágica | ⌊N × 0,5 + SPR ÷ 5⌋, mais N ÷ 4 no Wizard |
| Razão de defesa em área | 1 |
| Esquiva | ⌊DEX + N⌋, mais N ÷ 8 no Archer |
| Bloqueio | 0 |
| Resistência a crítico | CON |
| Estamina | 25 |
| Velocidade de movimento | 30 |
| Peso máximo | 5.000 + 5 × CON + 5 × STR |

A calculadora mostra os valores **sem equipamento**: ataque, defesa e outros bônus dos itens se somam a eles.

## Exemplo

Swordsman no nível 100 com 99 pontos, todos em CON, no primeiro rank:

- CON = 8 + 99 = 107. O bônus é 10 + (107 − 50) ÷ 4 = 24 (arredondado), então CON final = **131**.
- HP máximo = ⌊3,3 × ⌊99 × 17⌋ + 131 × 85⌋ = ⌊5.553,9 + 11.135⌋ = **16.688**.

## O que a calculadora não diz

- A fórmula de **dano**: como o ataque do personagem, o "ataque" de cada habilidade (por exemplo, 56 a 265 na Thrust) e a defesa do alvo se combinam.
- Os efeitos de **equipamentos**, refinamento e atributos de habilidade.
- Os valores de **monstros** (HP, ataque, defesa) e de **experiência**.
