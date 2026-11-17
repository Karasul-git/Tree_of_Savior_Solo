// Página do simulador de builds: monta o formulário e a tabela de resultados.
// O cálculo está em simulador-core.mjs (as mesmas funções dos testes).
import { ATTRIBUTES, compute, distribute } from './simulador-core.mjs';

const data = window.WIKI_SIM;
const root = document.getElementById('simulador');

const ATTRIBUTE_LABELS = {
  str: ['STR', 'Força'], con: ['CON', 'Constituição'], int: ['INT', 'Inteligência (preparados)'],
  spr: ['SAB', 'Sabedoria (antigo SPR)'], dex: ['DEX', 'Destreza'],
};
const ARMORS = [['leather', 'Couro'], ['cloth', 'Tecido'], ['iron', 'Placa']];
const fmt = (value, digits = 0) => (value === null || value === undefined || Number.isNaN(value))
  ? '–' : Number(value).toLocaleString('pt-BR', { minimumFractionDigits: digits, maximumFractionDigits: digits });

function el(tag, props = {}, ...children) {
  const node = document.createElement(tag);
  for (const [key, value] of Object.entries(props)) {
    if (key === 'class') node.className = value;
    else if (key === 'text') node.textContent = value;
    else if (key.startsWith('on')) node.addEventListener(key.slice(2), value);
    else if (value !== false && value !== null && value !== undefined) node.setAttribute(key, value === true ? '' : value);
  }
  for (const child of children.flat()) if (child !== null && child !== undefined) node.append(child);
  return node;
}

function fromPreset(preset, level = 120) {
  return {
    base: preset.base, level, main: preset.main, off: preset.off, armor: preset.armor,
    weaponGrade: 1, sameGear: true, gearLevel: level, skillLevel: data.constants.reference_skill_level,
    points: distribute(preset.pct, level - 1),
  };
}

function blank(level = 120, base = 'warrior') {
  return {
    base, level, main: 'Sword', off: 'none', armor: 'leather', weaponGrade: 1, sameGear: true, gearLevel: level,
    skillLevel: data.constants.reference_skill_level, points: Object.fromEntries(ATTRIBUTES.map(k => [k, 0])),
  };
}

const state = { builds: [blank()], compare: false };

function loadHash() {
  try {
    const raw = new URLSearchParams(location.hash.slice(1)).get('s');
    if (!raw) return;
    const saved = JSON.parse(decodeURIComponent(raw));
    if (!Array.isArray(saved.builds) || !saved.builds.length) return;
    const clean = build => {
      const level = Math.min(data.constants.max_level, Math.max(1, Math.round(Number(build.level) || 1)));
      const points = Object.fromEntries(ATTRIBUTES.map(k => [k, Math.max(0, Math.round(Number(build.points?.[k]) || 0))]));
      if (!data.bases[build.base] || !data.weapon_families.some(f => f.id === build.main)) throw new Error('inválido');
      return {
        base: build.base, level, points, main: build.main,
        off: ['none', 'shield', 'Dagger', 'Pistol'].includes(build.off) ? build.off : 'none',
        armor: ARMORS.some(([id]) => id === build.armor) ? build.armor : 'leather',
        weaponGrade: build.weaponGrade === 2 ? 2 : 1, sameGear: build.sameGear !== false,
        gearLevel: Math.min(315, Math.max(1, Math.round(Number(build.gearLevel) || level))),
        skillLevel: Math.min(data.constants.skill_max_level, Math.max(1, Math.round(Number(build.skillLevel) || 1))),
      };
    };
    state.builds = saved.builds.slice(0, 2).map(clean);
    state.compare = state.builds.length === 2;
  } catch (error) { /* link inválido: fica o modelo inicial */ }
}

function saveHash() {
  try {
    const text = encodeURIComponent(JSON.stringify({ builds: state.compare ? state.builds : state.builds.slice(0, 1) }));
    history.replaceState(null, '', '#s=' + text);
  } catch (error) { /* sem histórico: ignora */ }
}

function isTwoHanded(id) { return data.weapon_families.find(f => f.id === id)?.two_handed; }

function effective(build) {
  const gearLevel = build.sameGear ? build.level : build.gearLevel;
  const off = isTwoHanded(build.main) ? 'none' : build.off;
  return { ...build, gearLevel, off };
}

function setLevel(build, level) {
  const next = Math.min(data.constants.max_level, Math.max(1, Math.round(level) || 1));
  const used = ATTRIBUTES.reduce((sum, k) => sum + (build.points[k] || 0), 0);
  // Sem pontos distribuídos, continua sem pontos; senão mantém as proporções.
  if (used > 0) build.points = distribute(build.points, (next - 1) * data.constants.points_per_level);
  build.level = next;
}

function field(label, control, hint) {
  return el('label', { class: 'sim-field' }, el('span', { text: label }), control, hint ? el('small', { text: hint }) : null);
}

function select(options, value, onChange) {
  const node = el('select', { onchange: event => onChange(event.target.value) },
    options.map(([id, text]) => el('option', { value: id, text, selected: id === value })));
  return node;
}

function numberInput(value, min, max, onChange, extra = {}) {
  return el('input', { type: 'number', inputmode: 'numeric', min, max, value, ...extra, onchange: event => onChange(Number(event.target.value)) });
}

function buildCard(index) {
  const build = state.builds[index];
  const eff = effective(build);
  const result = compute(data, eff);
  const left = result.pointsTotal - result.pointsUsed;
  const card = el('section', { class: 'sim-card', 'aria-label': `Build ${index ? 'B' : 'A'}` });
  const rerender = () => { saveHash(); render(); };

  card.append(el('div', { class: 'sim-card-head' }, el('h3', { text: `Build ${index ? 'B' : 'A'}` }),
    el('span', { class: 'sim-card-actions' },
      el('button', { type: 'button', class: 'sim-button small', onclick: () => { build.points = Object.fromEntries(ATTRIBUTES.map(k => [k, 0])); rerender(); }, text: 'Limpar pontos' }),
      el('button', { type: 'button', class: 'sim-button small', onclick: () => { state.builds[index] = blank(build.level, build.base); rerender(); }, text: 'Limpar tudo' }))));
  card.append(field('Começar de um modelo', select([['', 'Escolha um modelo…'], ...data.presets.map(p => [p.id, p.label])], '', id => {
    const preset = data.presets.find(p => p.id === id);
    if (preset) { state.builds[index] = fromPreset(preset, build.level); rerender(); }
  })));
  card.append(el('div', { class: 'sim-row' },
    field('Base', select(Object.entries(data.bases).map(([id, b]) => [id, b.name]), build.base, v => { build.base = v; rerender(); })),
    field('Nível', numberInput(build.level, 1, data.constants.max_level, v => { setLevel(build, v); rerender(); })),
    field('Nível da habilidade', numberInput(build.skillLevel, 1, data.constants.skill_max_level, v => { build.skillLevel = Math.min(data.constants.skill_max_level, Math.max(1, Math.round(v) || 1)); rerender(); })),
  ));

  const base = data.bases[build.base];
  card.append(el('h4', { text: 'Pontos de atributo' }),
    el('p', { class: `sim-points${left === 0 ? '' : left < 0 ? ' over' : ' left'}`, role: 'status',
      text: left === 0 ? `Todos os ${fmt(result.pointsTotal)} pontos distribuídos.` : left > 0 ? `Faltam distribuir ${fmt(left)} de ${fmt(result.pointsTotal)} pontos.` : `Passou ${fmt(-left)} pontos do limite de ${fmt(result.pointsTotal)}.` }));
  const table = el('table', { class: 'sim-stats' },
    el('thead', {}, el('tr', {}, ['Atributo', 'Inicial', 'Pontos', 'Bônus', 'Total'].map(t => el('th', { scope: 'col', text: t })))),
    el('tbody', {}, ATTRIBUTES.map(k => el('tr', {},
      el('th', { scope: 'row', title: ATTRIBUTE_LABELS[k][1], text: ATTRIBUTE_LABELS[k][0] }),
      el('td', { text: base.stats[k] }),
      el('td', {}, numberInput(build.points[k], 0, (build.level - 1) * data.constants.points_per_level, v => { build.points[k] = Math.max(0, Math.round(v) || 0); rerender(); }, { 'aria-label': `Pontos em ${ATTRIBUTE_LABELS[k][1]}` })),
      el('td', { text: '+' + result.bonus[k] }),
      el('td', { text: fmt(result.stat[k]) })))));
  card.append(table);

  card.append(el('h4', { text: 'Equipamento' }));
  card.append(el('div', { class: 'sim-row' },
    field('Arma principal', select(data.weapon_families.map(f => [f.id, f.label]), build.main, v => { build.main = v; if (isTwoHanded(v)) build.off = 'none'; rerender(); })),
    field('Mão secundária', (() => {
      const node = select([['none', 'Nenhuma'], ['shield', 'Escudo'], ['Dagger', 'Adaga (20% do ataque)'], ['Pistol', 'Pistola (20% do ataque)']], eff.off, v => { build.off = v; rerender(); });
      if (isTwoHanded(build.main)) node.disabled = true;
      return node;
    })(), isTwoHanded(build.main) ? 'Arma de duas mãos ocupa as duas mãos.' : null),
  ));
  card.append(el('div', { class: 'sim-row' },
    field('Armadura', select(ARMORS, build.armor, v => { build.armor = v; rerender(); })),
    field('Grau da arma', select([['1', 'Comum (grau 01)'], ['2', 'Grau 02']], String(build.weaponGrade), v => { build.weaponGrade = Number(v); rerender(); }), 'A armadura só tem dados do grau 01.'),
  ));
  card.append(el('div', { class: 'sim-row' },
    el('label', { class: 'sim-check' }, el('input', { type: 'checkbox', checked: build.sameGear, onchange: event => { build.sameGear = event.target.checked; rerender(); } }), el('span', { text: 'Equipamento do mesmo nível do personagem' })),
    build.sameGear ? null : field('Nível do equipamento', numberInput(build.gearLevel, 1, 315, v => { build.gearLevel = Math.min(315, Math.max(1, Math.round(v) || 1)); rerender(); })),
  ));
  return card;
}

const ROWS = [
  ['Personagem', null],
  ['HP máximo', r => r.hp, 0],
  ['SP máximo', r => r.sp, 0],
  ['Ataque físico', r => r.attack, 0],
  ['Ataque de preparados (INT)', r => r.prepAttack, 0],
  ['Bônus de cura de preparados', r => r.healBonusPct, 0, '%'],
  ['Defesa', r => r.defense, 0],
  ['Precisão', r => r.accuracy, 1],
  ['Esquiva', r => r.dodge, 1],
  ['Ataque crítico', r => r.critAttack, 0],
  ['Taxa de crítico', r => r.critRate, 1],
  ['Estamina', r => r.stamina, 0],
  ['Peso máximo', r => r.weight, 0],
  ['Contra um monstro comum do mesmo nível', null],
  ['Fator da habilidade', r => r.fight?.skillFactorPct, 0, '%'],
  ['Dano de uma habilidade', r => r.fight?.skillDamage, 0],
  ['Defesa do monstro corta', r => r.fight?.monsterReductionPct, 1, '%'],
  ['Habilidades para derrubar (sozinho)', r => r.fight?.skillsToKill, 1],
  ['Dano de um golpe do monstro', r => r.fight?.hitDamage, 0],
  ['Golpe do monstro tira do HP', r => r.fight?.hitPctHp, 1, '%'],
  ['Sua defesa corta', r => r.fight?.playerReductionPct, 1, '%'],
  ['Chance de desviar', r => r.fight?.dodgePct, 1, '%'],
  ['Golpes até cair', r => r.fight?.hitsToDie, 1],
];

function resultsTable(results) {
  const names = results.map((_, i) => `Build ${i ? 'B' : 'A'}`);
  const head = el('tr', {}, el('th', { scope: 'col', text: 'Resultado' }), names.map(n => el('th', { scope: 'col', text: n })),
    results.length === 2 ? el('th', { scope: 'col', text: 'B − A' }) : null);
  const body = ROWS.map(([label, pick, digits = 0, unit = '']) => {
    if (!pick) return el('tr', { class: 'sim-section' }, el('th', { scope: 'colgroup', colspan: results.length === 2 ? 4 : 2, text: label }));
    const values = results.map(r => pick(r));
    const delta = results.length === 2 && typeof values[0] === 'number' && typeof values[1] === 'number' ? values[1] - values[0] : null;
    return el('tr', {}, el('th', { scope: 'row', text: label }),
      values.map(v => el('td', { text: v === undefined ? 'sem referência' : fmt(v, digits) + (unit ? ' ' + unit : '') })),
      results.length === 2 ? el('td', { class: delta === null ? '' : delta > 0 ? 'up' : delta < 0 ? 'down' : '', text: delta === null ? '–' : (delta > 0 ? '+' : '') + fmt(delta, digits) }) : null);
  });
  return el('div', { class: 'table-scroll', tabindex: '0', role: 'region', 'aria-label': 'Resultados do simulador' },
    el('table', { class: 'sim-results' }, el('thead', {}, head), el('tbody', {}, body)));
}

function render() {
  const results = state.builds.map(b => compute(data, effective(b)));
  const cards = state.builds.map((_, i) => buildCard(i));
  const toolbar = el('div', { class: 'sim-toolbar' },
    el('button', { type: 'button', class: 'sim-button', onclick: () => {
      if (state.compare) { state.builds.pop(); state.compare = false; }
      else { state.builds.push(JSON.parse(JSON.stringify(state.builds[0]))); state.compare = true; }
      saveHash(); render();
    }, text: state.compare ? 'Remover a build B' : 'Comparar com uma build B' }),
    el('button', { type: 'button', class: 'sim-button', onclick: async event => {
      try { await navigator.clipboard.writeText(location.href); event.target.textContent = 'Link copiado'; }
      catch (error) { event.target.textContent = 'Copie o endereço da barra do navegador'; }
    }, text: 'Copiar link desta build' }));
  const note = el('p', { class: 'sim-note', text: 'Com a defesa percentual, o monstro de referência vem da tabela de ajustes do projeto (níveis 15 a 270). Abaixo do nível 15 ainda não há monstro de referência.' });
  root.replaceChildren(toolbar, el('div', { class: `sim-grid${state.compare ? ' two' : ''}` }, cards), el('h3', { text: 'Resultados' }), resultsTable(results), note);
}

loadHash();
render();
