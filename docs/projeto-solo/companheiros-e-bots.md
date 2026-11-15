# Companheiros, bots e grupo

[Diretrizes solo](diretrizes.md) · [Elenco e pendências](disponibilidade-das-classes.md) · [Remoção das deusas](remocao-das-deusas.md)

**Decisão do proprietário em 15/11/2026.** A adaptação **não será solo puro**. O jogo será pensado para um jogador acompanhado de **bots aliados controlados pelo servidor**, que lutam junto com ele, à semelhança do projeto anterior do proprietário em Ragnarok. Amigos reais também poderão jogar juntos no mesmo servidor.

Esta é uma decisão de design do projeto, não uma descrição do Tree of Savior de 2016. O nome "versão solo" continua sendo usado nas páginas por enquanto; ele passa a significar **um jogador humano por grupo como caso principal**, não ausência de grupo.

## Especificação inicial · decisões de 15/11/2026

O proprietário escolheu as quatro bases do sistema. São **decisões de design**, sem implementação, números ou interface definidos. Os dados estão em [bots-spec.json](../../data/proposals/bots-spec.json).

| Ponto | Decisão | O que continua aberto |
| --- | --- | --- |
| **Origem dos bots** | **Dois tipos:** personagens do próprio barracão (a equipe da conta) como regra, e bots gerados pelo servidor para completar o grupo ou para tarefas específicas. | Como se obtêm os bots do servidor (recrutamento em NPC, contrato, custo), se têm classe e equipamento fixos e se podem ser trocados. |
| **Tamanho do grupo** | **Jogador + até 4 bots**, grupo de 5. Amigos que entram ocupam vagas de bot. | O limite de 5 só tem fonte do beta de 2015 ([pesquisa](../sistemas/barracao-e-grupo.md)); decidir se animais de Hunter e Falconer contam como vaga. |
| **Controle** | **Posturas + ordens simples.** Posturas: seguir, agressivo, defensivo, passivo. Ordens: atacar o alvo, recuar/reagrupar, manter posição. As habilidades saem de um **perfil por classe**. | Interface (atalhos, menu, comandos de chat) e o conteúdo de cada perfil. |
| **Progressão** | **Ganham experiência junto** com o grupo e seguem as mesmas regras de ranks e círculos do jogador. | Divisão de experiência entre jogador e bots; progressão dos bots do servidor. |
| Jogadores reais | Amigos podem jogar juntos, com ou sem bots. | Regras de grupo misto e divisão de recompensas. |
| Expansão futura | Personagens no mapa controlados pelo servidor, com interação limitada (conversa e outras ações). | Escopo e tecnologia. **Fora da etapa atual.** |

### Por que o barracão

O barracão já reunia vários personagens da mesma conta numa equipe. Usá-lo como fonte dos bots **reaproveita um sistema original**: o jogador escolhe classe, build e equipamento de cada companheiro com as mesmas regras do próprio personagem. A [pesquisa do barracão e do grupo](../sistemas/barracao-e-grupo.md) mostra quatro vagas iniciais, animais ocupando vagas e um bônus de experiência por nível de equipe, com fontes de 2016 majoritariamente de jogadores. Quatro vagas dão o jogador + 3; completar o grupo de 5 exige comprar vagas ou usar bots do servidor. As três opções originais de divisão de experiência (separada, igual, por nível) podem servir à progressão dos bots.

### Perfis por classe

Cada classe da seleção precisará de um perfil: quais habilidades o bot usa, em que ordem e com que prioridade (por exemplo, quando usar uma poção ou um efeito de grupo). A ordem sugerida de trabalho:

1. As três bases: Warrior (Swordsman), Archer e Scout, com o [kit ratificado](scout-kit-base.md).
2. Especializações com habilidades de grupo: Squire, Templar, Paladin e Alchemist.
3. As demais especializações, por rank.

Os bots usam **poções e consumíveis originais** do próprio inventário, seguindo a regra de começar pelas poções existentes antes de criar sistemas de cura.

### Dependências com outras decisões

- **Derrota e retorno de um bot** dependem do substituto das estátuas. Ver a [remoção das deusas](remocao-das-deusas.md).
- **Habilidades de guilda de Templar** continuam sem destinatário: o grupo de bots não é uma guilda. É preciso decidir se as habilidades de guilda passam a valer para o grupo ou se são revistas.
- **Base técnica:** o projeto de Ragnarok do proprietário é um mod sobre o rAthena e também ainda não especifica tipo e autonomia dos companheiros. Para o ToS, a base técnica equivalente ainda não foi escolhida. A wiki cita o Melia/Laima apenas como base de outro projeto, não como prova de 2016. Avaliar se a base escolhida suporta IA de aliados é uma etapa própria.

## Efeito nas pendências do elenco

Várias pendências do [quadro de disponibilidade](disponibilidade-das-classes.md) diziam "se o projeto vier a adotar companheiros ou aliados NPC". Com esta decisão, as habilidades de grupo deixam de ser inúteis por falta de aliados. **Isso não aprova nenhuma delas automaticamente:** o tema sem magia e a função de cada uma continuam a ser avaliados.

| Classe | Habilidades afetadas | Leitura com bots | Pendência que permanece | Fonte |
| --- | --- | --- | --- | --- |
| [Templar](../classes/catalogo/templar.md) | Battle Orders, Summon/Warp To Guild Member, Build Guild Tower, Reduce Craft Time | Battle Orders ganha alvos naturais. As demais dependem de **guilda**, não apenas de grupo. | Decidir se o grupo de bots substitui a guilda ou se as habilidades de guilda são revistas. | [F50](../fontes.md#f50) |
| [Squire](../classes/catalogo/squire.md) | Weapon/Armor Maintenance, Repair, Base Camp, Refreshment Table | Serviços e acampamento passam a atender o grupo inteiro. | Base Camp inclui transporte; ver o [inventário das deusas](remocao-das-deusas.md). | [F46](../fontes.md#f46) |
| [Paladin](../classes/catalogo/paladin.md) | Restoration, Barrier, Resist Elements | Efeitos de área para aliados ficam relevantes. | A revisão temática (efeitos sagrados) continua aberta. | [F84](../fontes.md#f84) |
| [Hunter](../classes/catalogo/hunter.md) e [Falconer](../classes/catalogo/falconer.md) | Comandos de companheiros animais | Animais somam-se aos bots no grupo. | Avaliar o número total de aliados controlados. | [F69](../fontes.md#f69), [F75](../fontes.md#f75) |
| Base [Scout](../classes/catalogo/scout.md) | Perspective Distortion e efeitos de apoio | Possíveis alvos aliados passam a existir. | Ver o [kit base ratificado](scout-kit-base.md). | [F71](../fontes.md#f71) |

Pardoner e Chaplain também tinham serviços e efeitos para o grupo, mas estão fora da seleção solo pelo critério de magia. A decisão sobre bots não os traz de volta.

## Próximos estudos ligados a esta decisão

- Decidir as vagas do barracão para bots e se os animais contam no limite de 4.
- Definir a obtenção e as regras dos bots do servidor.
- Escrever os perfis de comportamento das três bases.
- Reavaliar as habilidades de grupo das classes selecionadas com bots como destinatários.
- Separar habilidades de **guilda** (Templar) das habilidades de **grupo**.

**Próximo passo recomendado:** decidir as vagas do barracão e os animais no limite de bots; depois, escrever os perfis de comportamento das três bases.
