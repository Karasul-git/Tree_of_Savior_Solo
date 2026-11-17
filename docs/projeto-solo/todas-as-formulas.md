# Todas as fórmulas

[Fórmulas de dano](formulas-de-dano.md) · [Resumo do balanceamento](resumo-do-balanceamento.md) · [Estrutura do servidor](servidor-estrutura.md) · [Fontes](../fontes.md)

**Inventário de 17/11/2026.** Esta página lista **cada fórmula** que o servidor Melia calcula hoje (arquivos `system/scripts/zone/core/*.cs`, [F124](../fontes.md#f124)) e diz o que o projeto decidiu sobre ela. É a base do simulador de builds: o simulador só pode calcular o que está aqui.

**Limites:** são as fórmulas do Melia, que reconstrói o jogo **posterior ao Re:Build**, não as de 2016. Parte delas é preliminar, como o próprio código avisa. Nada foi executado: os números abaixo são cálculos feitos à mão a partir do código.

**Legenda da situação**

- **Decidida:** o proprietário já escolheu a regra do projeto (detalhada na página indicada).
- **Melia:** vale a fórmula do Melia, sem mudança decidida. Pode mudar na calibragem.
- **Pendente:** falta decisão, fonte de 2016 ou teste no servidor.
- **Fora:** não se aplica ao projeto (por exemplo, ligada às deusas).

## Os padrões que se repetem

As fórmulas parecem muitas, mas seguem poucos moldes. Entender os moldes basta para ler qualquer uma.

| Molde | Como é | Onde aparece |
| --- | --- | --- |
| **Base + nível + atributo + itens + efeitos** | Uma soma: valor inicial, mais um tanto por nível, mais um tanto por ponto do atributo, mais o que vem dos itens equipados e dos efeitos temporários. Os efeitos temporários têm o sufixo `_BM` (efeito) ou `_Bonus` (bônus permanente) nos nomes do código. | ataque, precisão, esquiva, defesa, HP, SP, cura |
| **Extra a cada 10 ou 15 pontos** | Um termo com ⌊atributo ÷ 10⌋ ou ⌊atributo ÷ 15⌋ soma um bônus extra a cada faixa completa. É o "bônus por investimento" que o proprietário decidiu manter. | ataque, HP, SP, precisão, esquiva, crítico |
| **Chance com expoente fracionário** | Chance = (diferença entre os dois lados)<sup>k</sup>, com k entre 0,46 e 0,7 e um teto. O expoente faz a chance subir rápido no começo e devagar depois. | errar o golpe (0,65), bloquear (0,7), crítico (0,6), velocidade pela DEX (0,46) |
| **Taxa da classe** | Cada classe tem um multiplicador de HP, SP e recuperação em `jobs.txt`. | HP máximo, SP máximo, recuperação |
| **Chave de configuração** | Um multiplicador global do servidor, ligado ou desligado sem mexer no código. | experiência, drops, fator das habilidades, limite de dano |
| **Chave de recurso (`features.txt`)** | Uma opção liga ou desliga uma regra inteira. | peso, corrida, pontos por nível, tabela de elementos |

## 1. Atributos e recursos do personagem

| Fórmula | Regra do Melia | Situação |
| --- | --- | --- |
| Ataque físico e mágico | 20 + nível + 2 × atributo + ⌊atributo ÷ 10⌋ × (nível × 0,05) + itens | **Decidida** (INT vira o caminho dos preparados) · [4.3](formulas-de-dano.md) |
| Precisão | nível ÷ 4 + STR ÷ 2 + ⌊STR ÷ 15⌋ × 3 + itens | Melia · [4.3](formulas-de-dano.md) |
| Esquiva | nível ÷ 4 + DEX ÷ 2 + ⌊DEX ÷ 15⌋ × 3 + itens | Melia · [4.6](comparacao-das-bases.md) |
| Ataque crítico | 4 × DEX + ⌊DEX ÷ 10⌋ × 10 | Melia |
| Taxa de crítico | nível ÷ 2 + itens (a DEX **não** entra) | **Pendente** (ver como era em 2016) |
| Defesa física e mágica | 20 + nível + itens; CON **não** entra no Melia | **Decidida**: CON soma defesa (+2 por ponto) e a defesa passa a ser percentual · [4.3](formulas-de-dano.md) |
| HP máximo | (400 + (nível − 1) × 80) × taxa da classe × (1 + 0,003 × CON + 0,01 × ⌊CON ÷ 10⌋) | Melia · calibrado em [4.4](calibragem-monstros.md) |
| SP máximo | (200 + (nível − 1) × 18) × taxa da classe × (1 + 0,005 × SPR + 0,015 × ⌊SPR ÷ 10⌋) | Melia (SPR vira Sabedoria) |
| Recuperação de HP | ⌊HP máximo ÷ 100 × taxa da classe⌋ a cada 20 s; o efeito de descanso dobra o valor e sentar reduz o intervalo à metade (mínimo de 1 s) | **Decidida** de outro jeito: [5.3](ciclo-de-recuperacao.md) propõe 10% a cada 3 s sentado |
| Recuperação de SP | ⌊SP máximo × 0,03 × taxa da classe⌋ a cada 20 s; sentado, o intervalo cai à metade | Melia |
| Poder de cura | 40% × (20 + 2,5 × nível + atributo) + 60% × (maior ataque médio ÷ 4), mais itens e efeitos | **Decidida**: cura de preparados = valor base × (1 + INT ÷ 100) · [5.1](cura-e-pocoes.md) |
| Pontos de atributo livres | pontos por nível + bônus − gastos; no Melia a chave `NoStatByLevel` está **ligada**, ou seja, **não dá ponto por nível** | **Decidida**: 1 ponto por nível (a chave precisa ser desligada) · [4.1](pontos-por-nivel.md) |
| Resistências a elemento | itens + efeitos, sem fórmula própria (fogo, gelo, raio, terra, veneno, sagrado, trevas, alma) | Melia |
| Estamina máxima | 25 + ⌊CON ÷ 20⌋ + itens + bônus | Melia |
| Custo de correr, pular e esquivar | corrida **grátis** (chave `FreeRunning` ligada); pulo grátis (`FreeJumping`); o impulso de corrida (Dash) gasta estamina fora das cidades | Melia |
| Recuperação de estamina | 400 por segundo; dobra sentado | Melia |
| Peso máximo | 8.000 (chave `IncreasedMaxWeight`) + 5 × STR + 5 × CON (chave `StaticInventoryWeight`), mais bônus e efeitos | Melia |
| Excesso de peso | acima do máximo, a velocidade de movimento cai a **um terço** | Melia |
| Velocidade de movimento | 30 + efeitos e bônus; zero quando o movimento está travado | Melia |
| Velocidade de conjuração | 100 + efeitos, limitada entre 10 e 200 | Melia |
| Tiro em movimento | Archer 0,8 (ou habilidades específicas); limite de 5 | Melia |
| Sorte de saque (`LootingChance`) | itens + efeitos, sem fórmula própria | Melia |

## 2. Combate: como um golpe vira dano

A ordem de cálculo está em [4.3](formulas-de-dano.md). Os pontos de cada etapa:

| Etapa | Regra do Melia | Situação |
| --- | --- | --- |
| Errar o golpe | (esquiva do alvo − precisão)<sup>0,65</sup>, teto de 80%; habilidade mágica nunca erra | Melia · conferir no servidor |
| Ataque base | sorteio entre o mínimo e o máximo do ataque | Melia |
| Defesa | o Melia **subtrai** a defesa do dano; o projeto usa fração: defesa ÷ (defesa + ataque do golpe), máximo de 75% | **Decidida** · [4.3](formulas-de-dano.md) |
| Fator da habilidade | multiplica o dano pelo percentual da habilidade | **Decidida**: fatores ÷ 10 e +10% do nível 1 por nível |
| Bloqueio | (bloqueio − quebra de bloqueio)<sup>0,7</sup>; o golpe bloqueado perde 50% do dano (70% com Alta Guarda) | Melia · conferir no servidor |
| Crítico | (taxa de crítico − resistência a crítico)<sup>0,6</sup>; o crítico soma o ataque crítico e multiplica o dano por 1,5 | Melia · **pendente** (DEX e crítico) |
| Tamanho do alvo | bônus da arma principal contra alvos pequenos, médios ou grandes; não vale para magia | Melia |
| Elemento | magia ×0,5 a ×2, conforme a tabela (chave `AttributeBonusRevamp` ligada): fogo → terra ×1,5; gelo → fogo ×1,5; raio → gelo ×2; terra → raio ×1,5; veneno → terra ×1,5; sagrado ↔ trevas ×2; mesmo elemento ×0,5 | Melia · proposta: vale também para os preparados |
| Tipo de ataque e raça | multiplicadores só para magia | Melia |
| Múltiplos golpes | a habilidade decide a quantidade de golpes; o multiplicador do cálculo só repassa esse valor | Melia |
| Limite de dano | teto global do servidor (`MaxDamageCap`) | Melia |
| Armas secundárias | soma 20% ao ataque geral e o valor inteiro nas habilidades que a usam | **Decidida** · [4.5](curva-de-equipamentos.md) |

## 3. Habilidades

| Fórmula | Regra do Melia | Situação |
| --- | --- | --- |
| Fator da habilidade | fator do nível 1 + fator por nível × (nível − 1) | **Decidida**: fator ÷ 10; cada nível soma 10% do fator do nível 1 |
| Reforço por habilidade de apoio | +0,5% por nível da habilidade de reforço, +10% extra no nível 100, mais a parte oculta de algumas | **Pendente** (decidir se as habilidades de reforço entram) |
| Ataque adicional da habilidade | valor fixo + valor por nível × (nível − 1) | Melia |
| Custo de SP | custo base × (1 + correção de nível), onde a correção é (nível − 300) × 2,75 ÷ 1.000 abaixo do 300 e × 1,25 ÷ 1.000 a partir do 300; depois, o modificador da habilidade | **Pendente**: o Melia centra a correção no nível **300** e o projeto termina no **280**; no nível 120 o custo fica em cerca de 50% do base |
| Custo de estamina | valor da habilidade × 1.000 | Melia |
| Velocidade da habilidade | taxa base + (DEX ÷ 500)<sup>0,46</sup> (se a habilidade depende da DEX) + efeitos ÷ 1.000; DEX 100 soma 0,48 e DEX 300 soma 0,79 | **Pendente**: decidir se a DEX acelera as habilidades |
| Tempo de disparo | tempo base ÷ taxa de velocidade | Melia |
| Recarga (cooldown) | tempo base × (1 + efeitos ÷ 100) + modificador da habilidade | Melia |
| Alcance, ângulo e área | valor da habilidade + bônus do personagem (só para leque e quadrado) | Melia |
| Atraso entre golpes dos monstros | 3,0 s abaixo do nível 40 até 1,0 s a partir do 220 (projéteis: 3,0 s abaixo do 75 até 1,5 s a partir do 220) | Melia · **pendente** para chefes |
| Preço dos efeitos de classe (habilidades passivas) | tabela de preços por rank e círculo em `ability_price.cs` | **Pendente**: o projeto muda ranks e círculos; precisa refazer |

## 4. Monstros

| Fórmula | Regra do Melia | Situação |
| --- | --- | --- |
| HP, SP, ataque, defesa, precisão, esquiva, bloqueio, crítico | valores fixos por monstro em `monsters.txt` (7.253 monstros), sem multiplicador global | **Decidida**: multiplicadores por faixa de nível em [equipment-curve.json](../../data/proposals/equipment-curve.json) · [4.4](calibragem-monstros.md) |
| Velocidade dos monstros | valor fixo por monstro | Melia |
| Chefes, elites e masmorras | chefes seguem o tamanho do grupo; elites mantêm os valores do Melia | **Decidida** · [4.7](chefes-elites-masmorras.md) |
| Experiência e experiência de classe | valor por monstro × taxa de experiência; bônus separado para elites e monstros azuis (jackpot) | **Decidida**: taxa em 150% e curva própria · [4.8](curva-de-experiencia.md) |

## 5. Drops, itens e economia

| Fórmula | Regra do Melia | Situação |
| --- | --- | --- |
| Tabela de drops | cada monstro traz sua lista de itens com chance e quantidade (1.654 dos 7.253 monstros têm lista; 8.736 entradas no total) | Melia · **pendente**: calibrar com a economia |
| Chance ajustada | chance do monstro × taxa do tipo: silver, equipamento, orbes (azul e vermelha), gemas e geral | Melia · **pendente**: silver talvez saia |
| Preço de venda e compra | por item em `items.txt` e `shops.txt` | **Pendente** · [5.2](economia-das-pocoes.md) |
| Receitas de fabricação | 1.411 linhas em `recipes.txt` | **Pendente** |
| Refino, transcendência, encantamento e gemas | o Melia tem só uma parte (gemas); os valores do jogo original não estão nos dados | **Pendente**: sem fonte; decidir se entram |
| Poções | proporcionais ao HP, recarga de 30 s | **Decidida** · [5.3](ciclo-de-recuperacao.md) |
| Cubos e selos ligados às deusas | scripts em `goddess_cubes.cs` | **Fora**: sai com as deusas · [1.2](remocao-das-deusas.md) |

## O que isso quer dizer para o simulador

- **O simulador de builds consegue começar já** com as seções 1, 2 e 3: atributos, ataque, defesa percentual, esquiva, crítico, bloqueio, HP, SP e fator das habilidades. As fórmulas decididas já bastam para comparar Warrior, Archer e Scout.
- **Três decisões travam detalhes do simulador:** a taxa de crítico pela DEX, o custo de SP (nível 300 contra 280) e se a DEX acelera as habilidades.
- **Itens e refino não entram na primeira versão.** O simulador pode usar os graus de equipamento de [4.5](curva-de-equipamentos.md) e deixar refino e gemas como pendência.
- **Drops e quests não são fórmulas:** são tabelas de dados, tratadas na etapa da base de dados.

## Pendências

- Pesquisar como a DEX afetava a taxa de crítico em 2016.
- Decidir o custo de SP para o nível máximo de 280.
- Decidir se a DEX acelera as habilidades e se o reforço por habilidades de apoio entra.
- Calibrar as taxas de drop e o silver com a economia.
- Refazer a tabela de preços das habilidades de classe para os ranks e círculos do projeto.
- Conferir no servidor, quando houver computador, as fórmulas preliminares de esquiva, crítico e bloqueio até o nível 280.

**Próximo passo recomendado:** construir o simulador de builds com as seções 1 a 3 desta página, começando pelas três bases revisadas.
