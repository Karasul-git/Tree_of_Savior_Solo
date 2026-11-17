# Simulador de builds

[Todas as fórmulas](todas-as-formulas.md) · [Comparação das bases](comparacao-das-bases.md) · [Resumo do balanceamento](resumo-do-balanceamento.md)

**Versão 2 · 17/11/2026** (habilidades reais das três bases). O simulador começa vazio: escolha uma base (Warrior, Archer ou Scout), o nível, distribua os pontos e monte o equipamento, ou comece de um modelo pronto. Os botões "Limpar pontos" e "Limpar tudo" zeram a build. O simulador calcula HP, ataque, defesa, esquiva e crítico com as fórmulas do [inventário](todas-as-formulas.md) e mostra como o personagem se sai contra um monstro comum do mesmo nível. É possível comparar duas builds lado a lado e copiar o link para guardar ou enviar uma build.

<div id="simulador"><noscript><p>O simulador precisa de JavaScript. As fórmulas estão em <a href="todas-as-formulas.html">Todas as fórmulas</a>.</p></noscript></div>

## Como ler o resultado

- **Pontos de atributo:** 1 ponto por nível, até 279 pontos no nível 280. Cada atributo ganha um **bônus por investimento** (a cada 5 pontos gastos até 50, a cada 4 até 150, a cada 3 até 300), contado só sobre os pontos que você gasta.
- **Defesa:** corta uma fração do golpe, defesa ÷ (defesa + ataque), com máximo de 75%. Vale para você e para os monstros.
- **Habilidade:** você pode escolher a **habilidade de referência** (fator mediano de 151% no nível 1, 211% no nível 5, 362% no 15) ou uma **habilidade real** da base com dano (lista abaixo). Os monstros foram calibrados para o nível 5.
- **Habilidade real:** o fator é o do Melia dividido por 10 e cresce 10% do valor do nível 1 a cada nível, igual à habilidade de referência. Golpes múltiplos somam (Multi Shot dá 10 golpes). A habilidade exige a arma certa; se faltar, o simulador avisa.
- **Arma secundária:** nas habilidades que usam a arma da mão secundária (Dagger Slash, Oblique Fire), vale o ataque inteiro dela no lugar do bônus de 20%. É a nossa leitura da decisão de 15/11/2026; confirme se for diferente.
- **SP:** o custo vem do Melia, com a correção por nível que tem o nível 300 como referência (pendente para o 280). "Monstros com o SP cheio" mostra quantos monstros iguais dá para derrubar antes de acabar o SP, sem recuperação.
- **Habilidades para derrubar:** quantas habilidades iguais seriam necessárias para matar **sozinho** um monstro comum do mesmo nível. A meta do projeto é de 4 a 8 sozinho (1 a 2 de cada membro num grupo de 4).
- **Golpe do monstro tira do HP:** meta de 5% a 10% do HP de quem não é tanque.
- **Ataque de preparados:** usa o INT mais a arma principal, como decidido. A cura de preparados é o valor base × (1 + INT ÷ 100); o simulador mostra só o bônus.

## Limites da versão 1

- **Só as três bases (rank 1).** Os outros ranks e as habilidades das demais classes ainda não entram.
- **Poucas habilidades têm dano nos dados:** só Thrust e Bash (Warrior), Multi Shot, Oblique Shot e Twin Arrows (Archer), Dagger Slash e Oblique Fire (Scout). As demais são de apoio ou o Melia só traz um valor provisório de 100% (marcadas "Dano sem dados" na tabela).
- **Recarga e SP aparecem, mas não entram no dano por segundo:** o simulador compara o dano de **um uso**, não o ritmo de combate.
- **Crítico, bloqueio e velocidade das habilidades ficam de fora das contas de dano.** O ataque crítico e a taxa de crítico aparecem, mas a chance de crítico ainda não depende da DEX (pendente, ver [4.10](todas-as-formulas.md)).
- **Monstro de referência de 15 a 270.** Abaixo do nível 15 ainda não há tabela; acima do 270 vale a faixa do 270.
- **Equipamento:** só armas e armaduras comuns (grau 01), mais o grau 02 de armas. Refino, encantamento e gemas não entram. A arma de uma mão com escudo, a de duas mãos e a mão secundária (20%) seguem a [curva de equipamentos](curva-de-equipamentos.md).
- **O crescimento automático de atributos por classe do Melia não entrou**, porque a calibragem do projeto não o usou. Se for ligado no servidor, os números mudam.
- **São contas à mão:** nada foi testado no servidor. O simulador reproduz a [comparação das bases](comparacao-das-bases.md) com diferença de arredondamento (conferido por testes automáticos nos níveis 40, 120 e 220).

## Habilidades das três bases

Valores originais do Melia ([F124](../fontes.md#f124), `skills.txt`), antes da escala do projeto. Os dados estão em [base-skills.json](../../data/proposals/base-skills.json). O fator do projeto segue a regra decidida: valor do Melia ÷ 10, mais 10% do valor do nível 1 por nível.

### Warrior

| Habilidade | Círculo | Tipo | Fator do Melia | Fator no projeto (nível 1 → 5 → 15) | Golpes | SP base | Recarga |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Thrust | 1 | Dano | 1.678% (+252% por nível) | 168% → 235% → 403% | 1 | 10 | 1 s |
| Bash | 1 | Dano | 1.835% (+688% por nível) | 183% → 257% → 440% | 1 | 49 | 10 s |
| Gung Ho | 1 | Apoio | – | – | 1 | 168 | 60 s |
| Concentrate | 1 | Apoio | – | – | 1 | – | 20 s |
| Pain Barrier | 1 | Apoio | – | – | 1 | 134 | 30 s |
| Restrain | 2 | Apoio | – | – | 1 | – | 120 s |
| Pommel Beat | 2 | Dano sem dados | – | – | 1 | – | 14 s |
| Double Slash | 3 | Dano sem dados | – | – | 1 | – | 21 s |

- Pain Barrier: Escudo que absorve dano (o valor de 25.000% do Melia é a absorção, não um ataque).
- Pommel Beat: O Melia só traz o valor provisório de 100%; o dano real não está nos dados.
- Double Slash: O Melia só traz o valor provisório de 100%.

### Archer

| Habilidade | Círculo | Tipo | Fator do Melia | Fator no projeto (nível 1 → 5 → 15) | Golpes | SP base | Recarga |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Swift Step | 1 | Apoio | – | – | 1 | 196 | 60 s |
| Multi Shot | 1 | Dano | 199% (+74% por nível) | 20% → 28% → 48% | 10 | 117 | 5 s |
| Full Draw | 1 | Dano sem dados | – | – | 1 | – | 20 s |
| Oblique Shot | 1 | Dano | 1.754% (+263% por nível) | 175% → 246% → 421% | 1 | 11 | 1 s |
| Kneeling Shot | 2 | Apoio | – | – | 1 | – | – |
| Heavy Shot | 2 | Dano sem dados | – | – | 1 | – | 10 s |
| Twin Arrows | 3 | Dano | 1.002% (+375% por nível) | 100% → 140% → 240% | 2 | 56 | 15 s |

- Full Draw: O Melia só traz o valor provisório de 100%; o dano real não está nos dados.
- Kneeling Shot: No Melia só existe como habilidade de Quarrel Shooter; é uma postura, sem dano direto.
- Heavy Shot: O Melia só traz o valor provisório de 100%.

### Scout (kit base proposto)

| Habilidade | Círculo | Tipo | Fator do Melia | Fator no projeto (nível 1 → 5 → 15) | Golpes | SP base | Recarga |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Dagger Slash | 1 | Dano | 522% (+196% por nível) | 52% → 73% → 125% | 2 | 12 | 1 s |
| Oblique Fire | 1 | Dano | 994% (+373% por nível) | 99% → 139% → 239% | 1 | 11 | 1 s |
| Cloaking | 1 | Apoio | – | – | 1 | 145 | 40 s |
| Free Step | 1 | Apoio | – | – | 1 | 168 | 60 s |
| Double Attack | 1 | Apoio | – | – | 1 | 168 | 60 s |
| Camouflage | 2 | Apoio | – | – | 1 | – | 76 s |
| Perspective Distortion | 2 | Apoio | – | – | 1 | – | 40 s |
| Scan | 3 | Apoio | – | – | 1 | – | 58 s |
## Próximas versões

1. Dano por segundo, com recarga e tempo de conjuração.
2. Ranks, círculos e pontos de habilidade.
3. Crítico, bloqueio e velocidade, quando as decisões pendentes saírem.
4. Equipamento por item, com refino e gemas, se entrarem no projeto.

**Próximo passo recomendado:** testar as habilidades reais no simulador e dizer o que falta; depois, decidir o que fazer com as habilidades sem dado de dano e trazer o ritmo de combate (recarga e conjuração).
