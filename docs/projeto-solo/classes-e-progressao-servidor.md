# Classes e progressão no servidor

[Estrutura do servidor](servidor-estrutura.md) · [Ranks e especializações solo](ranks-e-especializacoes.md) · [Ranks e círculos de 2016](../sistemas/ranks-e-circulos.md) · [Fontes](../fontes.md)

**Fase 0, rascunho de 15/11/2026.** Esta página converte a árvore solo para os dados do [Melia](servidor-estrutura.md) e propõe o esqueleto de progressão. É uma tabela de trabalho: nada foi aplicado ou testado no servidor, e a árvore foi ratificada pelo proprietário no mesmo dia. Os dados estão em [solo-jobs-melia.json](../../data/proposals/solo-jobs-melia.json).

## Resumo

- **39 classes**, todas presentes no `jobs.txt` do Melia. [F124](../fontes.md#f124)
- **9 não precisam de mudança**, **26 precisam mudar de rank** e **4 precisam mudar de rank e de árvore**: Monk e Paladin (Cleric → Warrior), Alchemist (Wizard → Scout) e Plague Doctor (Cleric → Scout).
- No Melia, todas as especializações estão no rank 2, porque o jogo atual tem uma base e três avanços. A árvore solo volta a escalonar os ranks de R2 a R7, como em 2016.
- Mudar a árvore de uma classe no servidor pode exigir mudança nos dados do cliente. É o ponto de maior risco da tabela.

## Esqueleto de progressão

| Regra | Referência histórica | Proposta solo | Situação | Fonte |
| --- | --- | --- | --- | --- |
| Rank máximo | Rank 7 no lançamento. | **R7**, com a base em R1 e duas especializações novas por rank. | Decidido | [F127](../fontes.md#f127) |
| Nível de classe por rank | 15 níveis de classe para avançar. | **15 por rank**, como em 2016. | Proposta | [F01](../fontes.md#f01), [F128](../fontes.md#f128) |
| Círculos | Repetir uma classe dava C2 e C3. | **Até C3.** Quem entra em R5 chega a C3; em R6, a C2; em R7, só C1. | Proposta | [F01](../fontes.md#f01) |
| Nível máximo do personagem | 280 no acesso antecipado de 2016. | **280** como referência inicial. | Proposta | [F126](../fontes.md#f126) |
| Pontos de habilidade | 1 por nível de classe, segundo jogadores do beta. | 1 por nível de classe. | Pendente de fonte de 2016 | [F128](../fontes.md#f128) |
| Pontos de atributo | Distribuição livre por nível antes do Re:Build. | Voltar à distribuição livre. No Melia, desligar `NoStatByLevel`. | Quantidade por nível pendente | [F124](../fontes.md#f124) |
| Curva de experiência | Não documentada nesta wiki. | A definir no balanceamento, considerando grupos de 4. | Pendente | — |

### O que muda no Melia

| Arquivo ou parte | Hoje | Necessário |
| --- | --- | --- |
| `system/db/jobs.txt` | Especializações no rank 2; árvores do jogo atual. | Rank solo de cada classe e mudança de árvore das quatro classes acima. |
| `system/db/exp.txt` (experiência de classe) | Ranks 1 a 4, com 45 níveis de classe. | Ranks 1 a 7, com 15 níveis cada. |
| `system/db/exp.txt` (experiência do personagem) | Até o nível 550. | Limite de 280 e curva própria. |
| `system/db/features.txt` | Chaves do Re:Build ligadas. | Desligar as que conflitam com o sistema de 2016, como `NoStatByLevel`. |
| Círculos | O código já conhece C1 a C3 (`JobCircle`). | Ligar a regra de repetir classe ao avanço de rank. |
| Dados do cliente | Experiência e classes do jogo atual. | O próprio Melia avisa que a experiência precisa mudar também no cliente. Conferir o mesmo para ranks e árvores. |

## Warrior

| Rank solo | Classe | Posição | Rank no Melia | Árvore no Melia | Círculo máximo até R7 | Mudança no servidor |
| --- | --- | --- | --- | --- | --- | --- |
| R1 | [Swordsman](../classes/catalogo/swordsman.md) | Definida | 1 | Swordsman | C3 | Nenhuma |
| R2 | [Highlander](../classes/catalogo/highlander.md) | Definida | 2 | Swordsman | C3 | Nenhuma |
| R2 | [Peltasta](../classes/catalogo/peltasta.md) | Definida | 2 | Swordsman | C3 | Nenhuma |
| R3 | [Barbarian](../classes/catalogo/barbarian.md) | Definida | 2 | Swordsman | C3 | rank 2 → 3 |
| R3 | [Hoplite](../classes/catalogo/hoplite.md) | Definida | 2 | Swordsman | C3 | rank 2 → 3 |
| R4 | [Cataphract](../classes/catalogo/cataphract.md) | Definida | 2 | Swordsman | C3 | rank 2 → 4 |
| R4 | [Rodelero](../classes/catalogo/rodelero.md) | Definida | 2 | Swordsman | C3 | rank 2 → 4 |
| R5 | [Monk](../classes/catalogo/monk.md) | Definida | 2 | Cleric | C3 | rank 2 → 5; árvore Cleric → Warrior |
| R5 | [Paladin](../classes/catalogo/paladin.md) | Definida | 2 | Cleric | C3 | rank 2 → 5; árvore Cleric → Warrior |
| R6 | [Doppelsoeldner](../classes/catalogo/doppelsoeldner.md) | Definida | 2 | Swordsman | C2 | rank 2 → 6 |
| R6 | [Fencer](../classes/catalogo/fencer.md) | Definida | 2 | Swordsman | C2 | rank 2 → 6 |
| R7 | [Dragoon](../classes/catalogo/dragoon.md) | Definida | 2 | Swordsman | C1 | rank 2 → 7 |
| R7 | [Templar](../classes/catalogo/templar.md) | Definida | 2 | Swordsman | C1 | rank 2 → 7 |

## Archer

| Rank solo | Classe | Posição | Rank no Melia | Árvore no Melia | Círculo máximo até R7 | Mudança no servidor |
| --- | --- | --- | --- | --- | --- | --- |
| R1 | [Archer](../classes/catalogo/archer.md) | Definida | 1 | Archer | C3 | Nenhuma |
| R2 | [Quarrel Shooter](../classes/catalogo/quarrel-shooter.md) | Definida | 2 | Archer | C3 | Nenhuma |
| R2 | [Ranger](../classes/catalogo/ranger.md) | Definida | 2 | Archer | C3 | Nenhuma |
| R3 | [Hunter](../classes/catalogo/hunter.md) | Definida | 2 | Archer | C3 | rank 2 → 3 |
| R3 | [Sapper](../classes/catalogo/sapper.md) | Definida | 2 | Archer | C3 | rank 2 → 3 |
| R4 | [Appraiser](../classes/catalogo/appraiser.md) | Definida | 2 | Archer | C3 | rank 2 → 4 |
| R4 | [Wugushi](../classes/catalogo/wugushi.md) | Definida | 2 | Archer | C3 | rank 2 → 4 |
| R5 | [Arbalester](../classes/catalogo/arbalester.md) | Definida | 2 | Archer | C3 | rank 2 → 5 |
| R5 | [Fletcher](../classes/catalogo/fletcher.md) | Definida | 2 | Archer | C3 | rank 2 → 5 |
| R6 | [Falconer](../classes/catalogo/falconer.md) | Definida | 2 | Archer | C2 | rank 2 → 6 |
| R6 | [Mergen](../classes/catalogo/mergen.md) | Definida | 2 | Archer | C2 | rank 2 → 6 |
| R7 | [Cannoneer](../classes/catalogo/cannoneer.md) | Definida | 2 | Archer | C1 | rank 2 → 7 |
| R7 | [Musketeer](../classes/catalogo/musketeer.md) | Definida | 2 | Archer | C1 | rank 2 → 7 |

## Scout

| Rank solo | Classe | Posição | Rank no Melia | Árvore no Melia | Círculo máximo até R7 | Mudança no servidor |
| --- | --- | --- | --- | --- | --- | --- |
| R1 | [Scout](../classes/catalogo/scout.md) | Definida | 1 | Scout | C3 | Nenhuma |
| R2 | [Assassin](../classes/catalogo/assassin.md) | Definida | 2 | Scout | C3 | Nenhuma |
| R2 | [Clown](../classes/catalogo/clown.md) | Definida | 2 | Scout | C3 | Nenhuma |
| R3 | [Outlaw](../classes/catalogo/outlaw.md) | Definida | 2 | Scout | C3 | rank 2 → 3 |
| R3 | [Sheriff](../classes/catalogo/sheriff.md) | Definida | 2 | Scout | C3 | rank 2 → 3 |
| R4 | [Ardito](../classes/catalogo/ardito.md) | Definida | 2 | Scout | C3 | rank 2 → 4 |
| R4 | [Rogue](../classes/catalogo/rogue.md) | Definida | 2 | Scout | C3 | rank 2 → 4 |
| R5 | [Corsair](../classes/catalogo/corsair.md) | Definida | 2 | Scout | C3 | rank 2 → 5 |
| R5 | [Squire](../classes/catalogo/squire.md) | Definida | 2 | Scout | C3 | rank 2 → 5 |
| R6 | [Alchemist](../classes/catalogo/alchemist.md) | Definida | 2 | Wizard | C2 | rank 2 → 6; árvore Wizard → Scout |
| R6 | [Schwarzer Reiter](../classes/catalogo/schwarzer-reiter.md) | Definida | 2 | Scout | C2 | rank 2 → 6 |
| R7 | [Plague Doctor](../classes/catalogo/plague-doctor.md) | Definida | 2 | Cleric | C1 | rank 2 → 7; árvore Cleric → Scout |
| R7 | [Shinobi](../classes/catalogo/shinobi.md) | Definida | 2 | Scout | C1 | rank 2 → 7 |

## Pendências

- Confirmar com fonte de 2016 os pontos de habilidade e de atributo por nível.
- Verificar se o cliente atual aceita classes de outra árvore e ranks escalonados sem mudar os arquivos dele.
- Definir a curva de experiência para grupos de 4.

Os pontos por nível foram detalhados em [Pontos de atributo e de habilidade](pontos-por-nivel.md).

**Próximo passo recomendado:** listar as habilidades da árvore solo que usam ataque mágico, para definir o novo papel do INT.
