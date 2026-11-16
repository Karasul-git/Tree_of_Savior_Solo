# Ciclo de recuperação

[Recuperação no lançamento](../sistemas/recuperacao-2016.md) · [Cura e poções](cura-e-pocoes.md) · [Resumo do balanceamento](resumo-do-balanceamento.md)

**Direção do proprietário, 16/11/2026.** A sustentação segue o **ciclo do jogo original**: lutar usando poções só para segurar, depois **sentar e acender a fogueira** para recuperar. A meta é manter as sensações do original: se for preciso parar para descansar, o grupo para. As poções só serão reforçadas se os testes pedirem. Esta direção substitui a decisão anterior de usar poções como base. Os números abaixo são **propostas** para teste. Os dados estão em [recovery-cycle.json](../../data/proposals/recovery-cycle.json).

## O ciclo

1. **Puxar e lutar.** Sozinho, de 2 a 5 monstros por vez, conforme a classe: Archer e Scout perto de 2 a 3; Warrior de escudo, 4 a 5.
2. **Segurar com poções.** Elas curam uma porcentagem do HP e servem para emergências, não para encher a barra.
3. **Sentar e acender a fogueira.** O grupo inteiro recupera junto, em poucos segundos.
4. **Voltar a caçar.**

## O que o Melia já tem

| Recurso | Como funciona hoje | Fonte |
| --- | --- | --- |
| Descanso sentado | Dobra a regeneração natural: 2% do HP a cada 10 s. Encher a barra leva ~8 minutos. | [F124](../fontes.md#f124) |
| Fogueira | Usa lenha (`Firewood`); dura 5 minutos; cura 10% do HP e do SP a cada 5 s de quem está perto e sentado. Barra cheia em ~50 s. | [F124](../fontes.md#f124) |
| Lenha | Vendida nas lojas de Klaipeda, Fedimian e Orsha, por 30. Nenhum monstro deixa cair. | [F124](../fontes.md#f124) |
| Poção proporcional | O efeito `Drug_HealHP_MHP` já cura uma porcentagem do HP; hoje só as poções de missão o usam. | [F124](../fontes.md#f124) |

A estrutura do original está toda lá. Falta ajustar o ritmo.

## Proposta para teste

| Parte | Proposta | Por quê |
| --- | --- | --- |
| Regeneração em combate | Manter baixa (1% do HP a cada 20 s). | Cada luta custa alguma coisa. |
| Descanso sentado | **10% do HP e do SP a cada 3 s** fora de combate: barra cheia em ~30 s. | Hoje leva ~8 minutos; o ciclo ficaria parado demais. |
| Fogueira | **Mais 10% a cada 3 s**: barra cheia em ~15 s, para todo o grupo perto. | Faz da fogueira um ritual do grupo, como em 2016. |
| Poções | Proporcionais ao HP: **5% a 10%** as comuns, **20% a 40%** as de Alchemist; recarga de **30 s**. | Valores dos relatos de 2015–2016 ([Recuperação no lançamento](../sistemas/recuperacao-2016.md)). Reaproveita o efeito proporcional que já existe. |
| Lenha | Lojas, como hoje, e também **drops de monstros** (plantas e árvores) e coleta. | Mantém a fogueira funcionando mesmo se o silver sair. |
| Squire | Base Camp e Refreshment Table como versão melhorada da fogueira. | Reaproveita habilidades originais e dá papel ao Squire no grupo. |

## Efeito em outras decisões

- **Poções e economia.** O consumo cai das 2 a 3 poções por minuto da [conta anterior](cura-e-pocoes.md) para algumas por luta difícil. A [economia das poções](economia-das-pocoes.md) deixa de ser o problema central.
- **Ritmo de caça e experiência.** Com descansos, o ritmo efetivo sozinho cai de ~4 para ~2,5 a 3 monstros por minuto. Para manter ~150 horas até o nível 280, a experiência por monstro sobe **50%** (`exp_rate` 150%), por decisão do proprietário em 16/11/2026.
- **Dano dos monstros.** Continua em 5% a 10% do HP por golpe. O ciclo absorve esse dano sem mudar a calibragem.

## Pendências

- Testar o ritmo do ciclo e ajustar descanso e fogueira.
- Definir quais monstros deixam cair lenha.

**Próximo passo recomendado:** testar o ciclo no servidor, quando houver computador.
