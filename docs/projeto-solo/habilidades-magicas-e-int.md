# Habilidades mágicas e o novo INT

[Pontos de atributo e de habilidade](pontos-por-nivel.md) · [Classes e progressão no servidor](classes-e-progressao-servidor.md) · [Elenco e pendências](disponibilidade-das-classes.md) · [Fontes](../fontes.md)

**Levantamento de 15/11/2026.** O proprietário decidiu dar um novo papel ao INT. Para escolher esse papel, esta página lista as habilidades da árvore solo que ainda usam magia nos dados do servidor. Os dados estão em [magic-skills-int.json](../../data/proposals/magic-skills-int.json).

## Como foi feito

- Para as 39 classes da árvore, cruzei a árvore de habilidades do Melia (`skilltree.txt`) com o cadastro de habilidades (`skills.txt`). [F124](../fontes.md#f124)
- Entraram as habilidades marcadas como mágicas (`classType` ou `attackType` igual a `Magic`).
- Também conferi as habilidades de 2016 do catálogo que saíram do kit atual mas continuam no cadastro do Melia.
- **Limite:** os dados do Melia são do jogo posterior ao Re:Build. Uma habilidade de 2016 pode ter funcionado de outro jeito naquele ano.

## Resultado

**A magia está concentrada em quatro classes.** Nas árvores atuais dessas 39 classes, o Melia tem 260 habilidades: 181 corpo a corpo, 50 à distância, 16 mágicas e 13 de outros tipos. Ao todo, **26 habilidades** usam magia ou ataque mágico:

| Categoria | Quantidade | Habilidades |
| --- | --- | --- |
| Dano | 4 | Combustion, Alchemistic Missile, Incineration, Black Death Steam |
| Dano (execução) | 1 | Turn Undead |
| Cura | 4 | Restoration, Sprinkle HP Potion, Sprinkle SP Potion, Healing Factor |
| Apoio, defesa e controle | 11 | Iron Skin, Resist Elements, Conversion, Barrier, Stone Skin, Sanctuary, Bloodletting, Fumigate, Beak Mask, Modafinil, Pandemic |
| Fabricação e serviço | 6 | Dig, Item Awakening, Briquetting, Tincturing, Magnum Opus, Gem Roasting |

As outras 35 classes não têm nenhuma habilidade mágica nesses dados.

### [Monk](../classes/catalogo/monk.md) · Warrior R5

| Habilidade | Origem | Categoria | Função | Tipo no Melia |
| --- | --- | --- | --- | --- |
| Iron Skin | 2016 e posterior | Apoio e defesa | Reflete ataques físicos com o corpo endurecido. | Magic · Melee |

### [Paladin](../classes/catalogo/paladin.md) · Warrior R5

| Habilidade | Origem | Categoria | Função | Tipo no Melia |
| --- | --- | --- | --- | --- |
| Restoration | 2016 e posterior | Cura | Aura que melhora a recuperação de HP próxima. | Magic · Holy |
| Resist Elements | Só 2016 | Apoio e defesa | Reforça resistências elementais. | Magic · Holy |
| Turn Undead | Só 2016 | Dano (execução) | Pode derrotar na hora demônios e mutantes. | Magic · Holy |
| Conversion | Só 2016 | Controle | Círculo com chance de converter inimigos. | Magic · Holy |
| Barrier | 2016 e posterior | Apoio e defesa | Barreira que afasta inimigos e protege aliados. | Magic · Holy |
| Stone Skin | Posterior | Apoio e defesa | Habilidade defensiva do kit atual. | Magic · Holy |
| Sanctuary | Posterior | Apoio e defesa | Área de proteção do kit atual. | Magic · Holy |

### [Alchemist](../classes/catalogo/alchemist.md) · Scout R6

| Habilidade | Origem | Categoria | Função | Tipo no Melia |
| --- | --- | --- | --- | --- |
| Combustion | 2016 e posterior | Dano | Explode itens no chão para atingir inimigos. | Magic, ataque mágico · Fire |
| Alchemistic Missile | Posterior | Dano | Projétil de energia do kit atual. | Magic, ataque mágico · Soul |
| Sprinkle HP Potion | Posterior | Cura | Aplica poção de HP em si e no grupo. | Magic, ataque mágico |
| Sprinkle SP Potion | Posterior | Cura | Aplica poção de SP em si e no grupo. | Magic, ataque mágico |
| Dig | 2016 e posterior | Fabricação e serviço | Escava materiais. | Ataque mágico |
| Item Awakening | 2016 e posterior | Fabricação e serviço | Revela propriedade oculta de item. | Ataque mágico |
| Briquetting | Só 2016 | Fabricação e serviço | Combina itens para alterar propriedades. | Ataque mágico |
| Tincturing | 2016 e posterior | Fabricação e serviço | Produz poções a partir de ervas. | Ataque mágico |
| Magnum Opus | 2016 e posterior | Fabricação e serviço | Combina materiais para criar itens. | Ataque mágico |
| Gem Roasting | 2016 e posterior | Fabricação e serviço | Serviço que reduz penalidades de gemas. | Ataque mágico |

### [Plague Doctor](../classes/catalogo/plague-doctor.md) · Scout R7

| Habilidade | Origem | Categoria | Função | Tipo no Melia |
| --- | --- | --- | --- | --- |
| Healing Factor | 2016 e posterior | Cura | Recuperação rápida de HP após sofrer dano. | Magic, ataque mágico · Holy |
| Incineration | 2016 e posterior | Dano | Queima alvos com estados negativos. | Magic, ataque mágico · Fire |
| Black Death Steam | Posterior | Dano | Vapor venenoso do kit atual. | Magic, ataque mágico · Poison |
| Bloodletting | Só 2016 | Apoio e defesa | Protege contra estados negativos por sangria. | Magic, ataque mágico · Holy |
| Fumigate | 2016 e posterior | Apoio e defesa | Nuvem de antídoto que remove estados negativos. | Magic, ataque mágico · Holy |
| Beak Mask | 2016 e posterior | Apoio e defesa | Máscara que protege contra estados negativos. | Magic, ataque mágico · Poison |
| Modafinil | Posterior | Apoio e defesa | Estimulante do kit atual. | Magic, ataque mágico |
| Pandemic | 2016 e posterior | Controle | Espalha estados negativos do alvo para outros inimigos. | Magic, ataque mágico · Holy |

## Decisão: INT como potência de preparados

**15/11/2026 — decisão do proprietário.** O INT passa a medir a **potência de preparados**: química, medicina e consumíveis. Ele aumenta o dano de explosões, fogo e veneno de Alchemist e Plague Doctor e a força das curas por poções e medicina. Os valores por ponto e as fórmulas ainda serão definidos.

## Por que esse papel

- **Todo o dano mágico restante vem de química:** itens que explodem, fogo, veneno e vapor de Alchemist e Plague Doctor.
- **A cura também vem de preparados:** poções aplicadas, recuperação medicinal. A exceção é a aura de Paladin.
- **Monk e Paladin** usam magia em efeitos sagrados, que já estão em revisão temática. Esses casos se resolvem na revisão das classes, não pelo INT.

Por isso, das três ideias da página de pontos, a que cobre o que existe é **INT como potência de preparados**: química, medicina e consumíveis. Ele aumentaria o dano de explosões, fogo e veneno de Alchemist e Plague Doctor e a força das curas por poções e medicina. As outras ideias (explosivos e armadilhas de Sapper e Cannoneer, ou precisão técnica) atingiriam habilidades que hoje são físicas e exigiriam mudar fórmulas que funcionam.

## Pendências

- Conferir no cliente se a mudança do tipo de dano dessas habilidades funciona.
- Decidir o destino de Turn Undead, Conversion e Resist Elements na revisão de Paladin.
- Verificar se Alchemistic Missile entra no kit, já que o estudo de suporte sugeria substituir projéteis de energia.

**Próximo passo recomendado:** montar as fórmulas de dano: como STR, DEX e o INT de preparados viram dano, e quanto os monstros precisam aguentar com um grupo de 4.
