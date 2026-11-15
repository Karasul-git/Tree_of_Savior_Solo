# Estrutura do servidor

[Diretrizes solo](diretrizes.md) · [Companheiros e bots](companheiros-e-bots.md) · [Remoção das deusas](remocao-das-deusas.md) · [Fontes](../fontes.md)

**Estudo de 15/11/2026.** Esta página organiza o que é preciso para transformar as decisões da wiki em um servidor próprio. Nada foi instalado, compilado ou testado. A base técnica foi inspecionada só pela leitura do código.

## Base técnica candidata: Melia

O [Melia](https://github.com/NoCode-NoLife/melia) é um servidor de código aberto para Tree of Savior. Ele faz o papel que o rAthena cumpre no projeto de Ragnarok do proprietário. [F124](../fontes.md#f124)

| Ponto | Situação |
| --- | --- |
| Linguagem e banco | C# (.NET 8+) com MySQL ou MariaDB. |
| Licença | GPL-3.0. Mudanças no servidor podem ser feitas; se o servidor modificado for distribuído, o código precisa acompanhar. |
| Cliente | Compatível com o **cliente internacional atual da Steam**, que é gratuito. O Melia não distribui o cliente. |
| Situação do projeto | Ativo (último commit inspecionado: 19/05/2026), mas o próprio projeto diz que ainda falta para ser plenamente jogável. Funcionam personagens, inventário, chat, atributos, habilidades, combate e IA básica de monstros, NPCs, lojas, monstros no mapa e missões. |

## A decisão mais importante: qual cliente

O Melia segue o **cliente atual**, posterior ao Re:Build de 2018. O mantenedor explica que um cliente antigo perde a possibilidade de capturar pacotes dos servidores oficiais, o que torna o trabalho muito mais difícil. [F125](../fontes.md#f125)

| Caminho | Vantagens | Problemas |
| --- | --- | --- |
| **Cliente atual + regras no servidor (recomendado)** | É o caminho do Melia. A árvore de Scout já existe no cliente. Recursos do lançamento podem ser religados no servidor. | A interface e os dados são do jogo posterior. É preciso conferir se o cliente mostra círculos e ranks como queremos. |
| Cliente de 2016 | Visual e sistema mais próximos do recorte histórico. | Não há servidores oficiais para captura de pacotes; seria preciso refazer a comunicação. Obter o cliente da época também é incerto. |

## O que o Melia já oferece para o projeto

| Decisão do projeto | Onde fica no Melia | Situação |
| --- | --- | --- |
| 39 classes da seleção solo | `system/db/jobs.txt` | **As 39 existem.** Os ranks seguem o jogo posterior (por exemplo, Templar e Paladin como rank 2) e precisam ser ajustados à nossa tabela. |
| Ranks e círculos | `JobComponent` (código) e `system/db/exp.txt` | O código já tem círculos de classe. Falta conferir como o cliente atual exibe isso. |
| Remover classes mágicas | `system/db/features.txt` e `jobs.txt` | Há chaves para ligar e desligar recursos. A remoção de classes precisa ser configurada ou programada. |
| Kit base de Scout | `system/db/skilltree.txt`, `skills.txt` e manipuladores de habilidades em `src/ZoneServer/Skills/Handlers/Scouts` | Dados e código de habilidades de Scout já existem. É preciso conferir cada habilidade do [kit ratificado](scout-kit-base.md). |
| Atributos como em 2016 | `features.txt` (`NoStatByLevel`, `IncreasedStatRatio`) | Chaves do Re:Build podem ser desligadas para voltar aos pontos de atributo por nível. Comportamento não testado. |
| Poções e itens | `system/db/items.txt` | Itens originais disponíveis como dados. |
| Retorno após derrota | `system/db/resurrection_points.txt` | Os pontos ficam nas cidades (Klaipeda, Fedimian e outras), não nas estátuas. Isso facilita a [remoção das deusas](remocao-das-deusas.md). |
| Estátuas e teletransporte | Scripts de conteúdo em `system/scripts/zone` | É preciso localizar e substituir as estátuas nos scripts dos mapas. |

## O que precisa ser construído

| Peça | Situação no Melia | Tamanho do trabalho |
| --- | --- | --- |
| **Grupo de 4** | Não encontrei um sistema de grupo no código inspecionado. | Grande: é pré-requisito dos bots. |
| **Bots aliados** | Não há companheiros nem IA de aliados; a IA de scripts é só de monstros (`basic_monster.cs`). | O maior trabalho do projeto. Começa depois do grupo. |
| Animais de Hunter e Falconer | Não encontrei sistema de companheiros. | Depende da decisão sobre animais. |
| Carroças no lugar das estátuas | Não existe. | Médio: depende da arte disponível no cliente. |
| Habilidades adaptadas sem magia | Parte das habilidades já tem código. | Classe por classe, conforme as fichas. |

## Ordem sugerida

| Fase | O que fazer | Precisa de computador? |
| --- | --- | --- |
| 0 · Dados de design | Converter as decisões da wiki em tabelas no formato do Melia: classes por rank, círculos, kit de Scout e progressão. | **Não.** Pode ser feito agora, no repositório. |
| 1 · Servidor original | Instalar MariaDB, .NET 8 e o cliente da Steam; rodar o Melia sem mudanças e entrar no jogo. | Sim. |
| 2 · Época e seleção | Ajustar as chaves de recursos e os dados de classes para a seleção solo. | Sim. |
| 3 · Progressão | Aplicar ranks, círculos, experiência e atributos do balanceamento. | Sim. |
| 4 · Grupo | Implementar ou completar o grupo de 4. | Sim. |
| 5 · Bots | Personagens do barracão e do servidor como aliados, com posturas e ordens. | Sim. |
| 6 · Mundo sem deusas | Trocar estátuas por transporte e revisar textos. | Sim. |

## Cuidados

- **Cliente e arte pertencem à IMC Games.** O servidor deve ficar privado ou local, sem redistribuir arquivos do cliente.
- **O cliente da Steam atualiza sozinho.** Uma atualização pode quebrar a compatibilidade com o Melia; vale manter uma cópia do cliente que funciona.
- **O Melia está incompleto.** Partes do jogo podem não funcionar, e o projeto terá de acompanhar as atualizações dele ou manter uma versão própria.

A fase 0 começou com a [tabela de classes e o esqueleto de progressão](classes-e-progressao-servidor.md).

**Próximo passo recomendado:** definir os pontos de atributo e de habilidade por nível.
