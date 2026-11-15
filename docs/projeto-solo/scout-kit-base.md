# Scout · kit base proposto

[Diretrizes solo](diretrizes.md) · [Scout em 2016](../classes/catalogo/scout.md) · [Companheiros e bots](companheiros-e-bots.md) · [Elenco e pendências](disponibilidade-das-classes.md)

**Estudo de 15/11/2026 · opção B ratificada pelo proprietário no mesmo dia.** Scout é base da árvore solo por decisão do proprietário. Em 2016, porém, Scout era uma especialização de **Archer** (primeiro rank 4) e três das suas sete habilidades usavam arco. Esta página compara o kit de 2016 com o kit oficial posterior e propõe uma base que **reaproveita apenas habilidades existentes**, sem criar técnicas novas.

A ficha histórica não muda. Os dados da proposta estão em [scout-base-kit.json](../../data/proposals/scout-base-kit.json).

## Os dois kits existentes

| Habilidade | Scout 2016 (Archer) | Scout posterior (base) | Arma | Fonte |
| --- | --- | --- | --- | --- |
| Camouflage | C1 · esconde o usuário num barril | — | Nenhuma específica | [F71](../fontes.md#f71) |
| Flu Flu | C1 · flecha sonora que assusta | — | **Arco** | [F71](../fontes.md#f71) |
| Flare Shot | C1 · sinalizador com dano em área | — | **Arco** | [F71](../fontes.md#f71) |
| Cloaking | C1 · oculta até atacar ou saltar | Ocultação temporária; esconde também companheiros | Nenhuma específica | [F71](../fontes.md#f71), [F111](../fontes.md#f111) |
| Perspective Distortion | C2 · aliados atacam além do alcance | — | Nenhuma específica | [F71](../fontes.md#f71) |
| Scan | C3 · revela itens e inimigos escondidos | — | Nenhuma específica | [F71](../fontes.md#f71) |
| Split Arrow | C3 · flecha que ricocheteia | — | **Arco** | [F71](../fontes.md#f71) |
| Dagger Slash | — | Cortes rápidos com adaga | Adaga | [F111](../fontes.md#f111) |
| Double Attack | — | Mais crítico; ataques básicos com acertos múltiplos | Nenhuma específica | [F111](../fontes.md#f111) |
| Free Step | — | Aumenta a esquiva temporariamente | Nenhuma específica | [F111](../fontes.md#f111) |
| Oblique Fire | — | Tiro de pistola que ricocheteia para um inimigo próximo | Pistola | [F111](../fontes.md#f111) |

A página oficial atual não tem data de revisão e mostra só as habilidades principais. Números, atributos e círculos posteriores não foram importados.

## Armas da árvore

O anúncio do Re:Build (26/11/2018) descreve a árvore de Scout com **espada de uma mão como arma principal** e a maior parte das habilidades usando **adagas e pistolas na arma secundária**. A armadura indicada é de couro, como para Archer. [F12](../fontes.md#f12). A página atual de Scout também lista os atributos de troca de arma e de uso de espada de uma mão. [F111](../fontes.md#f111)

As especializações da árvore solo já usam essas armas: Rogue usa adagas (Backstab), Corsair usa adaga e pistola, Schwarzer Reiter usa pistola e Outlaw usa adaga e pistola. Squire, Alchemist e Plague Doctor não dependem de uma arma específica. Wild Shot, de Schwarzer Reiter, também usa **besta**, que é arma de Archer: é uma pendência própria dessa classe.

**Ratificado:** espada de uma mão (principal) + adaga ou pistola (secundária), armadura de couro. Isso não aprova a compatibilidade de equipamento no cliente.

## Opções avaliadas

| Opção | Ideia | Vantagem | Problema |
| --- | --- | --- | --- |
| A · Kit posterior integral | Só as cinco habilidades atuais. | Kit oficial coerente com adagas e pistolas. | Não preenche C1–C3 no padrão das bases de 2016. |
| **B · Posterior + 2016 sem arco (recomendada)** | Cinco habilidades atuais em C1; três de 2016 em C2–C3. | Usa só habilidades existentes, preserva identidade de reconhecimento e encaixa nos círculos. | Duas habilidades precisam de conferência de função. |
| C · Kit de 2016 com arco | Manter o Scout histórico como base. | Fiel ao lançamento. | Três habilidades de arco contradizem a árvore de adagas e pistolas. |

## Kit ratificado (opção B)

O proprietário ratificou a opção B, incluindo os círculos abaixo e as armas propostas, como **direção de design**. A ratificação não certifica funcionamento no cliente nem define números.

O formato segue o de Swordsman em 2016: **cinco habilidades em C1, duas em C2 e uma em C3**.

| Círculo | Habilidade | Origem | Função na base | Situação |
| --- | --- | --- | --- | --- |
| C1 | Dagger Slash | Posterior | Ataque principal de adaga. | Reaproveitar |
| C1 | Oblique Fire | Posterior | Ataque de pistola com ricochete; ocupa o papel de Split Arrow. | Reaproveitar |
| C1 | Cloaking | 2016 e posterior | Ocultação; a versão atual já esconde companheiros. | Reaproveitar · conferir animação |
| C1 | Free Step | Posterior | Esquiva. | Reaproveitar |
| C1 | Double Attack | Posterior | Crítico e acertos múltiplos. | Reaproveitar |
| C2 | Camouflage | 2016 (C1 histórico) | Barril que absorve ataques físicos; defesa de emboscada. | Reaproveitar |
| C2 | Perspective Distortion | 2016 (C2 histórico) | Aliados atacam além do alcance; com bots, passa a ter destinatários. | Conferir alvo e alcance |
| C3 | Scan | 2016 (C3 histórico) | Reconhecimento: revela itens e inimigos escondidos. | Conferir utilidade no mapa |

Camouflage desce de C1 para C2 para abrir espaço ao núcleo de adaga e pistola. Perspective Distortion e Scan mantêm seus círculos históricos.

**Ficam no acervo de 2016:** Flu Flu, Flare Shot e Split Arrow, por dependerem de arco. Adaptar Flare Shot para pistola seria criar uma habilidade nova, o que esta etapa evita.

## O que continua aberto

- Conferir no cliente as animações de Cloaking e Camouflage e a utilidade de Perspective Distortion e Scan com bots.
- Confirmar a compatibilidade de espada de uma mão, adaga e pistola com cada especialização da árvore.
- Números, atributos, níveis e requisitos ficam para depois da seleção.

**Próximo passo recomendado:** especificar o grupo de bots, já que Perspective Distortion e Cloaking dependem dele.
