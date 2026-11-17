# Pontos de atributo e de habilidade

[Classes e progressão no servidor](classes-e-progressao-servidor.md) · [Estrutura do servidor](servidor-estrutura.md) · [Remoção das deusas](remocao-das-deusas.md) · [Fontes](../fontes.md)

**Estudo de 15/11/2026.** Esta página define como o personagem ganha pontos, a base de todas as fórmulas de balanceamento. As três escolhas de design abaixo são do proprietário. Os números ainda são provisórios. Os dados estão em [points-per-level.json](../../data/proposals/points-per-level.json).

**Sobre as fontes:** os guias oficiais atuais descrevem o jogo posterior ao Re:Build, com distribuição automática de atributos e nível máximo 470. O sistema anterior só aparece em relatos de jogadores do **beta de 2015**. Ainda não há fonte de março–abril de 2016 para estes números.

## Decisões do proprietário

| Tema | Decisão |
| --- | --- |
| **INT** | **Potência de preparados** (decidido em seguida, no mesmo dia): química, medicina e consumíveis. [Levantamento e decisão](habilidades-magicas-e-int.md). |
| **Bônus por investimento** | **Manter**, como no original. |
| **Pontos extras de história, revelações e estátuas** | **Realocar** para missões, exploração e marcos do mapa, em quantidade parecida. |

## Os cinco atributos

| Atributo | Função no jogo (guia oficial atual) | Situação no projeto |
| --- | --- | --- |
| STR | Ataque físico. | Mantém. |
| CON | HP máximo e defesas. | Mantém. |
| INT | Ataque mágico. | **Potência de preparados.** |
| SPR → **Sabedoria** | Recuperação, SP máximo e crítico mágico. | **Passa a se chamar Sabedoria** (SPR vem de *Spirit*). Mantém recuperação e SP; o crítico mágico perde função. |
| DEX | Velocidade de ataque e crítico físico. | Mantém. |

**Nome de SPR (15/11/2026, decisão do proprietário):** o atributo passa a se chamar **Sabedoria** no projeto, com a mesma função. Para o nome aparecer no jogo, será preciso mudar os textos de localização do cliente e do servidor.

Descrições do guia oficial atual. [F113](../fontes.md#f113) Os valores por ponto que ele traz são do jogo posterior e não foram adotados.

### Ideias consideradas para o novo INT

- **Potência de consumíveis e preparados:** poções, venenos e frascos de Alchemist e Plague Doctor.
- **Técnica com dispositivos:** explosivos, armadilhas e munições especiais (Sapper, Cannoneer, Ardito).
- **Precisão técnica:** acerto ou dano de habilidades que hoje usam ataque mágico.

O [levantamento das habilidades mágicas](habilidades-magicas-e-int.md) mostra que o dano e a cura mágicos restantes vêm de química e medicina de Alchemist e Plague Doctor.

## Pontos de atributo

| Regra | Referência (beta de 2015) | Proposta solo | Fonte |
| --- | --- | --- | --- |
| Por nível | 1 ponto por nível. | **1 por nível**: 279 pontos até o nível 280. | [F129](../fontes.md#f129) |
| Bônus por investimento | +1 a cada 5 pontos até 50, a cada 4 até 150, a cada 3 até 300 e a cada 2 até 500. | **Manter a mesma escala.** | [F130](../fontes.md#f130) |
| Pontos extras | ~18 da história principal, 1 por missão de revelação e ~6 das estátuas. | **Realocar** em missões, exploração e marcos, cerca de 24 pontos mais o equivalente às revelações. | [F129](../fontes.md#f129) |
| Atributos iniciais | Não documentados para 2016. | Partir dos valores do Melia: Swordsman 7/7/2/3/6, Archer 8/4/2/3/8, Scout 6/5/2/3/9 (STR/CON/INT/SPR/DEX). | [F124](../fontes.md#f124) |

### Quanto o bônus por investimento rende

Contando só os pontos investidos, sem os atributos iniciais:

| Pontos investidos num atributo | Bônus | Total no atributo |
| --- | --- | --- |
| 50 | +10 | 60 |
| 100 | +22 | 122 |
| 150 | +35 | 185 |
| 279 (tudo num atributo) | +78 | 357 |

A discussão do beta não concorda se os atributos iniciais da classe entram nessa contagem. Fica pendente.

## Pontos de habilidade

| Regra | Referência | Proposta solo | Fonte |
| --- | --- | --- | --- |
| Por nível de classe | 1 ponto por nível de classe. | **1 por nível de classe.** | [F128](../fontes.md#f128) |
| Por rank | 15 níveis de classe por rank. | **15 pontos por rank**, 105 até R7. | [F128](../fontes.md#f128) |
| Nível máximo das habilidades por círculo | Não documentado nesta wiki. | Pendente. | — |

## Efeito nos bots

Os bots seguem as mesmas regras. Como a distribuição é livre, cada classe vai precisar de uma **distribuição padrão de atributos** para os bots do servidor. Os bots do barracão usam a distribuição que o próprio jogador fizer.

## No Melia

- Desligar `NoStatByLevel` e `IncreasedStatRatio` em `features.txt` para voltar aos pontos livres por nível. [F124](../fontes.md#f124)
- Conferir se o Melia já implementa o bônus por investimento ou se é preciso programá-lo.
- O novo papel do INT exige mudanças nas fórmulas de dano do servidor.

## Pendências

- Confirmar os números com fonte de 2016.
- Definir se os atributos iniciais contam no bônus por investimento.
- Definir o nível máximo de cada habilidade por círculo.

As fórmulas estão em [Fórmulas de dano](formulas-de-dano.md).

**Próximo passo recomendado:** decidir as propostas pendentes das fórmulas de dano.
