# Economia das poções

[Cura e poções](cura-e-pocoes.md) · [Resumo do balanceamento](resumo-do-balanceamento.md)

**Estudo de 16/11/2026.** Quanto silver a caça rende contra quanto as poções custam, com a sustentação por poções decidida pelo proprietário. Os dados estão em [potion-economy.json](../../data/proposals/potion-economy.json). [F124](../fontes.md#f124)

**O proprietário avalia remover a mecânica de silver.** O estudo foi feito mesmo assim, porque mostra o tamanho do problema que a economia, com ou sem silver, precisa resolver.

## Como o silver cai no Melia

- Cada monstro tem um drop de silver (item 900011) com chance e quantidade próprias.
- Dois ajustes globais em `drops.conf`: `silver_drop_amount` (quantidade) e `silver_drop_rate` (chance), ambos em 100%.
- Cerca de 60% a 75% dos monstros comuns deixam cair silver.

## Silver contra poções, caçando sozinho

Hipóteses: 240 monstros por hora; 2 a 3 poções por minuto (120 a 180 por hora), como na [conta de sustentação](cura-e-pocoes.md). A venda de outros itens que os monstros deixam cair **não** entrou na conta.

| Nível | Silver por monstro (média) | Silver por hora | Poção e preço | Gasto em poções por hora | Silver cobre |
| --- | --- | --- | --- | --- | --- |
| 40 | 14,8 | 3.552 | HP Potion (160) | 19.200 – 28.800 | 12% – 18% |
| 120 | 31,2 | 7.488 | Large HP Potion (360) | 43.200 – 64.800 | 12% – 17% |
| 220 | 61,6 | 14.784 | Huge HP Potion (720) | 86.400 – 129.600 | 11% – 17% |

**Leitura:** o silver da caça cobre só **12% a 19%** do gasto com poções. Para comparação, um jogador relatava ganhar ~200 mil de silver por hora caçando em abril de 2016, muito acima dos 7 a 15 mil por hora dos dados atuais do Melia. Ver [Economia no lançamento](../sistemas/economia-2016.md). Com os números do Melia, a caça sozinho dá prejuízo, mesmo antes de contar equipamento, reparos e habilidades. A venda de itens ajuda, mas teria de render de 5 a 8 vezes o silver direto para fechar a conta.

## Caminhos

| Caminho | O que muda | Observação |
| --- | --- | --- |
| **Sem silver** | Poções vêm de drops, de fabricação e de recompensas. Reaproveita a alquimia original: Tincturing (Alchemist) produz poções a partir de ervas, e Dig coleta materiais. | Combina com a ideia do proprietário e com o reaproveitamento de sistemas originais. Exige definir as fontes de ervas e materiais. |
| Silver com ajuste | Aumentar `silver_drop_amount` em 6 a 8 vezes, ou baratear as poções na mesma proporção. | Só configuração. Resolve a conta, mas mantém toda a economia de silver. |
| Rever a sustentação | Voltar à regeneração fora de combate ou baixar o dano dos monstros. | Já foi descartado ao escolher poções como base. |

## Pendências

- Decidir se o silver sai do jogo.
- Se sair: definir de onde vêm poções, equipamento e serviços (reparo, habilidades, atributos).
- Contar a venda de itens, se o silver continuar.

**Próximo passo recomendado:** decidir sobre o silver. Se ele sair, o próximo estudo é a **fabricação de poções** a partir da alquimia original (ervas, Tincturing e Dig).
