# 2016, hoje e o nosso sistema

[Índice do projeto](indice.md) · [Resumo do balanceamento](resumo-do-balanceamento.md) · [Todas as fórmulas](todas-as-formulas.md) · [Simulador de builds](simulador-de-builds.md) · [Fontes](../fontes.md)

**Comparativo de 17/11/2026.** Uma página só para ver, assunto por assunto, **como era em 2016**, **como é hoje** (o servidor Melia, que reconstrói o jogo depois do Re:Build) e **o que fizemos no nosso sistema**. A última coluna diz onde seguimos o original, onde mantivemos o de hoje e onde criamos algo novo.

**Como ler os selos**

- **Volta ao original:** o projeto escolheu a regra de 2016.
- **Mantém o de hoje:** vale o que o Melia já faz.
- **Novo:** regra criada pelo projeto, que não existia em nenhuma das duas épocas.
- **Pendente:** falta decisão ou fonte.

**Sobre as fontes de 2016:** a maior parte é de relatos de jogadores do beta de 2015 e de 2016 e de capturas do ToSBase de março a abril de 2016. Quando o número de 2016 não existe, a tabela diz "sem fonte". O Melia vem de [F124](../fontes.md#f124).

## 1. Visão geral, assunto por assunto

| Assunto | Em 2016 | Hoje (Melia) | Nosso sistema | Selo |
| --- | --- | --- | --- | --- |
| Classes | Sequência de ranks; a mesma classe repetia círculos (C1 a C3). [F01](../fontes.md#f01) | Uma base e três classes de avanço (Re:Build, 2019). [F12](../fontes.md#f12) | Ranks de R1 a R7 e círculos até C3, em três bases (Warrior, Archer e Scout). [Ranks](ranks-e-especializacoes.md) | Volta ao original |
| Nível máximo | 280 no acesso antecipado. [F126](../fontes.md#f126) | Tabela de experiência até o nível 550. | 280. | Volta ao original |
| Nível de classe | 15 níveis de classe por rank. | Base até 15; avanço até 45 no guia atual. | 15 por rank, 105 até o R7. | Volta ao original |
| Pontos de atributo | 1 por nível, mais extras (história, revelações, estátuas). Só relato do beta de 2015. [F129](../fontes.md#f129) | Distribuição automática; a chave `NoStatByLevel` vem ligada. | 1 por nível, 279 até o 280, distribuídos à mão; extras realocados. [Pontos](pontos-por-nivel.md) | Volta ao original |
| Bônus por investimento | +1 a cada 5 pontos até 50, a cada 4 até 150, a cada 3 até 300. [F130](../fontes.md#f130) | Termos de +1 a cada 10 ou 15 pontos nas fórmulas de ataque, HP, precisão e esquiva. | Escala de 2016 mantida. | Volta ao original |
| Atributos | INT para magos, SPR para SP e cura. CON sem efeito na defesa (nos dados do Melia). | Idem. | INT vira potência de **preparados**; SPR vira **Sabedoria**; CON soma +2 de defesa por ponto. | Novo |
| Dano das habilidades | Valor de "ataque" fixo por nível (Thrust: 56 no nível 1, 265 no 15), não percentual. | Fator percentual: Thrust 1.678% no nível 1, +252% por nível. | Fator do Melia ÷ 10; cada nível soma 10% do valor do nível 1. | Novo |
| Defesa | Fórmula de 2016 sem fonte. A reforma de dano de maio de 2017 mudou isso. [F14](../fontes.md#f14) | Subtrai a defesa do dano (mínimo 1). | Corta uma fração: defesa ÷ (defesa + ataque), máximo de 75%. | Novo |
| Crítico | Sem fonte para a DEX em 2016. | Taxa = nível ÷ 2; a DEX só aumenta o dano do crítico. | Igual ao Melia por enquanto. | Pendente |
| Recuperação | Poções proporcionais ao HP (5% a 10%, recarga de ~30 s); sentar e fogueira entre as lutas. [F135](../fontes.md#f135) a [F139](../fontes.md#f139) | 1% do HP a cada 20 s; sentado, o dobro e o intervalo pela metade. Poções de cura fixa. | Ciclo de 2016: 10% a cada 3 s sentado, +10% com fogueira, poções proporcionais. [Ciclo](ciclo-de-recuperacao.md) | Volta ao original |
| Silver | Muito silver por hora (relato de ~200 mil), gasto em poções, reparos e atributos de classe. [F131](../fontes.md#f131) | 7 a 15 mil por hora, só do silver direto. | Em avaliação: talvez sair do jogo. [Economia](economia-das-pocoes.md) | Pendente |
| Experiência | Bônus de equipe de até 49% (nível 50), segundo citação de 2016. [F117](../fontes.md#f117) | Tabela e taxa do servidor. | ~150 horas até o 280, experiência dos monstros +50%, dividida igualmente no grupo. | Novo |
| Grupo | Quatro vagas de barracão; animais ocupam vagas. [F122](../fontes.md#f122) | Grupo de jogadores. | Jogador mais até 3 bots, grupo de 4. | Novo |
| Monstros | Sem tabela de 2016. | Valores fixos por monstro. | Multiplicadores de HP, ataque, defesa e precisão por faixa de nível. | Novo |
| História | Deusas, demônios, Árvore explicada. | Idem. | Sem deusas e sem demônios; reino rival; Árvore sem explicação. | Novo |

## 2. As habilidades do Warrior: 2016 contra hoje

O Warrior parte do Swordsman, a única classe de 2016 que tem tabelas por nível na wiki (capturas do ToSBase de 31/03 a 15/04/2016, [F18](../fontes.md#f18) a [F25](../fontes.md#f25)). **Para Archer e Scout ainda não temos tabelas de 2016.** O ambiente de pesquisa está sem acesso ao Internet Archive agora.

O "ataque exibido" de 2016 é um número fixo por nível, não um percentual. Por isso a comparação mais justa é **quanto a habilidade cresce do nível 1 ao último nível**.

### Crescimento por nível de habilidade

| Habilidade | 2016 | Hoje (Melia) | Nosso (10% por nível) |
| --- | --- | --- | --- |
| Thrust (nível 1 → 15) | 56 → 265 (4,7 vezes) | 1.678% → 5.206% (3,1 vezes) | 168% → 403% (2,4 vezes) |
| Bash (nível 1 → 15) | 54 → 298 (5,5 vezes) | 1.835% → 11.468% (6,3 vezes) | 183% → 440% (2,4 vezes) |
| Pommel Beat (nível 1 → 10) | 118 → 374 (3,2 vezes) | Só o valor provisório de 100% | Sem dado |
| Double Slash (nível 1 → 5) | 253 → 530 (2,1 vezes) | Só o valor provisório de 100% | Sem dado |

### Custo de SP e recarga

| Habilidade | SP em 2016 (nível 1 → último) | SP no Melia (base) | Recarga em 2016 | Recarga no Melia |
| --- | --- | --- | --- | --- |
| Thrust | 8 → 24 | 10 | nenhuma | 1 s |
| Bash | 8 → 27 | 49 | 6 s | 10 s |
| Gung Ho | 7 → 26 | 168 | 40 s | 60 s |
| Concentrate | 11 → 30 | sem dado | 20 s | 20 s |
| Pain Barrier | 27 → 74 | 134 | 39 s | 30 s |
| Restrain | 14 → 29 | sem dado | 120 s | 120 s |
| Pommel Beat | 13 → 29 | sem dado | 14 s | 14 s |
| Double Slash | 15 → 23 | sem dado | 21 s | 21 s |

No Melia, o custo base ainda é corrigido pelo nível do personagem (no nível 120, Thrust custa 5 SP, Bash 24 e Gung Ho 85).

## 3. O que isso mostra

1. **As habilidades crescem menos no nosso sistema do que em 2016.** Em 2016, subir a Thrust do nível 1 ao 15 multiplicava o dano por 4,7; no nosso sistema, por 2,4. Gastar pontos de habilidade vale menos do que valia. A regra de 10% por nível foi escolhida para simplificar a calibragem, mas ela achata as diferenças entre habilidades.
2. **Duas habilidades do Warrior nunca terão dano no Melia.** Pommel Beat e Double Slash só têm um valor provisório lá, mas temos os números reais de 2016. Dá para usá-los como régua: Pommel Beat vale cerca de 2,1 vezes a Thrust no nível 1, e Double Slash cerca de 4,5 vezes.
3. **As recargas do Melia são mais longas que as de 2016** nas três habilidades de uso mais frequente: Thrust (nenhuma contra 1 s), Bash (6 s contra 10 s) e Gung Ho (40 s contra 60 s). Isso deixa o ritmo de combate mais lento do que o original.
4. **O SP quase não pesa.** Em 2016 o custo subia com o nível da habilidade (a Thrust ia de 8 a 24 SP). No Melia o custo base é corrigido pelo nível do personagem e fica baixo. O simulador mostra de 7 a mais de 110 monstros por SP cheio, conforme a habilidade (só as de recarga longa e muito SP, como Multi Shot, pesam); no uso corrente o SP deixa de ser um recurso a gerir.
5. **Faltam os dados de Archer e Scout de 2016.** Sem eles, não dá para dizer se o kit fraco do Scout (Dagger Slash e Oblique Fire) vem do Melia ou do original.

## 4. O que podemos fazer

| # | Opção | Efeito | Custo |
| --- | --- | --- | --- |
| A | **Crescimento por habilidade em vez de uniforme.** Usar a proporção de 2016 (ou do Melia) para cada habilidade, mantendo o fator do nível 1 dividido por 10. | Subir habilidades volta a importar; Bash cresce mais que Thrust, como no original. | Muda a calibragem dos monstros, que supõe o nível 5 com 211%. |
| B | **Recargas de 2016** nas habilidades do Warrior (Thrust sem recarga, Bash 6 s, Gung Ho 40 s). | Combate mais rápido e mais parecido com o original. | Precisa ver o ritmo de combate; hoje a calibragem supõe uma habilidade a cada 2 s. |
| C | **Custo de SP de 2016** (cresce com o nível da habilidade) no lugar do corrigido pelo nível do personagem. | O SP volta a ser um recurso; os descansos ganham mais peso. | Precisa de teste; o SP máximo é o do Melia. |
| D | **Fatores de Pommel Beat e Double Slash pela régua de 2016.** | O Warrior ganha duas habilidades de dano que o Melia não tem. | Pequeno. |
| E | **Pesquisar Archer e Scout em 2016** e fazer a mesma tabela. | Diz se o Scout fraco é herança do Melia. | Precisa de acesso ao Internet Archive, hoje bloqueado aqui. |

**Recomendação:** começar por **D** e **A** (não dependem de teste e corrigem o que o comparativo mostrou), deixar **B** e **C** para o teste no servidor, e tentar **E** em seguida.

**Próximo passo recomendado:** escolher entre as opções acima. Se você disser "pode seguir com D e A", eu aplico no simulador e nos dados, recalculo a comparação das bases e mostro o efeito nos números.
