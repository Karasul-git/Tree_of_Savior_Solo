# Imagens originais e sua procedência

[Início](../README.md)

Esta edição contém **165 arquivos oficiais**: um emblema e dois sprites animados para cada uma das 55 classes do catálogo. São cópias locais dos arquivos servidos pela IMC Games, sem redesenho, geração por IA, corte ou recompressão. Isso evita depender de links de imagens em sites de terceiros para ler a wiki.

O [catálogo completo](classes/catalogo-2016.md?view=2016) dá acesso aos visuais de todas as especializações. As quatro árvores iniciais permanecem abaixo como atalhos.

| Classe | Página de origem | Arquivos locais |
| --- | --- | --- |
| Swordsman | [Site oficial](https://treeofsavior.com/page/class/view.php?c=Swordsman) | [Pasta](assets/classes/swordsman/) |
| Wizard | [Site oficial](https://treeofsavior.com/page/class/view.php?c=Wizard) | [Pasta](assets/classes/wizard/) |
| Archer | [Site oficial](https://treeofsavior.com/page/class/view.php?c=Archer) | [Pasta](assets/classes/archer/) |
| Cleric | [Site oficial](https://treeofsavior.com/page/class/view.php?c=Cleric) | [Pasta](assets/classes/cleric/) |

**Original da desenvolvedora não significa automaticamente comprovado em 2016.** A origem oficial é verificável. A versão histórica do arquivo ainda precisa ser comparada com um snapshot datado do site ou com um cliente da edição. Por enquanto, todas essas imagens têm a data histórica marcada como pendente; as mecânicas atuais das páginas de origem não foram usadas como prova de 2016.

O [manifesto de arquivos](../data/assets.json) registra classe, função, URL da página, URL direta, momento da coleta, tamanho, SHA-256 e titular dos direitos. O hash confirma a integridade da cópia coletada, não sua antiguidade.

## Preservação

```bash
# Confere os arquivos existentes, sem acessar a rede.
python3 scripts/validate.py

# Atualiza explicitamente as cópias oficiais e o manifesto.
python3 scripts/fetch_images.py
```

Atualizações das imagens devem ser revistas no diff. Não substituir o acervo silenciosamente por ilustrações de outra época. Capturas de clientes antigos, quando disponíveis, receberão edição, região e origem próprias.

## Créditos

Tree of Savior, personagens, emblemas e sprites são de **IMC Games Co., Ltd.** A inclusão no repositório não altera os direitos sobre esses materiais, não os coloca em domínio público e não implica vínculo oficial com a desenvolvedora.

## Classes posteriores pesquisadas

Assassin e Outlaw acrescentam seis arquivos oficiais originais (emblemas e dois visuais por classe), preservados sem alterações: **171 arquivos naquela etapa**, dos quais 165 pertencem às referências do catálogo de 2016. As novas classes são de 2019; seus arquivos vieram do site oficial atual em 15/11/2026, sem confirmação de identidade exata em 2019. A procedência e os hashes estão no mesmo manifesto. Fontes: [F101](fontes.md#f101) e [F102](fontes.md#f102).

## Seis candidatas escolhidas: nova etapa visual

Appraiser, Mergen, Arbalester, Ardito, Sheriff e Clown acrescentam 18 arquivos oficiais (emblema e dois visuais por classe), preservados sem alterações. O acervo atual reúne **189 arquivos originais**: 165 das referências de 2016 e 24 das oito referências posteriores. Arquivos das novas candidatas obtidos em 15/11/2026, sem prova de identidade com versões antigas. [Estudo visual e habilidades](projeto-solo/seis-classes-habilidades-e-circulos.md).
