# Tree of Savior · Memórias do lançamento

**Uma wiki em português para reencontrar o mundo de Tree of Savior em 2016.**

**[Abrir versão navegável privada](https://tos-solo-memorias-karasul.lucas-sousa-caldas.chatgpt.site)** · [Gerar e publicar o site](docs/versao-navegavel.md)

GitHub Pages está preparado, mas aguarda ativação pelo proprietário em **Settings → Pages → GitHub Actions**. A integração usada neste projeto não tem permissão para habilitar essa configuração.

Classes, caminhos e pequenas descobertas de uma época em que atravessar o próximo mapa já era uma aventura. Este acervo nasce para preservar essa experiência e, futuramente, servir de referência para um projeto solo.

**Direção solo:** remover classes mágicas. O catálogo preserva as classes originais para consulta; a seleção nominal e os casos híbridos estão nas [diretrizes do projeto](docs/projeto-solo/diretrizes.md).

## Explore todas as classes

**[Abrir catálogo de 2016](docs/classes/catalogo-2016.md)** — 55 classes, 382 habilidades com resumos em português e 165 imagens originais. A versão navegável permite filtrar por árvore/rank, buscar habilidades e alternar os visuais.

## As quatro árvores históricas

| Swordsman | Wizard | Archer | Cleric |
| :---: | :---: | :---: | :---: |
| [![Emblema de Swordsman](docs/assets/classes/swordsman/icon.png)](docs/classes/swordsman.md) | [![Emblema de Wizard](docs/assets/classes/wizard/icon.png)](docs/classes/wizard.md) | [![Emblema de Archer](docs/assets/classes/archer/icon.png)](docs/classes/archer.md) | [![Emblema de Cleric](docs/assets/classes/cleric/icon.png)](docs/classes/cleric.md) |
| Combate próximo | Magia e controle | Ataques à distância | Cura e proteção |
| [Abrir ficha](docs/classes/swordsman.md) | [Abrir ficha](docs/classes/wizard.md) | [Abrir ficha](docs/classes/archer.md) | [Abrir ficha](docs/classes/cleric.md) |

## Explore a wiki

- [Catálogo: todas as classes e habilidades](docs/classes/catalogo-2016.md)
- [Diretrizes da versão solo](docs/projeto-solo/diretrizes.md)
- [Classes: índice da primeira edição](docs/classes/README.md)
- [Como funcionavam ranks e círculos](docs/sistemas/ranks-e-circulos.md)
- [Qual lançamento estamos documentando?](docs/recorte-historico.md)
- [O que mudou do lançamento ao Re:Build?](docs/historia/evolucao-das-classes.md)
- [Fontes e critérios de pesquisa](docs/fontes.md)
- [Acervo das imagens originais](docs/imagens.md)
- [Próximas etapas](ROADMAP.md)

## Cobertura atual

**Pesquisa de 15/11/2026:** [Swordsman recebeu uma revisão documental](docs/classes/swordsman.md), com nove capturas de 2016, [100 linhas por nível e 19 atributos catalogados](docs/classes/swordsman-valores-2016.md), além de uma comparação separada com o kit atual. A nova pesquisa de [armas, mestres e avanços](docs/classes/swordsman-equipamentos-e-avancos-2016.md) acrescenta quatro listas de equipamento arquivadas e uma revisão histórica da tradução oficial. A [história do sistema de classes](docs/historia/evolucao-das-classes.md) explica a ruptura do Re:Build. Os campos sem confirmação permanecem identificados.

O panorama reúne **55 classes até Rank 7 e 382 habilidades organizadas por círculo**, com 165 imagens oficiais locais. Cada classe tem uma ficha de identidade e funções. Swordsman mantém também a revisão detalhada já feita; números, atributos, armas e missões das outras classes ficam para depois da seleção solo.

O marco principal é a abertura internacional dos servidores de acesso antecipado em **29/03/2016**. Guias de março e abril ajudam a reconstruir a janela de lançamento, mas não comprovam, sozinhos, o estado exato de cada habilidade naquele dia. Informações de beta, kToS e versões posteriores recebem identificação própria.

As imagens vêm da IMC Games, sem geração por IA ou redesenho. A origem oficial está registrada; a identidade byte a byte com arquivos de 2016 ainda não foi comprovada. [Entenda a procedência](docs/imagens.md).

## Manutenção

O conteúdo é Markdown: pode ser lido diretamente no GitHub, clonado e consultado offline. A seção fica nos arquivos do repositório para manter texto, imagens e fontes juntos, sem depender do recurso GitHub Wiki.

```bash
python3 scripts/validate.py

# Gerar e conferir a versão navegável (Node.js 22+)
npm ci --ignore-scripts
npm run check
npm test
```

Veja [como contribuir](CONTRIBUTING.md). Imagens, nomes e marcas de Tree of Savior pertencem aos respectivos titulares; este projeto de fãs não tem vínculo com a IMC Games.
