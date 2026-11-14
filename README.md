# Tree of Savior · Memórias do lançamento

**Uma wiki em português para reencontrar o mundo de Tree of Savior em 2016.**

**[Abrir versão navegável](https://karasul-git.github.io/Tree_of_Savior_Solo/)** · [Gerar e publicar o site](docs/versao-navegavel.md)

Classes, caminhos e pequenas descobertas de uma época em que atravessar o próximo mapa já era uma aventura. Este acervo nasce para preservar essa experiência e, futuramente, servir de referência para um projeto solo.

## Escolha seu primeiro caminho

| Swordsman | Wizard | Archer | Cleric |
| :---: | :---: | :---: | :---: |
| [![Emblema de Swordsman](docs/assets/classes/swordsman/icon.png)](docs/classes/swordsman.md) | [![Emblema de Wizard](docs/assets/classes/wizard/icon.png)](docs/classes/wizard.md) | [![Emblema de Archer](docs/assets/classes/archer/icon.png)](docs/classes/archer.md) | [![Emblema de Cleric](docs/assets/classes/cleric/icon.png)](docs/classes/cleric.md) |
| Combate próximo | Magia e controle | Ataques à distância | Cura e proteção |
| [Abrir ficha](docs/classes/swordsman.md) | [Abrir ficha](docs/classes/wizard.md) | [Abrir ficha](docs/classes/archer.md) | [Abrir ficha](docs/classes/cleric.md) |

## Explore a wiki

- [Classes: índice da primeira edição](docs/classes/README.md)
- [Como funcionavam ranks e círculos](docs/sistemas/ranks-e-circulos.md)
- [Qual lançamento estamos documentando?](docs/recorte-historico.md)
- [Fontes e critérios de pesquisa](docs/fontes.md)
- [Acervo das imagens originais](docs/imagens.md)
- [Próximas etapas](ROADMAP.md)

## Primeira edição · Classes iniciais

Esta entrega reúne as quatro classes de Rank 1, **30 habilidades organizadas por círculo** e imagens oficiais preservadas localmente. As fichas são uma reconstrução documental inicial, com pendências visíveis. Ainda não incluem todas as classes avançadas, valores numéricos, atributos e mudanças de cada patch.

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
