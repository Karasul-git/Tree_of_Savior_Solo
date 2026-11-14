# Imagens originais e sua procedência

[Início](../README.md)

Esta edição contém **12 arquivos oficiais**: um emblema e dois sprites animados por classe inicial. São cópias locais dos arquivos servidos pela IMC Games, sem redesenho, geração por IA, corte ou recompressão. Isso evita depender de links de imagens em sites de terceiros para ler a wiki.

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
