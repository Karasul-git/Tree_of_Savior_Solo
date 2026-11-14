"""Preserve original IMC class images, with provenance and SHA-256 hashes.

Run explicitly to refresh the collection; normal validation never accesses the network.
"""
import hashlib
from html.parser import HTMLParser
import json
from pathlib import Path
from datetime import datetime, timezone
from urllib.parse import urljoin, urlparse
from urllib.request import Request, urlopen

ROOT = Path(__file__).resolve().parents[1]
ORIGIN = 'https://treeofsavior.com'


class Images(HTMLParser):
    def __init__(self):
        super().__init__()
        self.paths = []

    def handle_starttag(self, tag, attrs):
        attrs = dict(attrs)
        if tag == 'img' and attrs.get('src'):
            self.paths.append(attrs['src'])


def fetch(url):
    if urlparse(url).hostname != 'treeofsavior.com':
        raise ValueError('Unexpected asset origin: ' + url)
    request = Request(url, headers={'User-Agent': 'ToSClassicWiki/0.1 documentary research'})
    with urlopen(request, timeout=45) as response:
        return response.read(), response.headers.get('Content-Type', '')


def main():
    manifest = []
    for name in ['Swordsman', 'Wizard', 'Archer', 'Cleric']:
        slug = name.lower()
        page_url = f'{ORIGIN}/page/class/view.php?c={name}'
        body, _ = fetch(page_url)
        parser = Images()
        parser.feed(body.decode('utf-8'))
        candidates = {
            'icon': [p for p in parser.paths if '/class_icon/' in p],
            'female': [p for p in parser.paths if '/class_character/' in p and p.endswith('_f.gif')],
            'male': [p for p in parser.paths if '/class_character/' in p and p.endswith('_m.gif')],
        }
        for role, paths in candidates.items():
            if len(paths) != 1:
                raise ValueError(f'{name}/{role}: expected exactly one original image, got {paths}')
            url = urljoin(ORIGIN, paths[0])
            raw, content_type = fetch(url)
            extension = '.png' if role == 'icon' else '.gif'
            expected = b'\x89PNG\r\n\x1a\n' if role == 'icon' else b'GIF8'
            if not raw.startswith(expected):
                raise ValueError('Unexpected image file: ' + url)
            relative = f'docs/assets/classes/{slug}/{role}{extension}'
            dest = ROOT / relative
            dest.parent.mkdir(parents=True, exist_ok=True)
            dest.write_bytes(raw)
            manifest.append(dict(class_id=slug, role=role, path=relative,
                                 source_page=page_url, source_url=url,
                                 retrieved_at=datetime.now(timezone.utc).isoformat(),
                                 sha256=hashlib.sha256(raw).hexdigest(), bytes=len(raw),
                                 content_type=content_type, rights_holder='IMC Games Co., Ltd.',
                                 transformations='none',
                                 historical_status='official-current-asset; 2016 identity unverified'))
            print(relative, len(raw), 'bytes', flush=True)
    (ROOT / 'data/assets.json').write_text(json.dumps(manifest, ensure_ascii=False, indent=2) + '\n')


if __name__ == '__main__':
    main()
