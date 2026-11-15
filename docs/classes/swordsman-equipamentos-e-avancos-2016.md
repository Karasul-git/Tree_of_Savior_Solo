# Swordsman · Armas, mestres e avanços em 2016

[Ficha de Swordsman](swordsman.md) · [Valores por nível](swordsman-valores-2016.md) · [Ranks e círculos](../sistemas/ranks-e-circulos.md)

**Revisão de 15/11/2026 · janela de março–abril de 2016.** Quatro listas arquivadas de equipamentos e uma revisão histórica do repositório oficial de tradução ampliam a ficha. Há evidência documental para equipamentos e NPCs; a sequência exata das missões ainda exige condições de execução da edição.

## Equipar uma arma e usar uma habilidade são regras distintas

Um item pode aceitar a árvore Swordsman e ainda depender de um atributo de especialização. Uma habilidade também pode exigir uma combinação específica de armas. Por isso, a coluna “classe permitida” do banco não representa toda a compatibilidade de uma build.

| Equipamento | O que o registro de 2016 sustenta | Exemplo preservado | Fonte |
| --- | --- | --- | --- |
| Espada de uma mão | O item lista Swordsman entre as classes permitidas. | Artie Saif: nível exigido 120. | [F31](../fontes.md#f31) |
| Maça de uma mão | O item também lista Swordsman; a árvore não se restringia a espadas. | Battle Hammer: nível exigido 75. | [F32](../fontes.md#f32) |
| Escudo | O exemplo permite todas as classes. É um equipamento da mão secundária, combinável com arma de uma mão. | Aston Shield: nível exigido 15. | [F30](../fontes.md#f30) · [F33](../fontes.md#f33) |
| Adaga | O exemplo permite todas as classes. A adaga é secundária; isso não equivale a portar duas espadas principais. | Arde Dagger: nível exigido 75. | [F30](../fontes.md#f30) · [F34](../fontes.md#f34) |
| Espada de duas mãos | Não presumir liberação por Swordsman C1: há relato de abril sobre aprender o atributo com mestres de especializações, citando Highlander e Barbarian. | Requisito documentado por jogador; exemplos não formam uma lista exaustiva. | [F35](../fontes.md#f35) |
| Lanças e rapieiras | Guias relacionam seu uso a caminhos como Hoplite e Fencer. | Conferir a permissão e a postura na ficha de cada especialização. | [F03](../fontes.md#f03) |

Os exemplos demonstram permissões exibidas para esses itens; não são recomendações de equipamento inicial, nem um catálogo completo. Os níveis da tabela são requisitos de personagem dos itens, não Class Level. As listas têm capturas próprias: espada em 04/04, maça em 01/04, escudo em 04/04 e adaga em 01/04/2016. Não as tratamos como um único build do cliente.

## Troca de armas

A revisão da tradução de 15/04/2016 contém os identificadores `UI_20150323_001380` e `UI_20150717_001727`, que registram **Alt+Z** para trocar armas. O guia contemporâneo também recomenda Weapon Swap. [F30](../fontes.md#f30) · [F02](../fontes.md#f02)

A captura da ficha de Swordsman exibe Weapon Swap sob Circle 2. Mantemos essa observação, mas ela não prova que todo personagem precisasse escolher Swordsman C2 para utilizar a função: faltam os requisitos efetivos de aprendizado da edição. [F26](../fontes.md#f26)

## Mestres e cidades

| NPC | Evidência encontrada | Limite |
| --- | --- | --- |
| **Rashua — Swordsman Master** | O nome aparece no rótulo `ETC_20150317_009204`; `QUEST_JOBSTEP_20150317_001389` situa o mestre em **Klaipeda**. | Posição exata no mapa e lista completa de serviços ainda não auditadas. |
| **Codill — Swordsman Submaster** | O rótulo `ETC_20151022_016440` preserva essa grafia. Há texto para o submestre em **Orsha** em QUEST_UNUSED e um relato de jogador que o visitou no dia de abertura. | O texto em arquivo não utilizado não certifica uma missão ativa; o relato não fornece o nome próprio. |

Referências: [F27](../fontes.md#f27), [F28](../fontes.md#f28), [F29](../fontes.md#f29) e [F36](../fontes.md#f36). A ligação entre o título do submestre, o nome e a cidade é um cruzamento documental; a validação visual no cliente permanece pendente.

## Começar como Swordsman e avançar para outro círculo

**Swordsman C1 era uma escolha na criação do personagem.** Não apresentamos uma prova posterior como se fosse necessária para começar nessa classe. Ao alcançar Class Level 15, o jogador podia seguir para outro círculo ou para uma classe disponível no rank seguinte, conforme seu caminho. [F01](../fontes.md#f01)

O relato de 29/03/2016 menciona o botão de avanço e a visita a mestres/submestres. Como a conversa trata de uma falha na interface, não usamos todas as sugestões dos participantes como instruções definitivas. [F36](../fontes.md#f36)

## Provas preservadas nos textos históricos

O arquivo QUEST_JOBSTEP contém diferentes objetivos ligados ao mestre de Swordsman. Estes são exemplos para investigação, **sem atribuição confirmada a C2/C3, ordem obrigatória ou quantidade de alvos**. [F28](../fontes.md#f28)

| Objetivo resumido | Local citado | Identificador para conferência |
| --- | --- | --- |
| Coletar corações de Stone Orca e retornar ao mestre. | Crystal Mine 1F | `QUEST_JOBSTEP_20150317_001642` a `001645` |
| Derrotar Minotaur e reportar ao mestre. | Mage Tower 3F | `QUEST_JOBSTEP_20150317_001561` e `001562` |
| Enfrentar Glizardon como prova de aprendizado. | Tenet Church B1 | `QUEST_JOBSTEP_20150729_003040` |

**Esses identificadores são chaves de tradução, não IDs de missão nem datas de ativação.** O repositório também conserva variantes de locais e monstros. A presença de uma linha não revela sozinha qual variante era executada no servidor internacional.

Não montamos uma sequência fictícia unindo esses exemplos. Quantidades, recompensas, círculo, rank, pré-requisitos e diferenças entre as rotas de Klaipeda e Orsha permanecem sem confirmação suficiente.

## Por que QUEST_UNUSED fica separado?

O próprio nome do arquivo identifica textos não utilizados. Ele preserva uma instrução de Orsha envolvendo Lapflammer e Lapeman em Paupys Crossing, mas isso serve como pista histórica, **não como passo a passo confirmado**. [F29](../fontes.md#f29)

Esse cuidado também vale para QUEST_JOBSTEP: a classificação do arquivo ajuda a localizar material, mas não substitui os scripts ou um registro da missão funcionando naquela edição.

## Procedência e pendências

O [manifesto desta pesquisa](../../data/research/swordsman-equipment-advancement.json) registra o commit da tradução, as URLs arquivadas e hashes SHA-256 dos oito arquivos consultados como evidência. O [registro da classe](../../data/classes/swordsman.json) conserva as chaves e mantém desconhecidos os campos sem prova. Não redistribuímos os arquivos integrais de tradução ou HTMLs do banco.

Para fechar a conferência no cliente, faltam:

- A matriz completa de itens, atributos de equipamento e posturas de habilidades.
- Coordenadas e serviços de cada mestre.
- Missões efetivamente ativas, ligação a rank/círculo, contadores e recompensas.
- Diferenças entre beta, rotas iniciais e o patch de 29/03/2016.

**Próximo passo recomendado:** consultar o [catálogo de classes](catalogo-2016.md) e consolidar a seleção solo sem magia, conforme as [diretrizes](../projeto-solo/diretrizes.md). O aprofundamento individual será retomado depois dessa visão geral.
