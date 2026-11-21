# Roteiro de teste do pacote "original 2016"

Para a primeira sessão com o computador. Tudo aqui foi validado só por compilação, execução de código isolado e um MariaDB de teste; **nada foi jogado**. O objetivo da sessão é descobrir o que diverge. Anote cada resultado na tabela do fim: "ok", "diferente (o que apareceu)" ou "não deu para testar".

Tempo estimado: 30 min de instalação, 1 h de teste. Se travar numa etapa, siga para a próxima: as etapas 5 a 10 são independentes entre si.

## 0. O que levar

- Windows com o cliente da Steam de Tree of Savior (o Melia só funciona com o cliente atual).
- .NET 8 SDK, MariaDB (ou MySQL), Git.
- Esta pasta (`server/original-2016/`) e o repositório do Melia no commit `67e93ee` (`git clone https://github.com/NoCode-NoLife/melia` e `git checkout 67e93ee`).

## 1. Instalar

1. Banco: crie o banco rodando `sql/main.sql` do Melia. Crie `user/conf/database.conf` com `user : ...` e `pass : ...`.
2. Na raiz do Melia: `git apply CAMINHO/server/original-2016/patches/0001-propriedades-de-item.patch`. O comando **não pode dar erro**. Se der, pare e me mande a mensagem.
3. Copie a pasta `server/original-2016/user/` **por cima** de `user/` do Melia.
4. Compile: `dotnet build -c Release`. Se falhar em `Send.cs` com "chamada ambígua de string.Split", troque `Split(['#'], ...)` por `Split(new[] { '#' }, ...)` (e o mesmo para `','`). É um problema do Melia, não do pacote.
5. Copie `server/original-2016/tools/DataCheck` e `tools/ScriptCheck` para `tools/` na raiz do Melia.

## 2. Conferências automáticas (antes de abrir o jogo)

Na raiz do Melia:

```
dotnet run -c Release --project tools/DataCheck
dotnet run -c Release --project tools/ScriptCheck -- CAMINHO/server/original-2016/quests-report.json CAMINHO/data/research/tosbase-2016-items.json
```

- **DataCheck esperado:** `0 avisos, 0 falhas` (13 conferências).
- **ScriptCheck esperado:** exit code 0, "22515 níveis conferidos, 0 diferenças", 8 linhas `OK` dos conjuntos, "ida e volta e cópia: True". A linha "banco: pulado" é normal; para testar o banco, defina `MELIA_TEST_DB=127.0.0.1:3306:usuario:senha:melia` (grava itens no armazém do personagem de id 1, então use um banco de teste com esse personagem).
- **Se falhar:** copie as linhas `FALHA` / `AVISO` / `error` e siga adiante só depois de me enviar.

## 3. Subir o servidor e olhar o log

Inicie os servidores (`start-all.bat`). No console da zona (ou no log), procure:

- erros de **compilação de scripts** (`user/scripts/...`): qualquer erro aqui vale mais que o resto do roteiro;
- erro ao carregar `user/db/*.txt`;
- confirmação de que a atualização `update_2026-11-19_1.sql` foi aplicada (a tabela `items` deve ter a coluna `properties`).

**Se os scripts não compilarem**, ache o culpado removendo, um por vez (reiniciando): a pasta `user/scripts/zone/content` (spawns); `user/scripts/zone/custom/original2016/quests.cs`. O `refine.cs`, o `sets.cs` e o `core/calc_character.cs` andam **juntos** (o cálculo de status chama os dois): para tirá-los, apague os três.

## 4. Conta, personagem e GM

1. No login do cliente digite `new//nomedaconta` e a senha (cria a conta).
2. No console do servidor de login: `auth nomedaconta 99` (feche o jogo antes).
3. Crie um **Swordsman**. Comandos usam o prefixo `>` no chat: `>item`, `>silver`, `>levelup`, `>warp`, `>iteminfo`, `>monsterinfo`, `>heal`, `>clearinv`. Para recarregar scripts sem reiniciar: `>reloadscripts`.

## 5. Status do personagem (fórmulas)

**Desequipe tudo** (o equipamento inicial muda os números). Valores calculados à mão pelas fórmulas do pacote, Swordsman nível 1, sem pontos distribuídos:

| Estatística | Esperado |
| --- | --- |
| STR / CON / SPR / INT / DEX | 8 / 9 / 3 / 2 / 6 |
| HP máximo / SP máximo | **765** / **39** |
| Ataque físico (mín e máx) | 9 e 9 |
| Ataque mágico | 3 |
| Defesa física / mágica | 0 / 1 |
| Precisão / Esquiva | 7 / 7 |

Depois: `>levelup 99` (nível 100), ponha os **99 pontos todos em CON**. Esperado: CON **131** e HP máximo **16.688** (exemplo da calculadora do ToSBase). Confira também que os pontos disponíveis são nível − 1.

Se diferir: anote os números exatos da janela de status; as fórmulas estão em `user/scripts/zone/core/calc_character.cs` e o quadro delas em `docs/sistemas/formulas-de-status-2016.md`.

## 6. Itens: potencial, durabilidade e refino (a parte mais incerta)

1. `>item 101101` (Old Gladius) e equipe. Tooltip esperado: ataque **4 ~ 5**, potencial **3**, durabilidade **36/36**.
   - Se a durabilidade aparecer como 3600/3600, 0,36/0,36 ou 0/0, a escala está errada: troque `Item.DurabilityScale` (hoje 100) em `src/ZoneServer/World/Items/Item.cs` e recompile. Anote o que apareceu.
2. `>silver 20000`, depois `>refine info`. Esperado no chat: `Old Gladius: +0, potencial 3. Próximo nível: 100% de chance, 115 de prata.`
3. Rode `>refine` e confira a cada vez: o item mostra **+1, +2...** no cliente? O ataque mínimo/máximo na janela de status sobe 3 por nível (Old Gladius, grau 1: +1 → 3, +2 → 6, +3 → 9)?
4. Continue até o +5 (100%) e depois tente o +6 (88%): quando falhar, o item deve cair um nível e **perder 1 de potencial**. Com potencial 0, `>refine` deve recusar.
5. **Persistência:** com o item em +3 ou mais, saia do jogo e entre de novo. O +N e o potencial devem continuar.
6. **Armazém:** `>storage`, guarde e retire o item. O refino deve continuar (isto testa a correção do bug de cópia).
7. Outra peça: `>item 531101` (Light Armor), equipe e rode `>refine info top`. Esperado: nível +0, potencial do item e a chance do +1 (100%).

Se o `+N` não aparecer no item mas o ataque subir, o servidor está certo e o cliente lê outro dado: anote isso. Se nada mudar, anote a mensagem do chat.

## 7. Conjuntos

Equipe as peças e olhe a janela de status (sem outras fontes de bônus). Ids: Elkosh (Elgos) `502188`, `512188`, `522188`, `532188`.

| Peças equipadas | Esperado (níveis somados) |
| --- | --- |
| 1 | nenhum bônus |
| 2 | defesa mágica **+5** |
| 3 | defesa mágica **+15** (5 + 10) |
| 4 | defesa mágica +15 e **INT +15** |

- **Teste da suposição:** se com 3 peças a defesa mágica for +10, o jogo usa só a maior linha: troque `Original2016Sets.Cumulative` para `false` em `sets.cs` e anote. (O conjunto Ibre, `Ibre Gloves/Boots/Pants/Armor`, tem o mesmo desenho com defesa física 5 e 10.)
- **Medalha e pulseira (Bronza):** `>item 582119` (medalha) e `>item 602116` duas vezes (pulseira). Com as duas pulseiras e a medalha, defesa mágica **+10**; com uma pulseira, nada.
- O tooltip do cliente provavelmente **não** mostra o efeito do conjunto (o cliente usa os dados dele). Anote se mostrar.

## 8. Coleções

Item da coleção "West Siauliai Woods" (`642018`) e as peças: Kepa Doll `628054`, Hanaming Petal `645024`, Kepa Stem `645117`, Leaf Bug Shell `645118`, Chinency Leaf `645120`, Infrorocktor Shell `645121`, Infrorocktor Bones `645267`. Gere tudo com `>item`, use o item da coleção e tente registrar as peças na janela de coleções (se a janela não deixar registrar, anote: é limite do Melia, não do pacote). Esperado: ataque físico **+2** (em 2016; o Melia dava outro valor). Compare com a janela de status antes e depois.

## 9. Monstros e spawns

1. `>warp f_siauliai_west`. Esperado: muitos Kepa (até ~50 ao todo em dois grupos), Leaf Bug, Hanaming, Chinency (Bokchoy) e Infrorocktor; **nenhum Tree Root Crystal**.
2. `>monsterinfo` num Kepa: nível 1, HP **17**, exp 16. Mate um: ele deve **voltar em 3 a 5 segundos** (em 2016; o padrão do Melia seria 30 a 60 s).
3. `>warp f_pilgrimroad_50` (nível alto: suba com `>levelup` e use `>heal` para não morrer): bata num **Kodomor** e veja a vida: esperado **4.439** (sem o pacote seriam 129.828). Isso confirma que as sobrescritas por mapa foram desligadas.
4. Mapas "(Closed)" (por exemplo `f_katyn_10`) não têm spawn; se o `>warp` falhar neles, anote.

## 10. Quests

Com um personagem **nível 1**, as quests de nível 1 devem aparecer sozinhas no registro em poucos segundos após entrar (não há NPC). Exemplos para testar (nome, objetivo, recompensa):

| Id | Nome | Objetivo | Recompensa |
| --- | --- | --- | --- |
| 1003 | Talk to the Scout (1) | matar 4 Kepa | Lv1 EXP Card |
| 1014 | Talk to the Search Scout | matar 1 Large Kepa | Lv1 EXP Card |
| 1018 | The Road to Klaipeda (1) | matar 7 Infrorocktor | Lv1 EXP Card |
| 1033 | Sentinel's Favor (1) | matar 7 Chupacabra | Lv1 EXP Card |

Esperado: ao cumprir o objetivo, a quest **conclui sozinha em até 1 segundo** e a carta de experiência cai no inventário. Anote: aparece no registro? o contador anda? a recompensa vem? Há 55 quests no total (`quests-report.json`). Se o servidor reclamar de quest no log, copie a mensagem.

## 11. Receitas

Sem interface de fabricação a gente só confere pelo banco: `>iteminfo 920006` (Recipe - Kris). Se você achar um jeito de fabricar, o Kris pede **16 Dumaro** (`misc_Dumaro`) além da receita. Item opcional: pode pular.

## 12. Limites esperados (não são defeitos)

- Não há NPCs de 2016: sem diálogo, bigorna, lojas das cidades do pacote, quests de conversa.
- O refino é pelo comando `>refine` (a bigorna é um NPC).
- O cliente mostra textos, ícones, descrições de habilidades e tooltips do jogo atual. Habilidades sem código no Melia não funcionam.
- Durabilidade não diminui (as regras de desgaste de 2016 não foram encontradas).
- 27 efeitos de conjunto de combate, a amplificação mágica do refino e as quests de conversa ainda não existem.

## 13. Se algo der muito errado

Desfaça por partes e anote qual parte causou o problema:

1. Tire `user/scripts/zone/core/calc_character.cs` + `custom/original2016` (cálculo, quests, refino e conjuntos juntos): volta aos status do Melia.
2. Tire `user/scripts/zone/content`: volta aos spawns do Melia.
3. Tire `user/db`: volta aos dados do Melia.
4. Para desfazer o patch: `git apply -R CAMINHO/patches/0001-propriedades-de-item.patch`. A coluna extra no banco não atrapalha.

## Folha de resultados

Copie, preencha e me mande (pode ser uma foto da janela de status nos itens 5, 6 e 7):

| Etapa | Resultado | O que apareceu, se diferente |
| --- | --- | --- |
| 1 Instalar (patch, build) | | |
| 2 DataCheck / ScriptCheck | | |
| 3 Log do servidor (compilação) | | |
| 5 Status nível 1 e HP nível 100 | | |
| 6 Item: potencial 3, durabilidade 36/36 | | |
| 6 Refino: +N, ataque, falha, persistência, armazém | | |
| 7 Conjuntos (somam ou só a maior linha) | | |
| 8 Coleção +2 de ataque | | |
| 9 Spawns, respawn de 3 s, Kodomor com 4.439 | | |
| 10 Quests 1003, 1014, 1018 | | |
