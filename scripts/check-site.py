"""Check every local HTML target, fragment and asset in the static output."""
from html.parser import HTMLParser
import json
from pathlib import Path
from urllib.parse import urlparse, unquote

ROOT = Path(__file__).resolve().parents[1] / 'dist'


class Page(HTMLParser):
    def __init__(self, file):
        super().__init__()
        self.file = file
        self.links = []
        self.ids = set()
        self.h1 = 0
        self.has_title = False
        self.language = None
        self.feed(file.read_text())

    def handle_starttag(self, tag, attrs):
        attrs = dict(attrs)
        if 'id' in attrs:
            assert attrs['id'] not in self.ids, f'Duplicate ID {attrs["id"]} in {self.file}'
            self.ids.add(attrs['id'])
        if tag == 'html':
            self.language = attrs.get('lang')
        if tag == 'h1':
            self.h1 += 1
        if tag == 'title':
            self.has_title = True
        if tag in ['a', 'link'] and attrs.get('href'):
            self.links.append(attrs['href'])
        if tag in ['img', 'script'] and attrs.get('src'):
            self.links.append(attrs['src'])
        if tag == 'img':
            assert 'alt' in attrs, f'Missing alt text in {self.file}'


pages = {file.resolve(): Page(file) for file in ROOT.rglob('*.html')}
checked = 0
for file, page in pages.items():
    assert page.h1 == 1, f'Expected one page heading in {file}'
    assert page.has_title and page.language == 'pt-BR', f'Missing metadata: {file}'
    for link in page.links:
        parts = urlparse(link)
        if parts.scheme or parts.netloc:
            continue
        target = (file.parent / unquote(parts.path)).resolve() if parts.path else file
        if target.is_dir():
            target = target / 'index.html'
        assert target.is_relative_to(ROOT), f'Link escapes output: {file} -> {link}'
        assert target.exists(), f'Broken site link: {file} -> {link}'
        if parts.fragment and target.suffix == '.html':
            assert unquote(parts.fragment) in pages[target].ids, f'Missing fragment: {file} -> {link}'
        checked += 1

manifest = json.loads((ROOT / 'data/assets.json').read_text())
for image in manifest:
    assert (ROOT / image['path']).read_bytes() == (ROOT.parent / image['path']).read_bytes(), image['path']
for image in json.loads((ROOT.parent / 'data/boss-assets.json').read_text()):
    assert (ROOT / image['path']).read_bytes() == (ROOT.parent / image['path']).read_bytes(), image['path']
assert (ROOT / '.nojekyll').exists()
print(f'OK: {len(pages)} HTML pages; {checked} local links/assets/fragments; original images unchanged.')
