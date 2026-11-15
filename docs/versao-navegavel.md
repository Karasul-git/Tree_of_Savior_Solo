# A versão navegável

[Início](../README.md)

A wiki também é um site estático, gerado a partir dos documentos Markdown do repositório. Inclui navegação entre as classes, índice dentro dos artigos, busca por conteúdo e galerias de imagens oficiais.

A visão solo organiza Warrior, Archer e Scout por rank, com filtro e quadro de cobertura. Ela distingue ranks de referência e ranks propostos. A visão de 2016 mantém suas quatro árvores; Assassin e Outlaw possuem fichas posteriores separadas. [Pesquisa e decisões de organização](projeto-solo/ranks-e-especializacoes.md).

[Versão navegável privada no Sites](https://tos-solo-memorias-karasul.lucas-sousa-caldas.chatgpt.site). Essa publicação exige a conta proprietária. O conteúdo-fonte permanece no repositório público informado pelo proprietário.

## Publicação pelo GitHub Pages

O workflow `Publicar wiki navegável` valida documentos, gera o site, testa a busca e publica `dist/`. A configuração esperada em **Settings → Pages → Build and deployment → Source** é **GitHub Actions**.

Enquanto Pages estiver desabilitado, o workflow confere o site e pula a publicação. Depois de habilitar a opção, execute **Actions → Publicar wiki navegável → Run workflow**; as próximas alterações em `main` também publicarão automaticamente.

O endereço padrão do projeto é `https://karasul-git.github.io/Tree_of_Savior_Solo/`. A publicação efetiva deve ser conferida no resultado do workflow antes de anunciar o site como disponível.

## Gerar no computador

Requer Node.js 22 ou posterior e Python 3.10 ou posterior.

```bash
npm ci --ignore-scripts
npm run check
npm test
```

Abra `dist/index.html` para navegar localmente. Todos os recursos de interface, imagens e índice da busca estão na pasta gerada; o funcionamento básico não depende de serviços de terceiros. Links para fontes externas continuam exigindo internet. Abrir por um servidor HTTP estático também funciona.

## Atualizar conteúdo

Edite os arquivos em `docs/`, o `README.md` ou o `ROADMAP.md`. O gerador recria as páginas a partir desses documentos. As imagens e seus créditos continuam sob controle do manifesto original em `data/assets.json`.

Os arquivos de apresentação ficam em `web/`. A lista de documentos da navegação e os textos da capa ficam em `scripts/build-site.mjs`. Ao acrescentar uma ficha, atualize essa lista e a navegação; não mantenha uma cópia manual divergente do artigo em HTML.

A verificação local cobre links, âncoras, integridade das imagens e buscas sobre o conteúdo real. Ela não substitui revisão histórica ou inspeção visual em navegador.
