# Resumo do balanceamento

[Diretrizes solo](diretrizes.md) · [Estrutura do servidor](servidor-estrutura.md) · [Fontes](../fontes.md)

**Resumo de 15/11/2026.** Esta página reúne num só lugar as decisões e os números do balanceamento, para guiar a implementação quando o servidor estiver rodando. Os detalhes, fontes e cálculos estão nas páginas de cada tema. Todos os números são **provisórios**: foram calculados à mão sobre os dados do Melia e ainda não foram testados no jogo.

## Decisões fechadas

| Tema | Decisão | Página |
| --- | --- | --- |
| Grupo | Jogador + até 3 bots (grupo de 4); bots do barracão e do servidor; posturas e ordens simples. | [Bots e grupo](companheiros-e-bots.md) |
| Árvore | 36 posições ratificadas em Warrior, Archer e Scout; ranks até R7, círculos até C3. | [Ranks e especializações](ranks-e-especializacoes.md) |
| Kit de Scout | Opção B: 5 habilidades posteriores em C1, Camouflage e Perspective Distortion em C2, Scan em C3. | [Kit base de Scout](scout-kit-base.md) |
| Pontos | 1 ponto de atributo por nível (279 até o 280), com bônus por investimento; 1 ponto de habilidade por nível de classe. Pontos extras de história e estátuas realocados. | [Pontos por nível](pontos-por-nivel.md) |
| Atributos | STR: ataque físico e precisão. CON: HP e defesas (+2 de cada defesa por ponto). INT: potência de preparados. SPR, agora **Sabedoria**: SP e recuperação. DEX: crítico e esquiva. | [Pontos por nível](pontos-por-nivel.md) |
| INT | Dano e cura de química, medicina e consumíveis; deve crescer para outras habilidades. | [Magia e INT](habilidades-magicas-e-int.md) |
| Escala das habilidades | Fatores das habilidades do Melia divididos por 10 (faixa de 150% a 200%). | [Curva de equipamentos](curva-de-equipamentos.md) |
| Defesa | Redução = defesa ÷ (defesa + ataque do golpe), no máximo 75%, para jogadores e monstros. | [Comparação das bases](comparacao-das-bases.md) |
| Arma secundária | Adaga ou pistola somam 20% do próprio ataque ao ataque geral; o valor inteiro vale nas habilidades que as usam. | [Curva de equipamentos](curva-de-equipamentos.md) |
| Monstros comuns | 1 a 2 habilidades de cada membro do grupo (4 a 8 sozinho); golpe de 5% a 10% do HP de quem não é tanque. | [Calibragem dos monstros](calibragem-monstros.md) |
| Chefes | Luta de 1 a 2 minutos com o grupo de 4; golpe de 15% a 25% do HP; HP ajustado ao tamanho do grupo. | [Chefes e masmorras](chefes-elites-masmorras.md) |
| Elites | Valores atuais do Melia (150% de HP e atributos, 200% de experiência). | [Chefes e masmorras](chefes-elites-masmorras.md) |
| Experiência | ~150 horas até o 280; ranks nos níveis 1, 15, 45, 80, 115, 155 e 200; experiência dividida igualmente no grupo. | [Curva de experiência](curva-de-experiencia.md) |

## Números de referência no nível 120

| Item | Valor |
| --- | --- |
| Espada comum / arma de duas mãos | 921 / 1.088 de ataque |
| Monstro comum (meta) | 10.248 de HP, 1.777 de ataque, 434 de defesa |
| Chefe com grupo de 4 (meta) | 205 mil a 410 mil de HP, 3.597 de ataque |
| Warrior de escudo e placa | Reduz 64% do golpe, leva ~5% do HP por golpe |
| Archer ou Scout | Leva ~9% do HP por golpe e desvia de ~12% |
| Habilidades para derrubar um monstro comum sozinho | 5 a 6 em qualquer base |
| Tempo acumulado ao chegar ao nível 120 | ~21 horas |

## O que muda no Melia

| Onde | Mudança |
| --- | --- |
| `system/db/jobs.txt` | Ranks solo das 39 classes; Monk, Paladin, Alchemist e Plague Doctor mudam de árvore. [Tabela](classes-e-progressao-servidor.md) |
| `system/conf/world/jobs.conf` | `job_max_rank` de 4 para 7. |
| `system/db/exp.txt` (e cliente) | Nova tabela de experiência e 7 ranks de 15 níveis de classe. [Curva](curva-de-experiencia.md) |
| `system/db/features.txt` | Desligar `NoStatByLevel` e `IncreasedStatRatio` para voltar aos pontos livres. |
| `calc_character.cs` | CON na defesa; cura de preparados com INT; regra da arma secundária; bônus por investimento, se ainda não existir. |
| `calc_combat.cs` | Defesa percentual com limite de 75% no lugar da subtração. |
| `calc_monster.cs` | Multiplicadores de HP, ataque, defesa e precisão por nível; ajuste de HP pelo tamanho do grupo em chefes e masmorras. |
| `system/db/skills.txt` ou cálculo de habilidades | Fatores das habilidades divididos por 10. |
| Localização do cliente e do servidor | Nome "Sabedoria" para SPR. |

## Pontos que ainda não conversam entre si

Revisando o conjunto, estes pontos precisam de atenção antes dos testes:

1. **Nível das habilidades.** A calibragem usou um fator médio de 175%. Com a divisão por 10, uma habilidade no nível 5 chega a ~282%. Conforme o jogador investe pontos, os monstros vão cair mais rápido que a meta. É preciso decidir se a meta vale para habilidades de nível baixo ou médio.
2. **Defesa mágica dos monstros.** O ajuste foi calculado para a defesa física. Os preparados (INT) usam a defesa mágica, que ainda não tem multiplicador.
3. **Equipamento melhor que o comum.** Toda a conta usa equipamento de grau 01. Itens de grau mais alto e melhorias deixam o personagem mais forte que a referência.
4. **Crítico.** A chance de crítico hoje não depende da DEX, e a resistência a crítico dos monstros não foi calibrada. Falta a fonte de 2016.
5. **Chefe jogando sozinho.** O HP cai com o grupo, mas o ataque não. Sem tanque, cada golpe tira 15% a 25% do HP.
6. **Ritmo de jogo.** As metas de chefes e de experiência supõem uma habilidade a cada 2 segundos e 4 monstros por minuto sozinho. Os dois valores precisam ser medidos.
7. **Cura e poções.** A fórmula da cura de preparados com INT e a suficiência das poções originais ainda não foram definidas.

## Pendências principais

- Resolver os pontos acima, começando pelo **nível das habilidades** e pela **defesa mágica**, que mudam os números da calibragem.
- Fase 1: rodar o Melia original com o cliente da Steam (requer computador).
- Medir no jogo o ritmo de habilidades e de caça e ajustar as tabelas.

**Próximo passo recomendado:** resolver o ponto 1 (nível das habilidades). Ele decide se a calibragem vale para o começo ou para o meio do investimento em cada habilidade.
