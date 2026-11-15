# Curva de experiência

[Classes e progressão no servidor](classes-e-progressao-servidor.md) · [Calibragem dos monstros](calibragem-monstros.md) · [Elites, chefes e masmorras](chefes-elites-masmorras.md)

**Estudo de 15/11/2026.** A curva de experiência do projeto, montada a partir das metas do proprietário. A tabela completa de 279 níveis está em [exp-curve.json](../../data/proposals/exp-curve.json). Nada foi testado no servidor. [F124](../fontes.md#f124)

## Decisões do proprietário

| Tema | Decisão |
| --- | --- |
| Tempo até o nível 280 | **Cerca de 150 horas** de jogo. |
| Ritmo | **Rápido no início, mais lento no fim.** |
| Divisão da experiência no grupo | **Igual entre os membros.** Como o grupo de 4 derruba monstros cerca de 4 vezes mais rápido, cada um evolui no mesmo ritmo sozinho ou em grupo. É a opção "distribuição igual" do menu de grupo original. |
| Níveis dos ranks | **R7 por volta do nível 200**, com ranks mais próximos no início. |

## Como a curva foi montada

- **Tempo por nível** = 2 minutos + um valor que cresce com o nível (proporcional a nível<sup>1,5</sup>). O piso de 2 minutos evita que os primeiros ranks passem em poucos minutos.
- **Ritmo de caça sozinho:** cerca de **4 monstros por minuto** (15 segundos cada: 4 a 8 habilidades a cada 2 segundos, mais o deslocamento). É uma hipótese, a ser medida no servidor.
- **Experiência dos monstros:** a mediana atual do Melia, sem mudança. Quem muda é a **tabela de experiência por nível**.
- O próprio Melia avisa que a tabela de experiência precisa mudar **também no cliente**, que usa os valores locais para alguns cálculos.

## Marcos

| Rank | Nível em que fica disponível | Tempo acumulado | Duração do rank | Experiência de classe por nível de classe |
| --- | --- | --- | --- | --- |
| R1 | 1 | 0 h | 0,6 h | 651 |
| R2 | 15 | 0,6 h | 2,3 h | 8.087 |
| R3 | 45 | 2,9 h | 5,8 h | 50.570 |
| R4 | 80 | 8,7 h | 10,2 h | 169.568 |
| R5 | 115 | 18,9 h | 18,2 h | 574.513 |
| R6 | 155 | 37,1 h | 30,1 h | 1.245.485 |
| R7 | 200 | 67,2 h | 82,8 h | 8.188.253 |

O R7 vai do nível 200 ao 280. Ao chegar ao R7 com cerca de 67 horas, sobram umas 83 horas para os últimos níveis, os círculos e o equipamento.

## Amostra da tabela

| Nível | Tempo para o próximo nível | Experiência por monstro | Experiência para o próximo nível | Hoje no Melia |
| --- | --- | --- | --- | --- |
| 10 | 2,51 min | 85 | 854 | 3.120 |
| 45 | 6,88 min | 494 | 13.591 | 119.400 |
| 80 | 13,56 min | 1.060 | 57.507 | 403.025 |
| 100 | 18,16 min | 1.738 | 126.246 | 365.645 |
| 155 | 33,18 min | 3.761 | 499.217 | 2.141.940 |
| 200 | 47,71 min | 7.869 | 1.501.606 | 3.627.056 |
| 240 | 62,08 min | 10.779 | 2.676.753 | 4.289.440 |
| 279 | 77,31 min | 10.767 | 3.329.478 | 30.595.456 |

A tabela do projeto pede bem menos experiência que a atual do Melia, porque o jogo atual foi pensado para outro ritmo e para o nível máximo 550.

## Pendências

- Medir no servidor o ritmo real de caça e ajustar o tempo por nível.
- Aplicar a tabela no servidor (`exp.txt`) e no cliente.
- Definir a experiência de missões, chefes e masmorras dentro da mesma curva.

O [resumo do balanceamento](resumo-do-balanceamento.md) reúne esta curva com as outras decisões.

**Próximo passo recomendado:** resolver o nível das habilidades na calibragem.
