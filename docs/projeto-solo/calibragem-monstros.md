# Calibragem dos monstros

[Fórmulas de dano](formulas-de-dano.md) · [Companheiros e bots](companheiros-e-bots.md) · [Estrutura do servidor](servidor-estrutura.md) · [Fontes](../fontes.md)

**Estudo de 15/11/2026.** Esta página define quanto os monstros devem aguentar e bater num jogo para **grupos de 4** (jogador + 3 bots). As metas são decisões do proprietário. Os números finais ainda dependem da curva de equipamentos, que não existe nos dados. Os dados estão em [monster-calibration.json](../../data/proposals/monster-calibration.json).

## Metas do proprietário

| Meta | Decisão |
| --- | --- |
| Resistência de um monstro comum de campo | **1 a 2 habilidades de cada membro** do grupo de 4, no mesmo nível (revisto no mesmo dia; antes eram 3 a 4). Sozinho, o jogador derruba um monstro comum com 4 a 8 habilidades. |
| Dano de um golpe de monstro comum | **5% a 10% do HP** de um personagem do mesmo nível que não seja o tanque. Morre com 10 a 20 golpes. |

## Como os monstros funcionam no Melia

- Cada monstro tem valores fixos em `system/db/monsters.txt`: HP, ataque, defesas, precisão, esquiva e experiência. **Não há multiplicador global** de HP ou ataque. [F124](../fontes.md#f124)
- Só os monstros de elite têm multiplicadores próprios (150% de HP e de atributos em `rare_monsters.conf`).
- A tabela vem do jogo atual, posterior ao Re:Build. Os valores foram feitos para o equipamento e os personagens de hoje.

## Monstros comuns por nível (mediana no Melia)

Monstros hostis com experiência, num intervalo de ±10 níveis. A última coluna mostra a meta de dano por golpe, calculada para um personagem sem equipamento com 40% dos pontos em CON.

| Nível | Monstros | HP | Ataque | Defesa | Esquiva | HP do personagem | Meta de dano por golpe |
| --- | --- | --- | --- | --- | --- | --- | --- |
| 10 | 581 | 218 | 82–137 | 63 | 1 | 1.167 | 58–117 |
| 50 | 136 | 3.158 | 416–777 | 356 | 50 | 4.790 | 240–479 |
| 100 | 124 | 9.436 | 1.224–2.105 | 994 | 102 | 10.074 | 504–1.007 |
| 150 | 76 | 22.348 | 1.684–2.947 | 1.480 | 151 | 16.087 | 804–1.609 |
| 200 | 109 | 44.644 | 2.450–4.370 | 2.135 | 200 | 23.024 | 1.151–2.302 |
| 250 | 57 | 60.607 | 3.753–6.687 | 2.654 | 246 | 30.598 | 1.530–3.060 |
| 280 | 97 | 80.069 | 3.932–6.799 | 2.903 | 280 | 35.689 | 1.784–3.569 |

## Como chegar aos números

As duas metas viram duas fórmulas:

- **HP do monstro** = 4 membros × 1 a 2 habilidades × dano médio de uma habilidade do personagem naquele nível.
- **Ataque do monstro** = defesa do personagem + 5% a 10% do HP do personagem naquele nível.

As duas dependem de quanto ataque e defesa o personagem tem em cada nível, e isso vem principalmente do **equipamento**.

### O que falta: a curva de equipamentos

Tentei estimar o ataque e a defesa do personagem com as armas e armaduras do Melia (`items.txt`). Os dados não servem para isso. Eles misturam itens de várias épocas e conjuntos especiais, e há faixas de nível sem nenhuma espada ou armadura comum. O resultado oscilou de forma absurda: no nível 100, o personagem matava qualquer monstro com um golpe; no 150, quase não causava dano. Por isso, esta página não traz HP e ataque finais.

## Como aplicar no servidor

- **Não editar os 4 mil monstros um por um.** O Melia permite substituir as funções que calculam HP, ataque e defesa dos monstros (`SCR_Get_MON_MHP`, `SCR_Get_MON_MINPATK` e outras, em `calc_monster.cs`). Basta aplicar um **multiplicador por faixa de nível** sobre os valores da tabela.
- **Elites e chefes** recebem metas próprias depois.
- **Configurações relacionadas:** `job_max_rank` está em 4 e precisa ir para 7 (`jobs.conf`); `exp_rate` e a curva de experiência entram na mesma etapa.

## Pendências

- Definir a curva de equipamentos (ataque da arma e defesa da armadura por nível).
- Calcular os multiplicadores por faixa de nível a partir dessa curva.
- Definir metas para elites, chefes e masmorras.
- Testar no servidor.

A curva e os multiplicadores calculados estão em [Curva de equipamentos](curva-de-equipamentos.md). O cálculo mostrou que a escala atual das habilidades do Melia exige decidir antes entre reduzir os fatores das habilidades ou multiplicar muito o HP dos monstros.

A escala foi decidida (opção A: fatores divididos por 10), e a [tabela de ajustes](curva-de-equipamentos.md#ajustes-monstros) está pronta.

As curvas das outras armas também estão lá.

**Próximo passo recomendado:** decidir o modelo de defesa, apontado na [comparação das bases](comparacao-das-bases.md).
