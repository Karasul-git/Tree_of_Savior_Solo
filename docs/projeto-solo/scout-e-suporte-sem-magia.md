# Scout, suporte físico e cura sem magia

[Diretrizes solo](diretrizes.md) · [Catálogo histórico](../classes/catalogo-2016.md) · [Fontes](../fontes.md)

**Estudo de 15/11/2026.** Scout como classe base e árvore própria é uma decisão do projeto. A seleção aprovada está abaixo; as transformações de habilidades continuam propostas de design, apoiadas por referências históricas e posteriores identificadas separadamente.

**Orientação posterior do proprietário:** reaproveitar o máximo do original. Poções existentes são o ponto de partida para recuperação; a necessidade de habilidades adicionais ainda não foi demonstrada. As conversões deste estudo são alternativas exploratórias. A próxima etapa passa a ser revisar recursos originais e a organização das classes, antes de criar novos kits.

## Resultado da revisão do proprietário

- **Alchemist e Plague Doctor entram em Scout.** A seleção de classe está aprovada; os detalhes de adaptação não estão definidos.
- **Linker e Thaumaturge saem da seleção solo.** As sugestões anteriores de convertê-los em outros ofícios ficam descartadas nesta etapa.
- **Enchanter permanece fora**, pelo critério de magia. Classes posteriores serão avaliadas em outra oportunidade.
- **Templar permanece no ramo de Swordsman**, para o qual **Warrior** é o nome provisório sugerido. Ele já pertencia a esse ramo em 2016; a decisão preserva sua posição e abre a discussão do nome da base.

O catálogo histórico mantém os nomes e as árvores originais. [Seleção e pendências consolidadas](diretrizes.md).

## A separação de Scout pode ser aproveitada

No Re:Build, Scout passou a reunir adagas, pistolas e suporte ao grupo, com classes transferidas de outras árvores. A documentação oficial também distingue esse suporte da cura e da religião. Portanto, Scout oferece uma boa referência de organização, mas sua criação não resolve sozinha a recuperação de HP. [F12](../fontes.md#f12)

| Especialização presente no catálogo de 2016 | Árvore de 2016 | Referência posterior |
| --- | --- | --- |
| Scout | Archer, Rank 4 | Base de Scout. |
| Rogue e Schwarzer Reiter | Archer | Agrupadas em Scout. |
| Corsair, Squire e Shinobi | Swordsman | Agrupadas em Scout. |
| Linker e Thaumaturge | Wizard | Agrupadas em Scout, mantendo habilidades de origem mágica. |

A composição posterior é documentada em [F96](../fontes.md#f96). Assassin, Outlaw, Bullet Marker e Enchanter também pertencem à referência de Re:Build, mas ficam fora do catálogo de lançamento. Sua presença nessa referência não decide sua inclusão solo. [F12](../fontes.md#f12) · [F96](../fontes.md#f96)

**Escolher essa organização não obriga a copiar o sistema de avanço de 2019.** Podemos estudar uma árvore Scout com ranks/círculos adaptados. Sua viabilidade no cliente e no servidor escolhidos ainda precisa de confirmação.

## Quais suportes físicos já temos?

| Classe | Base documental de 2016 | Papel aproveitável | Limite para cura |
| --- | --- | --- | --- |
| [Squire](../classes/catalogo/squire.md) | Reparos, manutenção, refeições e acampamento. | Preparação e recuperação entre confrontos. | Refeições de recuperação não equivalem a uma cura imediata durante a luta. |
| [Peltasta](../classes/catalogo/peltasta.md) | Provocação, bloqueio e escudo. | Absorver pressão e proteger a posição. | Não repõe diretamente o HP perdido. |
| [Rodelero](../classes/catalogo/rodelero.md) | Interrupções, deslocamento e enfraquecimento. | Evitar ataques e abrir oportunidades. | Controle não substitui recuperação. |
| [Corsair](../classes/catalogo/corsair.md) | Gancho, bandeira e pilhagem. | Controle e apoio econômico/ofensivo. | Não é curador. |
| [Templar](../classes/catalogo/templar.md) | Battle Orders fortalece STR de membros próximos da guilda. | Comando e reforço coletivo. | Era voltado a guildas; utilidade e condições no solo precisam ser avaliadas. |
| [Hunter](../classes/catalogo/hunter.md) e [Falconer](../classes/catalogo/falconer.md) | Comandos de companheiros, controle e apoio em área. | Reduzir pressão e facilitar ataques. | Dependem dos companheiros; não oferecem um kit de cura. |
| [Wugushi](../classes/catalogo/wugushi.md) | Venenos e Detoxify. | Especialista em toxinas e antídotos. | Remover veneno não recupera automaticamente HP nem limpa todos os estados. |

Fontes: [F46](../fontes.md#f46), [F40](../fontes.md#f40), [F44](../fontes.md#f44), [F45](../fontes.md#f45), [F69](../fontes.md#f69), [F75](../fontes.md#f75) e [F72](../fontes.md#f72). O guia contemporâneo descreve alimentos de Squire ligados a HP/SP máximos e intervalos de recuperação. Esses efeitos têm funções diferentes de uma cura direta. [F03](../fontes.md#f03)

Mesmo classes físicas exigem revisão temática: o teleporte de acampamento de Squire e os transportes de guilda de Templar não se tornam mundanos apenas porque sua árvore era Swordsman. Também há habilidades de pouco uso solo que precisariam de outra função. [F46](../fontes.md#f46) · [F50](../fontes.md#f50)

## Alchemist é um candidato forte para cura por itens

Em 2016, Alchemist já tinha produção de preparados por Tincturing e serviços de materiais/equipamentos. Seu kit também continha elementos mágicos, como despertar um item por uma dimensão de combate: a adaptação precisa selecionar habilidades, e não apenas trocar o nome da árvore. [F61](../fontes.md#f61)

Há um precedente posterior particularmente útil: o FAQ oficial de janeiro de 2019 registra a adição de **Sprinkle HP Potion** e **Sprinkle SP Potion** para reforçar o suporte de Alchemist. A página oficial atual descreve aplicação de poções em si e nos aliados. São referências posteriores, não habilidades certificadas do lançamento. [F97](../fontes.md#f97) · [F98](../fontes.md#f98)

**Proposta solo, ainda em estudo:** um alquimista/boticário fabrica e aplica preparados, fornece recuperação e usa frascos ofensivos. Ingredientes, recargas, tempo de aplicação e capacidade de transporte limitariam sua eficiência. A explicação é ficcional e coerente com itens; não exige simular química real.

## O que pode ser ressignificado?

| Origem | Possível identidade solo | O que aproveitar | O que realmente precisa mudar |
| --- | --- | --- | --- |
| Alchemist | Alquimista / boticário | Preparados, coleta, tratamento de materiais e serviços. | Remover projéteis de energia e efeitos dimensionais ou substituí-los por outras habilidades. |
| Plague Doctor | Médico de campo | Máscara, fumigação, prevenção e tratamento de estados. | Cura instantânea sobrenatural e propagação de efeitos exigem redesenho; curativos e aplicação de preparados são propostas novas. |
| Priest / Cleric | Socorrista | Função de recuperação e proteção. | Círculos sagrados e ressurreição não se preservam por renomeação; eventual reanimação precisaria de regras próprias. |
| Dievdirbys | Artesão de dispositivos | Instalar estruturas para sustentar uma posição. | Transporte divino, invulnerabilidade e estátuas ofensivas mágicas exigem revisão profunda. |

As colunas de identidade e adaptação são sugestões deste estudo. As funções originais estão nas fichas [Alchemist](../classes/catalogo/alchemist.md), [Plague Doctor](../classes/catalogo/plague-doctor.md), [Priest](../classes/catalogo/priest.md), [Cleric](../classes/catalogo/cleric.md) e [Dievdirbys](../classes/catalogo/dievdirbys.md). Somente Alchemist e Plague Doctor foram selecionados para Scout; os outros exemplos não são inclusões aprovadas. Uma animação reaproveitável não garante um efeito adequado à nova proposta.

**Prioridade revisada:** consolidar a árvore solo e reaproveitar habilidades de suporte e consumíveis originais. Alchemist e Plague Doctor ficam em Scout como classes independentes. Uma eventual relação de progressão entre eles seria uma decisão adicional; não existia como sequência de classes em 2016. [F61](../fontes.md#f61) · [F92](../fontes.md#f92)

## Como a cura pode funcionar num jogo solo?

O jogo já possuía poções de HP e SP no período: a discussão contemporânea em F99 menciona esses consumíveis comuns. Primeiro devemos conferir disponibilidade, requisitos e recuperação na edição escolhida, e testar sua suficiência para as builds solo. [F99](../fontes.md#f99)

As ideias abaixo permanecem como alternativas, sem números ou implantação definidos:

- **Recursos básicos para todos:** poções, comida e descanso acessíveis por exploração, receitas e comércio. Nenhuma build deveria exigir um personagem alternativo alquimista.
- **Especialização de cura:** maior eficiência, aplicação em aliados e variedade de preparados, sem tornar a classe obrigatória para sobreviver.
- **Recuperação em combate:** poções limitadas por recarga; curativos podem exigir tempo e ser interrompidos; antídotos tratam estados específicos.
- **Recuperação entre lutas:** comida, descanso e acampamento favorecem expedições pelo mundo aberto.
- **Benefício solo:** o suporte funciona no próprio usuário; efeitos coletivos podem alcançar companheiros ou aliados NPC se o projeto vier a adotá-los.

Essas regras precisam ser testadas contra duração dos confrontos, dano recebido, preço dos ingredientes e disponibilidade de recursos no mapa. Cura excessiva elimina o risco; cura escassa obriga uma especialização ou interrompe a exploração constantemente.

## O estudo necessário antes de implementar Scout

1. Mapear árvore original, árvore de referência e destino solo de cada classe; separar decisões de candidaturas.
2. Escolher o modelo de avanço: ranks/círculos adaptados ou outra progressão. A separação visual de árvores não decide isso.
3. Conferir no cliente e servidor escolhidos os IDs de classe base, criação de personagem, interface de avanço e restrições por árvore.
4. Revisar armas, posturas, atributos, animações, equipamentos e dependências entre habilidades.
5. Ajustar mestres, missões, dados salvos e desbloqueios ao novo caminho.
6. Prototipar uma build Scout e uma fonte de recuperação; testar autonomia no mundo aberto antes de migrar toda a árvore.

Não há implementação de gameplay nesta atualização da wiki. Sem o cliente e a base técnica definidos, a viabilidade de cada mudança continua pendente.

**Próximo passo recomendado:** revisar a distribuição provisória nas [árvores solo](../classes/catalogo-2016.md), ajustando as posições propostas. Depois, comparar habilidades originais de Alchemist e Plague Doctor para escolher o reaproveitamento com menos alterações.
