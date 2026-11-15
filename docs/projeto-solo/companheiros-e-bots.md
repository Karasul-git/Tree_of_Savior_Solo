# Companheiros, bots e grupo

[Diretrizes solo](diretrizes.md) · [Elenco e pendências](disponibilidade-das-classes.md) · [Remoção das deusas](remocao-das-deusas.md)

**Decisão do proprietário em 15/11/2026.** A adaptação **não será solo puro**. O jogo será pensado para um jogador acompanhado de **bots aliados controlados pelo servidor**, que lutam junto com ele, à semelhança do projeto anterior do proprietário em Ragnarok. Amigos reais também poderão jogar juntos no mesmo servidor.

Esta é uma decisão de design do projeto, não uma descrição do Tree of Savior de 2016. O nome "versão solo" continua sendo usado nas páginas por enquanto; ele passa a significar **um jogador humano por grupo como caso principal**, não ausência de grupo.

## O que fica definido

| Ponto | Decisão atual | O que continua aberto |
| --- | --- | --- |
| Modelo principal | Jogador acompanhado de bots aliados do servidor. | Quantidade de bots por grupo, controle (ordens, posturas, IA) e progressão deles. |
| Jogadores reais | Amigos podem jogar juntos, com ou sem bots. | Regras de grupo misto e divisão de recompensas. |
| Habilidades de grupo | Efeitos sobre "membros do grupo" passam a ter destinatários: bots e amigos. | Conferir cada habilidade; não presumir que funcione igual com bots. |
| Expansão futura | Personagens no mapa controlados pelo servidor, com interação limitada (conversa e outras ações). | Escopo, tipos de interação e tecnologia. **Não faz parte da etapa atual.** |

O modelo de bots ainda não foi especificado. Não há regra aprovada para classes, equipamento, número ou comportamento dos companheiros.

## Efeito nas pendências do elenco

Várias pendências do [quadro de disponibilidade](disponibilidade-das-classes.md) diziam "se o projeto vier a adotar companheiros ou aliados NPC". Com esta decisão, as habilidades de grupo deixam de ser inúteis por falta de aliados. **Isso não aprova nenhuma delas automaticamente:** o tema sem magia e a função de cada uma continuam a ser avaliados.

| Classe | Habilidades afetadas | Leitura com bots | Pendência que permanece | Fonte |
| --- | --- | --- | --- | --- |
| [Templar](../classes/catalogo/templar.md) | Battle Orders, Summon/Warp To Guild Member, Build Guild Tower, Reduce Craft Time | Battle Orders ganha alvos naturais. As demais dependem de **guilda**, não apenas de grupo. | Decidir se o grupo de bots substitui a guilda ou se as habilidades de guilda são revistas. | [F50](../fontes.md#f50) |
| [Squire](../classes/catalogo/squire.md) | Weapon/Armor Maintenance, Repair, Base Camp, Refreshment Table | Serviços e acampamento passam a atender o grupo inteiro. | Base Camp inclui transporte; ver o [inventário das deusas](remocao-das-deusas.md). | [F46](../fontes.md#f46) |
| [Paladin](../classes/catalogo/paladin.md) | Restoration, Barrier, Resist Elements | Efeitos de área para aliados ficam relevantes. | A revisão temática (efeitos sagrados) continua aberta. | [F84](../fontes.md#f84) |
| [Hunter](../classes/catalogo/hunter.md) e [Falconer](../classes/catalogo/falconer.md) | Comandos de companheiros animais | Animais somam-se aos bots no grupo. | Avaliar o número total de aliados controlados. | [F69](../fontes.md#f69), [F75](../fontes.md#f75) |
| Base [Scout](../classes/catalogo/scout.md) | Perspective Distortion e efeitos de apoio | Possíveis alvos aliados passam a existir. | O kit base de Scout continua indefinido. | [F71](../fontes.md#f71) |

Pardoner e Chaplain também tinham serviços e efeitos para o grupo, mas estão fora da seleção solo pelo critério de magia. A decisão sobre bots não os traz de volta.

## Próximos estudos ligados a esta decisão

- Definir o papel dos bots: quantos, quais classes podem assumir e como recebem ordens.
- Reavaliar as habilidades de grupo das classes selecionadas com bots como destinatários.
- Separar habilidades de **guilda** (Templar) das habilidades de **grupo**.
- Registrar, quando houver, a referência técnica do projeto de Ragnarok usado como inspiração.

**Próximo passo recomendado:** definir o kit base de Scout. Em seguida, especificar o grupo de bots (tamanho e controle) antes de rever as habilidades de grupo.
