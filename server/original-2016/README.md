# Pacote "original 2016" para o servidor Melia

Faz o Melia (que reconstrói o jogo atual) se comportar como o jogo da primavera de 2016, com os dados e as fórmulas recuperados do ToSBase. É a **base de comparação** do projeto: primeiro jogar o original, depois mudar o que quisermos.

**Estado:** versão 0.1, **validada só no papel e no carregamento de dados** (ver "O que foi conferido"). Ainda não foi jogada: falta um computador com o Melia rodando e o cliente da Steam.

## O que muda

| O quê | Como | Quanto |
| --- | --- | --- |
| **Era do jogo** | `user/db/features.txt` desliga o Re:Build e tudo o que veio depois, e deixa as eras Base, iCBT2 e Release | volta ao sistema de ranks, pontos de atributo por nível, atributos livres, bloqueio e bônus de tipo de ataque antigos |
| **Classes base** | `user/db/jobs.txt` | Swordsman, Wizard, Archer e Cleric com os atributos e as taxas de HP e SP de 2016 |
| **Fórmulas de status** | `user/scripts/zone/core/calc_character.cs` | HP, SP, recuperações, ataque, defesa, precisão, esquiva, crítico, penetração de bloqueio, estamina e peso de 2016; bônus por investimento sobre a soma classe + pontos; +10% de STR e INT por rank |
| **Monstros** | `user/db/monsters.txt` | 1.126 monstros com nível, HP, ataque, defesa, precisão, esquiva, crítico, experiência, elemento, raça, armadura, tamanho, tipo e drops de 2016 |
| **Itens** | `user/db/items.txt` | 2191 itens com ataque, defesa, nível necessário e peso de 2016, mais **grau, potencial e durabilidade máximos** (campos novos `grade`, `potential`, `durability`) |
| **Habilidades** | `user/db/skills.txt` e `calc_skill.cs` | 290 habilidades com ataque fixo por nível, fator 100% e recarga de 2016; custo de SP por nível (tabela de 316 habilidades) |
| **Configuração** | `user/conf/world/jobs.conf` e `exp.conf` | rank máximo 6; taxas de experiência 100% |
| **Quests** | `user/scripts/zone/custom/original2016/quests.cs` | 55 quests de 2016 que dá para cumprir hoje (matar monstros que nascem no Melia, juntar itens que esses monstros dropam): com nível exigido, quests anteriores e itens de recompensa. Chegam sozinhas ao alcançar o nível e se concluem sozinhas |
| **Refino** | `user/scripts/zone/custom/original2016/refine.cs` | regras de refino de 2016 para 1501 itens: bônus por nível, chance de sucesso, preço (15 níveis) e a regra de potencial. Conferido contra 22.515 níveis do ToSBase, sem diferenças |
| **Conjuntos** | `user/scripts/zone/custom/original2016/sets.cs` | 33 conjuntos de equipamento de 2016, com os efeitos por quantidade de peças que são um número somado a um atributo (42 de 69): defesa, HP, status, resistência a elemento, recuperação, estamina, velocidade, esquiva, taxa crítica e taxa de AoE. Entram no cálculo de status, então equipar ou tirar uma peça já recalcula |
| **Receitas e coleções** | `user/db/recipes.txt` e `collections.txt` | 54 receitas com os materiais de 2016 (as outras 613 já eram iguais) e 142 coleções com os itens exigidos e o bônus de 2016 |
| **Patch do Melia** | `patches/0001-propriedades-de-item.patch` | o Melia passa a ler `grade`, `potential` e `durability` dos itens, define PR, MaxDur e Dur ao criar o item, salva e carrega o refino, o potencial e a durabilidade (coluna `properties` em `items`) e **corrige a cópia de item**, que apagava as propriedades ao guardar no baú |
| **Comando >refine** | `refine.cs` | `>refine` refina a arma equipada com as regras de 2016 (chance, preço em prata, falha tira 1 nível e 1 de potencial); `>refine info` mostra a situação; aceita outra peça (`rh`, `lh`, `top`, `pants`, `gloves`, `boots`, `neck`, `bracelet1`, `bracelet2`). O bônus entra no ataque, ataque mágico, defesa, defesa mágica, precisão e esquiva do personagem |
| **Spawns** | `user/scripts/zone/content/r1/mobs/**` (143 arquivos) | tempo de nascimento de 2016 em 513 spawners (o Melia usaria 30 a 60 s); remoção de 138 spawners de cristal de raiz que o ToSBase de 2016 não lista; remoção de 424 sobrescritas de atributos por mapa (vida e ataque do jogo atual, dezenas de vezes maiores), para valerem os monstros de 2016 |
| **Experiência** | nada | a tabela de experiência base do Melia **já é a de 2016** |

**Primeiro teste com o computador:** siga o [roteiro de teste](ROTEIRO-DE-TESTE.md), que traz o que conferir em cada etapa, os valores esperados e o que fazer se algo divergir.

## Como instalar (quando houver computador)

1. Instale o .NET 8 SDK e o MariaDB, e prepare o Melia como no README dele (banco de dados, `user/conf/database.conf`).
2. Na raiz do Melia (commit 67e93ee), aplique o patch: `git apply CAMINHO/patches/0001-propriedades-de-item.patch`. Ele faz o Melia guardar o refino, o potencial e a durabilidade de cada item (nova coluna `properties` na tabela `items`; o servidor Barracks aplica o SQL sozinho ao iniciar). Sem o patch o pacote funciona, mas o refino se perde ao sair do jogo.
3. Copie a pasta `user/` deste pacote **por cima** da pasta `user/` do Melia. O Melia carrega `system/` primeiro e `user/` por cima, então nada em `system/` muda.
4. Compile e inicie o servidor como de costume. Se a compilação falhar em `Send.cs` com "chamada ambígua de string.Split", troque `Split(['#'], ...)` por `Split(new[] { '#' }, ...)` (e igual para `','`): é um problema do Melia com algumas versões do compilador, não do pacote.
5. Para **conferir os scripts** (quests, spawns e refino compilam, o refino reproduz o ToSBase e o patch grava e lê os itens), copie também `tools/ScriptCheck` para a raiz do Melia e rode `dotnet run -c Release --project tools/ScriptCheck -- CAMINHO/quests-report.json CAMINHO/tosbase-2016-items.json`. Com a variável `MELIA_TEST_DB=host:porta:usuario:senha:banco` ele também grava e lê itens num banco de teste.
6. Para **conferir se os dados carregam**, copie `tools/DataCheck` para a raiz do Melia e rode `dotnet run -c Release --project tools/DataCheck`. Ele carrega os dados como o servidor, mostra avisos e faz 13 conferências de valores de 2016. O resultado esperado é "0 avisos, 0 falhas".

## O que foi conferido

- Os arquivos `user/db/*.txt` são lidos pelo próprio carregador do Melia **sem nenhum aviso**.
- Os scripts `calc_character.cs` e `calc_skill.cs` **compilam** com o Melia.
- 11 conferências de valores de 2016 passam: Q2 Minotaur (nível 116, HP 64.094, defesa 163, ataque 331 a 416, 7 drops), Old Gladius (ataque 4 a 5), Swordsman (STR 7, CON 8, taxa de HP 3,3), Thrust (ataque fixo 56, +14,93 por nível), tabela de experiência (nível 2 = 8, nível 3 = 272), bônus por investimento (50 = 10, 107 = 24, 150 = 35, 300 = 85, 500 = 185, 600 = 285) e SP da Thrust (8 a 24).

## O que **não** está pronto

- **Nunca foi jogado.** Nem o servidor nem as fórmulas rodaram com um personagem de verdade.
- **Cliente:** o Melia só funciona com o cliente atual da Steam. A interface, os gráficos e as telas de classe são os de hoje, e o cliente pode mostrar textos e números do jogo atual (descrições de habilidades, por exemplo), mesmo que o servidor calcule os de 2016.
- **Habilidades:** o Melia só implementa em combate parte das habilidades. As que não têm código no servidor não funcionam, mesmo com os números de 2016.
- **Quests:** só 55 das 1830 quests de 2016 viraram script e são cumpríveis hoje. Das outras: 991 são só conversa com NPC; 203 têm passos de falar, investigar ou ir a um lugar; 138 têm objetivo sem monstro ou item identificável; 29 são avanços de classe (nível 9999 no ToSBase, dependem de NPC); 158 pedem um monstro que **não nasce** no Melia (aparece só dentro da quest ou falta no conteúdo) e 256 pedem um item que nenhum monstro que nasce dropa (o drop de item de quest fica no script da quest, que o ToSBase não mostra). O Melia exige NPCs com coordenadas e **só posiciona 9 NPCs**, então o cruzamento com os 335 NPCs iniciais das quests rende só 11 quests com NPC. Todos os 335 têm modelo no banco de monstros do Melia; faltam as coordenadas. Sem experiência nem prata direta. 57 quests têm recompensa de escolha, que o Melia não suporta. Pré-requisitos de quests não convertidas são ignorados (20). Lista completa em `quests-report.json`.
- **Spawns:** o Melia já traz os pontos de nascimento e as populações dos mapas (a população máxima dos spawners é a mesma que o ToSBase mostra para 2016), então o pacote ajusta o tempo de nascimento, tira os cristais de raiz e **desliga as sobrescritas de atributos por mapa** (sem isso, em 85 dos 147 mapas, os monstros teriam 30 vezes mais vida que em 2016: por exemplo, o Kodomor de Pilgrim Road 50 teria 129.828 de vida em vez dos 4.439 de 2016). Dos 622 monstros que o ToSBase lista em 147 mapas, 530 têm spawner no Melia; 34 mapas do ToSBase estão **fechados** no Melia (aparecem como "(Closed)" e não têm spawner). Os monstros que faltam são na maioria objetos de mapa (Stone, Box, Bonfire). 74 spawners tiveram tempo ambíguo no ToSBase (a mesma população com tempos diferentes) e ficaram como estavam. Lista por mapa em `spawns-report.json`.
- **Fórmula de dano:** não existe fonte. O pacote mantém o cálculo do Melia (ataque + ataque fixo da habilidade, menos defesa, vezes o fator), que parece ser o desenho original, e os tipos de dano contra armadura da era antiga. É hipótese.
- **Monstros:** a velocidade, a IA e as habilidades continuam as do Melia. O bloqueio dos monstros foi zerado (2016 não mostra). Drops com chance desconhecida em 2016 (644) usam a chance atual do Melia ou 1%. Quantidades de drop e silver continuam as do Melia.
- **Itens:** só ataque, defesa, nível e peso. Gemas e conjuntos não foram portados. O banco do Melia já traz quase todos os itens de 2016 (28.662 itens no total); só 9 do ToSBase não existem lá: Dark Sword e Master Sword (itens de teste, nível 1 com ataque de nível alto), Taoist Costume e 6 páginas sem item ("Item not found"). Eles não foram acrescentados.
- **Conjuntos:** o Melia não tem conjuntos, então o pacote traz o sistema. Os 27 efeitos de combate ficam **pendentes** (registrados com o texto original em `Original2016Sets.Pending`): ataque por elemento (Fogo, Gelo, Terra, Trevas), dano adicional de Terra, defesa contra Perfuração, Impacto e Corte, ataque contra Demônios, os que têm "chance de" ativar, o de Backstab e o do conjunto Poratore. **Duas suposições a confirmar no jogo:** (1) os níveis de peças são **somados** (com 3 peças valem os efeitos de 2 e de 3; troca-se em `Original2016Sets.Cumulative`); (2) a mesma peça em dois espaços conta duas vezes, o que fecha os conjuntos de medalha e pulseira (a página lista 2 peças e pede 3). O conjunto Zega lista 1 peça e pede 2: a lista do ToSBase parece incompleta e o efeito não ativa. Não existe conjunto 6 em 2016.
- **Receitas e coleções:** 2 receitas de 2016 ficaram sem troca por terem duas linhas no Melia para o mesmo produto. 4 coleções ficaram como no Melia por terem o bônus "Team Storage Slots" (não existe no Melia). O Melia tem 200 coleções que o ToSBase de 2016 não tem (conteúdo posterior); elas continuam no jogo. As receitas e as coleções não trazem **onde fabricar nem a chance**: o ToSBase não mostra.
- **Potencial, durabilidade e refino:** com o patch aplicado, o refino, o potencial e a durabilidade restantes de cada item **ficam salvos** (testado num MariaDB local: grava e lê pelo `ZoneDb`) e o bônus de refino entra nos status. Como a bigorna é um NPC sem coordenadas, o refino é feito pelo comando `>refine`. **Ainda não conferido no cliente:** a escala da durabilidade (o patch usa a que a documentação do pacote ZC_CHANGE_EQUIP_DURABILITY sugere: valor exibido × 100), se o cliente mostra o `+N` do refino a partir da propriedade `Reinforce_2` e se o tooltip soma o bônus por conta própria. **Durabilidade:** só o valor máximo de 2016 está nos dados; as regras de desgaste e de reparo não aparecem no ToSBase, então a durabilidade não diminui. **Amplificação mágica (MHR):** os itens que refinam esse atributo (29) têm a tabela, mas o Melia não calcula esse atributo, então o bônus não entra. 1501 itens têm tabela de refino (ATK 647, MATK 182, DEF 309, MDEF 136, precisão 89, esquiva 109, amplificação mágica 29); 195 têm bônus fora da fórmula padrão e 36 têm chance diferente (punhais, pistolas e canhões), guardados como tabelas à parte; 247 itens com tabela no ToSBase ficaram sem atributo ou grau e não entram.
- **Habilidades sem equivalente** no Melia: 68. Monstros: 16. Itens: 9.
- **Classes:** só as quatro bases de 2016 foram trocadas. As demais classes usam a taxa de HP e SP atual.
- **Ataque fixo das habilidades:** o Melia só guarda valor inicial e crescimento linear; as tabelas de 2016 têm pequenas irregularidades (erro de até 1 ponto).

## Como regenerar

```
python3 scripts/build-server-pack-2016.py CAMINHO/DO/MELIA server/original-2016
python3 scripts/build-quest-pack-2016.py server/original-2016 CAMINHO/DO/MELIA   # quests, depois dos bancos
python3 scripts/build-spawn-pack-2016.py server/original-2016 CAMINHO/DO/MELIA   # spawns
python3 scripts/build-refine-pack-2016.py server/original-2016   # regras de refino
python3 scripts/build-crafting-pack-2016.py server/original-2016 CAMINHO/DO/MELIA   # receitas e coleções
python3 scripts/build-sets-pack-2016.py server/original-2016 CAMINHO/DO/MELIA   # conjuntos (o build-server-pack-2016.py é que liga o bônus ao cálculo de status)
```

O gerador lê `data/research/tosbase-2016-*.json` e o `system/` do Melia (commit 67e93ee, de 19/05/2026) e escreve esta pasta de novo. Os arquivos `user/db` contêm linhas do banco do Melia modificadas, então seguem a licença GPL-3.0 do Melia. Fontes: [F142](../../docs/fontes.md), [F144](../../docs/fontes.md), [F145](../../docs/fontes.md).
