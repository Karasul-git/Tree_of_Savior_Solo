"""Offline checks for local wiki links, source IDs and preserved original assets."""
from datetime import date
import hashlib
import json
from pathlib import Path
import re
import struct
import sys
from urllib.parse import unquote, urlparse

ROOT = Path(__file__).resolve().parents[1]
errors = []
sources = json.loads((ROOT / 'data/sources.json').read_text())
source_ids = {source['id'] for source in sources}
if len(source_ids) != len(sources):
    errors.append('Duplicate source ID')
for source in sources:
    for field in ['id', 'title', 'author', 'scope', 'kind', 'url', 'supports', 'limitations']:
        if not source.get(field):
            errors.append(f'Missing source field {field}: {source.get("id")}')
    if source.get('published'):
        date.fromisoformat(source['published'])
    elif source.get('accessed'):
        date.fromisoformat(source['accessed'])
    else:
        errors.append(f'Undated source without access date: {source["id"]}')
    if urlparse(source['url']).scheme != 'https':
        errors.append(f'Non-HTTPS source: {source["id"]}')

for class_file in (ROOT / 'data/classes').glob('*.json'):
    record = json.loads(class_file.read_text())
    names = [skill['name'] for skill in record['skills']]
    if len(names) != len(set(names)):
        errors.append(f'Duplicate skill in {class_file.name}')
    archived_count = 0
    for skill in record['skills']:
        for source_id in skill['evidence_ids']:
            if source_id not in source_ids:
                errors.append(f'Unknown skill source: {source_id}')
        levels = skill.get('archived_levels', [])
        if levels:
            if [row['level'] for row in levels] != list(range(1, len(levels) + 1)):
                errors.append(f'Incomplete archived level sequence: {skill["name"]}')
            if skill.get('archive_evidence_id') not in source_ids:
                errors.append(f'Missing archive source: {skill["name"]}')
            for row in levels:
                if row['cooldown_seconds'] < 0 or row['sp_cost'] < 0:
                    errors.append(f'Invalid cooldown/SP: {skill["name"]}')
            archived_count += len(levels)
    if archived_count != record.get('snapshot_table_row_count', 0):
        errors.append(f'Archived row count mismatch: {class_file.name}')
    for attr in record.get('class_page_attribute_catalog', []):
        if attr['circle_displayed'] not in [1, 2, 3] or attr['max_level_displayed'] < 1:
            errors.append(f'Invalid archived attribute: {attr["name"]}')
        for source_id in attr['evidence_ids']:
            if source_id not in source_ids:
                errors.append(f'Unknown attribute source: {source_id}')

snapshot_file = ROOT / 'data/research/swordsman-snapshots.json'
if snapshot_file.exists():
    for snapshot in json.loads(snapshot_file.read_text()):
        if snapshot['source_id'] not in source_ids or not re.fullmatch('[a-f0-9]{64}', snapshot['html_sha256']):
            errors.append('Invalid snapshot provenance')
        date.fromisoformat(snapshot['captured_at'][:10])

pages = list(ROOT.glob('*.md')) + list((ROOT / 'docs').rglob('*.md'))
for page in pages:
    body = re.sub(r'```.*?```', '', page.read_text(), flags=re.S)
    for source_id in re.findall(r'\[(F\d{2})\]', body):
        if source_id not in source_ids:
            errors.append(f'Unknown source {source_id}: {page.relative_to(ROOT)}')
    for target in re.findall(r'!?\[[^\]]*\]\(([^\s)]+)\)', body):
        parsed = urlparse(target)
        if parsed.scheme or parsed.netloc:
            continue
        dest = (page.parent / unquote(parsed.path)).resolve() if parsed.path else page
        if not dest.is_relative_to(ROOT):
            errors.append(f'Link leaves repository: {target}')
        elif not dest.exists():
            errors.append(f'Broken link {page.relative_to(ROOT)} -> {target}')
        elif parsed.fragment and dest.suffix == '.md':
            anchors = set(re.findall(r'<a id="([^"]+)"', dest.read_text()))
            if parsed.fragment not in anchors:
                errors.append(f'Missing explicit anchor: {target}')

manifest_path = ROOT / 'data/assets.json'
assets = json.loads(manifest_path.read_text()) if manifest_path.exists() else []
expected = {(name, role) for name in ['swordsman', 'wizard', 'archer', 'cleric']
            for role in ['icon', 'female', 'male']}
actual = {(asset['class_id'], asset['role']) for asset in assets}
if actual != expected or len(assets) != 12:
    errors.append('Expected exactly 12 class images with unique class/role pairs')
for asset in assets:
    path = (ROOT / asset['path']).resolve()
    if not path.is_relative_to(ROOT) or not path.exists():
        errors.append(f'Missing/invalid asset: {asset["path"]}')
        continue
    raw = path.read_bytes()
    if hashlib.sha256(raw).hexdigest() != asset['sha256'] or len(raw) != asset['bytes']:
        errors.append(f'Asset changed: {asset["path"]}')
    if raw.startswith(b'\x89PNG\r\n\x1a\n'):
        width, height = struct.unpack('>II', raw[16:24])
    elif raw.startswith((b'GIF87a', b'GIF89a')):
        width, height = struct.unpack('<HH', raw[6:10])
    else:
        errors.append(f'Invalid image signature: {asset["path"]}')
        continue
    if min(width, height) < 16:
        errors.append(f'Unexpected image dimensions: {asset["path"]}')
    for field in ['source_page', 'source_url', 'retrieved_at', 'rights_holder', 'historical_status']:
        if not asset.get(field):
            errors.append(f'Missing provenance {field}: {asset["path"]}')
    if asset.get('transformations') != 'none':
        errors.append(f'Expected unchanged original: {asset["path"]}')
    if urlparse(asset['source_url']).hostname != 'treeofsavior.com':
        errors.append(f'Unexpected original-image host: {asset["path"]}')

if errors:
    print('\n'.join(errors))
    sys.exit(1)
print(f'OK: {len(pages)} Markdown pages, {len(sources)} sources, {len(assets)} original images.')
print('Local links, explicit source anchors, image signatures and SHA-256 hashes verified.')
print('These checks do not establish historical accuracy or availability of external links.')
