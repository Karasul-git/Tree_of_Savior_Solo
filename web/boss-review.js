// Revisão dos chefes na página inicial.
// Usa o banco de dados do Artifact quando disponível; fora dele, mostra só leitura.
(function () {
  const section = document.getElementById('revisao-chefes');
  const bosses = window.WIKI_BOSSES || [];
  if (!section || !bosses.length) return;
  const grid = section.querySelector('[data-boss-grid]');
  const statusLine = section.querySelector('[data-review-status]');
  const filter = section.querySelector('[data-review-filter]');
  const root = document.documentElement.dataset.root || './';
  const labels = { pendente: 'Pendente', aprovado: 'Aprovado', rejeitado: 'Rejeitado' };
  const decisions = {};
  let db = null;
  let canWrite = false;

  function current(b) {
    return (decisions[b.slug] && decisions[b.slug].status) || b.review_status || 'pendente';
  }

  function summary() {
    const counts = { pendente: 0, aprovado: 0, rejeitado: 0 };
    bosses.forEach(b => { counts[current(b)] += 1; });
    return `${counts.aprovado} aprovados · ${counts.rejeitado} rejeitados · ${counts.pendente} pendentes`;
  }

  function render() {
    const wanted = filter ? filter.value : 'todos';
    grid.textContent = '';
    bosses.forEach(b => {
      const status = current(b);
      if (wanted !== 'todos' && status !== wanted) return;
      const card = document.createElement('article');
      card.className = 'boss-card status-' + status;
      card.id = 'chefe-' + b.slug;
      const img = document.createElement('img');
      img.src = root + b.portrait;
      img.alt = 'Retrato de ' + b.name;
      img.width = 80; img.height = 80; img.loading = 'lazy';
      const body = document.createElement('div');
      const title = document.createElement('a');
      title.href = root + 'docs/projeto-solo/chefes/' + b.slug + '.html';
      title.textContent = b.name;
      const group = document.createElement('p');
      group.textContent = b.category_label;
      const state = document.createElement('p');
      state.className = 'boss-state';
      state.textContent = labels[status];
      body.append(title, group, state);
      const actions = document.createElement('div');
      actions.className = 'boss-actions';
      [['aprovado', 'Aprovar'], ['rejeitado', 'Rejeitar'], ['pendente', 'Desfazer']].forEach(([value, text]) => {
        const button = document.createElement('button');
        button.type = 'button';
        button.textContent = text;
        button.disabled = !canWrite || status === value;
        button.addEventListener('click', () => save(b, value, button));
        actions.append(button);
      });
      card.append(img, body, actions);
      grid.append(card);
    });
    statusLine.textContent = summary() + (canWrite ? '' : ' · Para aprovar, abra esta página no Claude com acesso de edição.');
  }

  async function save(b, value, button) {
    if (!db) return;
    button.disabled = true;
    try {
      await db.collection('boss_reviews').doc(b.slug).set({ status: value, name: b.name, updated_at: new Date().toISOString() });
    } catch (e) {
      statusLine.textContent = 'Não foi possível salvar agora (' + (e && e.code ? e.code : 'erro') + '). Tente de novo em instantes.';
      button.disabled = false;
    }
  }

  if (filter) filter.addEventListener('change', render);
  render();

  function connect() {
    window.claude.use('db').then(found => {
      if (!found) return;
      db = found;
      canWrite = true;
      const timer = setTimeout(() => { statusLine.textContent = summary() + ' · Revisões ainda carregando.'; }, 10000);
      db.collection('boss_reviews').onSnapshot(snap => {
        clearTimeout(timer);
        Object.keys(decisions).forEach(k => delete decisions[k]);
        snap.docs.forEach(doc => { if (doc.exists) decisions[doc.id] = doc.data(); });
        render();
      }, () => {
        clearTimeout(timer);
        canWrite = false;
        render();
      });
      render();
    }).catch(() => {});
  }
  let tries = 0;
  (function wait() {
    if (window.claude && typeof window.claude.use === 'function') return connect();
    if (++tries < 20) setTimeout(wait, 500);
  })();
})();
