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
| **Itens** | `user/db/items.txt` | 1.529 itens com ataque, defesa, nível necessário e peso de 2016 |
| **Habilidades** | `user/db/skills.txt` e `calc_skill.cs` | 290 habilidades com ataque fixo por nível, fator 100% e recarga de 2016; custo de SP por nível (tabela de 316 habilidades) |
| **Configuração** | `user/conf/world/jobs.conf` e `exp.conf` | rank máximo 6; taxas de experiência 100% |
| **Experiência** | nada | a tabela de experiência base do Melia **já é a de 2016** |

## Como instalar (quando houver computador)

1. Instale o .NET 8 SDK e o MariaDB, e prepare o Melia como no README dele (banco de dados, `user/conf/database.conf`).
2. Copie a pasta `user/` deste pacote **por cima** da pasta `user/` do Melia. O Melia carrega `system/` primeiro e `user/` por cima, então nada em `system/` muda.
3. Compile e inicie o servidor como de costume. Se a compilação falhar em `Send.cs` com "chamada ambígua de string.Split", troque `Split(['#'], ...)` por `Split(new[] { '#' }, ...)` (e igual para `','`): é um problema do Melia com algumas versões do compilador, não do pacote.
4. Para **conferir se os dados carregam**, copie `tools/DataCheck` para a raiz do Melia e rode `dotnet run -c Release --project tools/DataCheck`. Ele carrega os dados como o servidor, mostra avisos e faz 11 conferências de valores de 2016. O resultado esperado é "0 avisos, 0 falhas".

## O que foi conferido

- Os arquivos `user/db/*.txt` são lidos pelo próprio carregador do Melia **sem nenhum aviso**.
- Os scripts `calc_character.cs` e `calc_skill.cs` **compilam** com o Melia.
- 11 conferências de valores de 2016 passam: Q2 Minotaur (nível 116, HP 64.094, defesa 163, ataque 331 a 416, 7 drops), Old Gladius (ataque 4 a 5), Swordsman (STR 7, CON 8, taxa de HP 3,3), Thrust (ataque fixo 56, +14,93 por nível), tabela de experiência (nível 2 = 8, nível 3 = 272), bônus por investimento (50 = 10, 107 = 24, 150 = 35, 300 = 85, 500 = 185, 600 = 285) e SP da Thrust (8 a 24).

## O que **não** está pronto

- **Nunca foi jogado.** Nem o servidor nem as fórmulas rodaram com um personagem de verdade.
- **Cliente:** o Melia só funciona com o cliente atual da Steam. A interface, os gráficos e as telas de classe são os de hoje, e o cliente pode mostrar textos e números do jogo atual (descrições de habilidades, por exemplo), mesmo que o servidor calcule os de 2016.
- **Habilidades:** o Melia só implementa em combate parte das habilidades. As que não têm código no servidor não funcionam, mesmo com os números de 2016.
- **Quests:** o Melia quase não tem quests. As 1.826 quests de 2016 estão na wiki, mas ainda não viraram scripts.
- **Fórmula de dano:** não existe fonte. O pacote mantém o cálculo do Melia (ataque + ataque fixo da habilidade, menos defesa, vezes o fator), que parece ser o desenho original, e os tipos de dano contra armadura da era antiga. É hipótese.
- **Monstros:** a velocidade, a IA e as habilidades continuam as do Melia. O bloqueio dos monstros foi zerado (2016 não mostra). Drops com chance desconhecida em 2016 (644) usam a chance atual do Melia ou 1%. Quantidades de drop e silver continuam as do Melia.
- **Itens:** só ataque, defesa, nível e peso. Potencial, durabilidade, refino de 2016, gemas e conjuntos não foram portados.
- **Habilidades sem equivalente** no Melia: 68. Monstros: 16. Itens: 9.
- **Classes:** só as quatro bases de 2016 foram trocadas. As demais classes usam a taxa de HP e SP atual.
- **Ataque fixo das habilidades:** o Melia só guarda valor inicial e crescimento linear; as tabelas de 2016 têm pequenas irregularidades (erro de até 1 ponto).

## Como regenerar

```
python3 scripts/build-server-pack-2016.py CAMINHO/DO/MELIA server/original-2016
```

O gerador lê `data/research/tosbase-2016-*.json` e o `system/` do Melia (commit 67e93ee, de 19/05/2026) e escreve esta pasta de novo. Os arquivos `user/db` contêm linhas do banco do Melia modificadas, então seguem a licença GPL-3.0 do Melia. Fontes: [F142](../../docs/fontes.md), [F144](../../docs/fontes.md), [F145](../../docs/fontes.md).
