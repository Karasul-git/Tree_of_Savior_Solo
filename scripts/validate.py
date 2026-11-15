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
    def check_evidence(value):
        if isinstance(value, dict):
            for key, nested in value.items():
                if key == 'evidence_ids' or key.endswith(('_evidence_ids', '_source_ids')):
                    for source_id in nested:
                        if source_id not in source_ids:
                            errors.append(f'Unknown class evidence in {class_file.name}: {source_id}')
                else:
                    check_evidence(nested)
        elif isinstance(value, list):
            for nested in value:
                check_evidence(nested)
    check_evidence(record)
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

equipment_research = ROOT / 'data/research/swordsman-equipment-advancement.json'
if equipment_research.exists():
    research = json.loads(equipment_research.read_text())
    for entry in research['files']:
        if entry['source_id'] not in source_ids or not re.fullmatch('[a-f0-9]{64}', entry['sha256']):
            errors.append('Invalid equipment/advancement provenance')
        if entry.get('commit_sha') and not re.fullmatch('[a-f0-9]{40}', entry['commit_sha']):
            errors.append('Invalid translation revision')
        if entry.get('captured_at'):
            date.fromisoformat(entry['captured_at'][:10])
        date.fromisoformat(entry['retrieved_at'])

catalog_path = ROOT / 'data/catalog/classes-2016.json'
catalog = json.loads(catalog_path.read_text())
catalog_classes = catalog['classes']
catalog_ids = [entry['id'] for entry in catalog_classes]
if len(catalog_ids) != len(set(catalog_ids)) or len(catalog_ids) != catalog['class_count']:
    errors.append('Invalid catalog class count or duplicate class ID')
if set(catalog['trees']) != {'Swordsman', 'Wizard', 'Archer', 'Cleric'}:
    errors.append('Unexpected launch tree set')
skill_count = 0
for entry in catalog_classes:
    rank = entry['first_rank']
    if entry['tree'] not in catalog['trees'] or rank not in range(1, 8):
        errors.append(f'Invalid launch tree/rank: {entry["name"]}')
    cap = 1 if entry['hidden'] else min(3, 8 - rank)
    if entry['max_circle_in_catalog'] != cap:
        errors.append(f'Invalid catalog circle cap: {entry["name"]}')
    date.fromisoformat(entry['captured_at'][:10])
    if not ('2016-03-29' <= entry['captured_at'][:10] <= '2016-04-30'):
        errors.append(f'Catalog capture outside documentary window: {entry["name"]}')
    if not (ROOT / f'docs/classes/catalogo/{entry["id"]}.md').exists():
        errors.append(f'Missing catalog page: {entry["name"]}')
    skills = entry['skills']
    if not skills or len({skill['tosbase_id'] for skill in skills}) != len(skills):
        errors.append(f'Invalid catalog skill set: {entry["name"]}')
    for record in [entry, *skills]:
        if not record['summary'] or any(source not in source_ids for source in record['evidence_ids']):
            errors.append(f'Missing catalog summary/evidence: {entry["name"]}')
    for skill in skills:
        if skill['circle'] not in range(1, cap + 1):
            errors.append(f'Post-rank-7 skill in catalog: {entry["name"]}/{skill["name"]}')
    skill_count += len(skills)
if skill_count != catalog['skill_count']:
    errors.append('Catalog skill count mismatch')
for snapshot in json.loads((ROOT / 'data/research/catalog-snapshots.json').read_text()):
    if snapshot['source_id'] not in source_ids or not re.fullmatch('[a-f0-9]{64}', snapshot['html_sha256']):
        errors.append('Invalid catalog snapshot provenance')

pages = list(ROOT.glob('*.md')) + list((ROOT / 'docs').rglob('*.md'))
for page in pages:
    body = re.sub(r'```.*?```', '', page.read_text(), flags=re.S)
    for source_id in re.findall(r'\[(F\d{2,})\]', body):
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
expected = {(name, role) for name in catalog_ids
            for role in ['icon', 'female', 'male']}
actual = {(asset['class_id'], asset['role']) for asset in assets}
if actual != expected or len(assets) != len(expected):
    errors.append('Expected an icon and both character sprites for every catalog class')
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
