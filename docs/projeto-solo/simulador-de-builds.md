# Simulador de builds

[Todas as fórmulas](todas-as-formulas.md) · [Comparação das bases](comparacao-das-bases.md) · [Resumo do balanceamento](resumo-do-balanceamento.md)

**Versão 1 · 17/11/2026.** Escolha uma base (Warrior, Archer ou Scout), o nível, a distribuição dos pontos e o equipamento. O simulador calcula HP, ataque, defesa, esquiva e crítico com as fórmulas do [inventário](todas-as-formulas.md) e mostra como o personagem se sai contra um monstro comum do mesmo nível. É possível comparar duas builds lado a lado e copiar o link para guardar ou enviar uma build.

<div id="simulador"><noscript><p>O simulador precisa de JavaScript. As fórmulas estão em <a href="todas-as-formulas.html">Todas as fórmulas</a>.</p></noscript></div>

## Como ler o resultado

- **Pontos de atributo:** 1 ponto por nível, até 279 pontos no nível 280. Cada atributo ganha um **bônus por investimento** (a cada 5 pontos gastos até 50, a cada 4 até 150, a cada 3 até 300), contado só sobre os pontos que você gasta.
- **Defesa:** corta uma fração do golpe, defesa ÷ (defesa + ataque), com máximo de 75%. Vale para você e para os monstros.
- **Habilidade de referência:** o fator começa em 151% no nível 1 e soma 10% do valor inicial por nível (211% no nível 5, 362% no 15). Os monstros foram calibrados para o nível 5. Não é uma habilidade real de uma classe.
- **Habilidades para derrubar:** quantas habilidades iguais seriam necessárias para matar **sozinho** um monstro comum do mesmo nível. A meta do projeto é de 4 a 8 sozinho (1 a 2 de cada membro num grupo de 4).
- **Golpe do monstro tira do HP:** meta de 5% a 10% do HP de quem não é tanque.
- **Ataque de preparados:** usa o INT mais a arma principal, como decidido. A cura de preparados é o valor base × (1 + INT ÷ 100); o simulador mostra só o bônus.

## Limites da versão 1

- **Só as três bases.** Os ranks e as habilidades de cada classe não entram: a "habilidade de referência" tem o mesmo fator para todos.
- **Crítico, bloqueio e velocidade das habilidades ficam de fora das contas de dano.** O ataque crítico e a taxa de crítico aparecem, mas a chance de crítico ainda não depende da DEX (pendente, ver [4.10](todas-as-formulas.md)).
- **Monstro de referência de 15 a 270.** Abaixo do nível 15 ainda não há tabela; acima do 270 vale a faixa do 270.
- **Equipamento:** só armas e armaduras comuns (grau 01), mais o grau 02 de armas. Refino, encantamento e gemas não entram. A arma de uma mão com escudo, a de duas mãos e a mão secundária (20%) seguem a [curva de equipamentos](curva-de-equipamentos.md).
- **O crescimento automático de atributos por classe do Melia não entrou**, porque a calibragem do projeto não o usou. Se for ligado no servidor, os números mudam.
- **São contas à mão:** nada foi testado no servidor. O simulador reproduz a [comparação das bases](comparacao-das-bases.md) com diferença de arredondamento (conferido por testes automáticos nos níveis 40, 120 e 220).

## Próximas versões

1. Habilidades reais de cada base, com o fator de cada uma.
2. Ranks, círculos e pontos de habilidade.
3. Crítico, bloqueio e velocidade, quando as decisões pendentes saírem.
4. Equipamento por item, com refino e gemas, se entrarem no projeto.

**Próximo passo recomendado:** testar o simulador com as suas builds preferidas e dizer o que falta; depois, trazer as habilidades reais das três bases.
