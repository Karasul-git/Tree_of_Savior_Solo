# Elites, chefes e masmorras

[Calibragem dos monstros](calibragem-monstros.md) · [Comparação das três bases](comparacao-das-bases.md) · [Curva de equipamentos](curva-de-equipamentos.md)

**Estudo de 15/11/2026.** Metas do proprietário para os monstros mais fortes e números calculados com o mesmo método da [calibragem](calibragem-monstros.md). Nada foi testado no servidor. Os dados estão em [boss-targets.json](../../data/proposals/boss-targets.json).

## Decisões do proprietário

| Tema | Decisão |
| --- | --- |
| Duração da luta contra chefe | **1 a 2 minutos** com o grupo de 4, no mesmo nível. |
| Golpe comum de chefe | **15% a 25% do HP** de quem não é o tanque (4 a 6 golpes para cair). |
| Ajuste pelo tamanho do grupo | **Sim.** Chefes e masmorras se ajustam ao número de membros. |
| Elites | **Manter o Melia:** 150% de HP e de atributos e 200% de experiência. Sozinho, uma elite pede 6 a 12 habilidades. |

## Como os números foram calculados

- **Dano do grupo:** cada membro usa uma habilidade a cada 2 segundos, em média, contando recargas e ataques básicos. É uma hipótese, a ser conferida no servidor.
- **HP do chefe** = duração da luta × dano do grupo.
- **Ataque do chefe:** o que faz um golpe tirar 20% do HP do personagem de referência (couro), já com a defesa percentual.
- **Defesa do chefe:** a mesma dos monstros comuns (reduz ~25% do dano).
- **Ajuste pelo grupo:** o HP do chefe é proporcional ao número de membros (sozinho, um quarto). Assim, a luta dura parecido sozinho ou com o grupo. O ataque não muda.

## Metas dos chefes

| Nível | HP com grupo de 4 | HP sozinho | Ataque | Defesa | HP mediano dos chefes no Melia hoje |
| --- | --- | --- | --- | --- | --- |
| 15 | 33.075 – 66.150 | 8.269 – 16.538 | 497 | 70 | 3.508 |
| 40 | 72.765 – 145.530 | 18.191 – 36.382 | 1.166 | 154 | 14.037 |
| 75 | 128.992 – 257.985 | 32.248 – 64.496 | 2.176 | 273 | 42.892 |
| 120 | 204.908 – 409.815 | 51.227 – 102.454 | 3.597 | 434 | 117.392 |
| 170 | 291.848 – 583.695 | 72.962 – 145.924 | 5.333 | 618 | 225.641 |
| 220 | 380.520 – 761.040 | 95.130 – 190.260 | 7.271 | 805 | 426.938 |
| 270 | 473.602 – 947.205 | 118.401 – 236.801 | 9.342 | 1.002 | 595.028 |

**Leitura:**

- Um chefe tem **20 a 40 vezes** o HP de um monstro comum do mesmo nível, e cerca do **dobro do ataque**.
- Nos níveis altos, as metas ficam na mesma ordem dos chefes atuais do Melia. Nos níveis baixos, os chefes atuais são bem mais fracos que a meta.
- **Atenção ao jogar sozinho:** o chefe tem menos HP, mas bate igual. Sem tanque, cada golpe tira 15% a 25% do HP. O uso de poções e da esquiva vai pesar. Vale observar nos testes se o ataque também deve cair quando se joga sozinho.

## Masmorras

- Os monstros comuns de masmorra seguem as mesmas metas dos de campo, também ajustados pelo tamanho do grupo.
- O chefe final segue as metas acima.
- Duração total, número de salas e recompensas ficam para quando o conteúdo das masmorras for revisado.

## Pendências

- Conferir no servidor o ritmo de uma habilidade a cada 2 segundos.
- Decidir se o ataque do chefe também diminui quando se joga sozinho.
- Definir habilidades especiais e fases dos chefes.
- Estrutura das masmorras (salas, duração, recompensas).

A [curva de experiência](curva-de-experiencia.md) foi montada em seguida.

**Próximo passo recomendado:** reunir o balanceamento numa página-resumo.
