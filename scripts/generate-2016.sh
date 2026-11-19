#!/bin/sh
# Gera todas as páginas do "Original de 2016" na ordem certa (os itens vêm primeiro: os outros usam o índice deles).
set -e
cd "$(dirname "$0")/.."
python3 scripts/generate-item-pages-2016.py
python3 scripts/generate-world2-pages-2016.py
python3 scripts/generate-world-pages-2016.py
python3 scripts/generate-monster-pages-2016.py
