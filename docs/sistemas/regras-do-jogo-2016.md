# Regras do jogo em 2016

[Fórmulas de status](formulas-de-status-2016.md) · [Tabelas de experiência](../original-2016/experiencia.md) · [Fontes](../fontes.md)

Resumo das mecânicas do jogo no período do lançamento, a partir do wiki da comunidade do ToSBase ([F144](../fontes.md#f144)), escrito em outubro de 2015 e capturado entre março e junho de 2016. É um **resumo com as nossas palavras**: o texto original não é reproduzido. Quando uma regra tem números exatos em outra página da wiki, há link.

**Limites:** é um wiki de fãs do beta de 2015, e algumas regras podem ter mudado até o lançamento. A tabela completa de elementos era uma imagem e não está nos dados.

## Dano: tipos, armaduras e tamanhos

Todo golpe físico tem um **tipo de dano**, definido pela arma e pela habilidade, e todo personagem ou monstro tem um **tipo de armadura**: tecido, couro, placa ou espectral. A combinação muda o dano:

| Tipo de dano | Quem usa | Bônus | Penalidade |
| --- | --- | --- | --- |
| Corte | Espadas, espadas de duas mãos e muitas habilidades de Swordsman | +50% contra tecido | −25% contra placa |
| Perfuração | Lanças, adagas, floretes, muitas habilidades de Archer e algumas de Swordsman | +50% contra couro | −25% contra tecido |
| Impacto | Maças, varinhas e habilidades de Cleric | +50% contra placa | −25% contra couro |
| Mágico | Magias | +50% contra espectral | Nenhuma |
| Projétil | Arcos e bestas (pistolas e mosquetes sem confirmação) | Nenhum | −25% contra tamanho M, −50% contra L e XL |

Todo dano físico sofre ainda **−25% contra armadura espectral**.

## Elementos

Existem sete elementos: fogo, gelo, raio, veneno, terra, sagrado e trevas. O elemento do ataque e o do alvo mudam o dano. Ataques sem elemento não são afetados. Exemplos do wiki: gelo causa 150% contra fogo, raio causa 50% contra terra, e sagrado e trevas causam 200% um contra o outro.

## Razão de ataque e de defesa em área

Ataques em área têm uma **razão de ataque em área**, que é a soma do valor da habilidade, do valor do personagem e dos atributos que a melhoram. Ela funciona como um número de "cargas". Cada alvo atingido gasta cargas iguais à sua **razão de defesa em área**: personagens têm 1, e monstros costumam ter 1, 2 ou 3 conforme o tamanho (pequeno, médio, grande). O primeiro alvo atingido é o de maior defesa em área, e os outros são sorteados, até as cargas acabarem. Habilidades que não mostram a razão atingem todos na área.

## Atributos

Cada nível dá **1 ponto de atributo**, e estátuas da deusa Zemyna dão 1 ponto extra cada uma (uma vez por estátua). Os números exatos das contas estão em [Fórmulas de status](formulas-de-status-2016.md). O efeito de cada ponto:

| Atributo | O que cada ponto faz |
| --- | --- |
| STR | +1 ataque físico (mínimo e máximo), +1 ataque crítico, +5 no limite de peso |
| CON | +85 de HP, +1 de recuperação de HP, +1 de resistência a crítico, +5 no limite de peso, +1 de bloqueio com escudo |
| INT | +1 ataque mágico (mínimo e máximo) |
| SPR | +13 de SP, +1 de recuperação de SP, +1 de penetração de bloqueio, +0,2 de defesa mágica |
| DEX | +1 de precisão, +1 de taxa de crítico, +1 de esquiva |

STR e INT ganham ainda +10% por rank acima do primeiro. A DEX só vale para ataques físicos: a magia não erra, não é crítica e não pode ser desviada. Só as classes de Swordsman podem correr em disparada.

**Discrepância:** o wiki dá CON 3 e INT 3 ao Archer no nível 1, enquanto a calculadora do ToSBase dá CON 4 e INT 3. Usamos a calculadora, mas a dúvida fica registrada.

## Equipamentos

- **Raridade:** normal, mágico, raro e único. Quanto mais raro, mais forte.
- **Chapéus, penteados, braçadeiras e trajes** não dão atributos. As demais peças aparecem como o traje da classe.
- **Maestrias de armadura**, passivas que todas as classes têm:
  - **Tecido:** com 3 peças, mais SP máximo e −5% de dano mágico recebido. Com 4 peças, mais defesa mágica e outros −10%.
  - **Couro:** com 3 peças, mais esquiva. Com 4 peças, a esquiva dos atributos sobe 50%.
  - **Placa:** com 3 peças, mais HP máximo e −5% de dano físico recebido. Com 4 peças, mais estamina e outros −10%.

## Refino

Refinar melhora a arma, a armadura ou o escudo na bigorna. O item sobe de +1 até +15 ou mais, e a bigorna é consumida a cada uso, dê certo ou não. A taxa de sucesso cai conforme o nível, e quem erra perde 1 nível de refino **e 1 de potencial**. Com potencial 0, o item não pode mais ser refinado.

- **Armas:** sucesso garantido até +5; depois 88% (+6), 78%, 68%, 59% e 51% do +10 ao +15.
- **Armaduras:** sucesso garantido até +3, e a taxa cai a partir do +4.
- **Bônus de ataque** (estrelas = grau do item): até +5, nível × (estrelas + 2). Acima de +5, (5 + (nível − 5) × 2) × (estrelas + 2).
- **Defesa, defesa mágica, precisão e esquiva:** nível × (1 + estrelas ÷ 10). **Amplificação mágica:** nível × (estrelas + 1) ÷ 2.

Cada item mostra a sua tabela de refino na [página do item](../original-2016/itens/README.md).

## Gemas, encaixes e cartas

- **Encaixes:** o ferreiro de Klaipeda ou de Orsha abre buracos no item, até o máximo de encaixes dele. Cada buraco custa **1 de potencial**. Dá para encaixar gemas, amuletos mágicos e gemas de monstros. Tirar uma gema encaixada reduz o nível dela em 1.
- **Gemas:** quatro cores (vermelha, verde, azul e amarela), com bônus e penalidades que mudam conforme a peça. Sobem de nível sentado, na janela de aprimoramento, alimentadas com outras gemas. Só o Alchemist torra gemas para tirar as penalidades. **Quem morre larga as gemas do inventário.**
- **Cartas de chefe:** chefes podem soltar a carta deles. Servem para aprimorar a criatura do Necromancer e do Sorcerer e para um minijogo de duelo entre jogadores. Sobem de nível alimentadas com outras cartas.

## Descanso

Sentar (tecla Insert) acelera a recuperação de HP, SP e estamina. Sentado, a barra de atalhos oferece quatro coisas: acender uma **fogueira** (precisa de um reagente) para recuperar ainda mais rápido, fabricar com receitas, aprimorar gemas e aprimorar cartas.

## Experiência e grupo

- **Dois tipos de experiência:** a do personagem, que sobe o nível, e a de classe, que sobe o rank e libera classes. Os monstros dão as duas, em proporções variadas. Cartas de experiência de quests também dão. O companheiro ganha experiência junto.
- **Grupo:** tem sempre um líder. O líder escolhe a distribuição de itens (individual, em rodízio ou aleatória) e a de experiência (individual, igual para todos ou por nível), além de compartilhar quests. O grupo ganha **bônus de experiência** conforme o número de classes-base diferentes (três ou quatro) e se foi formado pelo buscador de masmorras; o bônus do buscador só vale em masmorras. As tabelas de experiência estão em [Tabelas de experiência](../original-2016/experiencia.md).

## Companheiros e alojamento

- **Companheiros** seguem o personagem, lutam, ganham nível e têm afinidade própria por personagem. Cada um ocupa **uma vaga do alojamento**. Quem tem o atributo de montaria pode cavalgá-los (Cataphract, Schwarzer Reiter e Hackapell); montado, ganha CON e esquiva. O Velheider custava 110.000 de silver, e o Hoglan e o Hawk, 453.600 cada, vendidos em Klaipeda. O Hawk é só do Falconer.
- **Alojamento:** o quarto inicial do Pyromancer comporta 7 personagens, e o salão de banquetes, 10.

## Transporte

As **estátuas da deusa Vakarine** são o transporte principal: descobrir uma estátua libera o ponto, e rezar diante dela permite viajar entre as liberadas pagando silver, conforme a distância. Nas cidades, as estátuas da deusa Austrine fazem esse papel. Há também pergaminhos de teletransporte, vendidos em Klaipeda e em Orsha, e a habilidade Base Camp do Squire.

## PvP

Existem **arenas** (3 contra 3, 5 contra 5 e grupos montados de 5 contra 5) e **duelos amistosos**. Não havia recompensa, só o ranking.

## Fonte

Wiki da comunidade do ToSBase ([F144](../fontes.md#f144)), artigos de outubro de 2015 capturados entre março e junho de 2016. Resumo próprio, sem reprodução do texto.
