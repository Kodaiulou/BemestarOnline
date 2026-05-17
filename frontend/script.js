// =============================================================
// PRODUTOS — fonte única da verdade
// Para adicionar: copie um objeto e preencha os campos.
// =============================================================
const PRODUTOS = [
  {
    id: 'losartana', emoji: '💊', categoria: 'genericos',
    nome: 'Losartana Potássica 50mg', descricao: 'Genérico · 30 comprimidos',
    descricaoLonga: 'Anti-hipertensivo da classe dos antagonistas dos receptores da angiotensina II. Indicado para tratamento da hipertensão arterial e proteção renal em diabéticos com nefropatia.',
    preco: 12.90, parcelamento: '3x R$ 4,43',
    detalhes: ['Genérico de referência aprovado pela ANVISA','Armazenar em local fresco e seco','Apresentação: 30 comprimidos revestidos','Laboratório: EMS Sigma Pharma'],
  },
  {
    id: 'protetor', emoji: '🧴', categoria: 'dermocosmeticos',
    nome: 'Protetor Solar FPS 50', descricao: 'Dermocosmético · 200ml',
    descricaoLonga: 'Protetor solar de amplo espectro com FPS 50, resistente à água. Proteção contra raios UVA e UVB. Textura leve, não oleosa, ideal para uso diário.',
    preco: 54.90, parcelamento: '3x R$ 18,30',
    detalhes: ['FPS 50 — proteção muito alta','Resistente à água por até 80 min','Não comedogênico','Conteúdo: 200ml'],
  },
  {
    id: 'dipirona', emoji: '💊', categoria: 'genericos',
    nome: 'Dipirona Sódica 500mg', descricao: 'Genérico · 10 comprimidos',
    descricaoLonga: 'Analgésico e antitérmico indicado para dores de intensidade leve a moderada e febres. Ação rápida em até 30 minutos após a ingestão.',
    preco: 4.50, parcelamento: 'à vista',
    detalhes: ['Alívio em até 30 minutos','Genérico aprovado pela ANVISA','10 comprimidos de 500mg','Não usar em crianças < 3 meses'],
  },
  {
    id: 'sabonete', emoji: '🧼', categoria: 'higiene',
    nome: 'Sabonete Líquido Neutro', descricao: 'Higiene Pessoal · 250ml',
    descricaoLonga: 'Sabonete líquido de pH neutro, formulado para peles sensíveis. Limpa suavemente sem agredir a barreira protetora da pele. Sem fragrância artificial.',
    preco: 18.20, parcelamento: '2x R$ 9,10',
    detalhes: ['pH neutro (5,5) — peles sensíveis','Sem fragrância artificial','Sem parabenos','250ml com dosador pump'],
  },
  {
    id: 'curativos', emoji: '🩹', categoria: 'primeiros-socorros',
    nome: 'Kit Curativos Variados', descricao: 'Primeiros Socorros · 40 un.',
    descricaoLonga: 'Kit completo com curativos de diferentes tamanhos e formatos: redondos, retangulares e para dedos. Com almofada absorvente e adesivo hipoalergênico.',
    preco: 9.90, parcelamento: 'à vista',
    detalhes: ['40 unidades em 4 tamanhos','Adesivo hipoalergênico','Almofada com proteção antibacteriana','Embalagem selada individualmente'],
  },
  {
    id: 'vitamina-c', emoji: '💊', categoria: 'genericos',
    nome: 'Vitamina C 1g', descricao: 'Suplemento · 10 pastilhas',
    descricaoLonga: 'Suplemento de Vitamina C (ácido ascórbico) em pastilhas efervescentes com sabor laranja. Auxilia na imunidade e é poderoso antioxidante.',
    preco: 15.00, parcelamento: '2x R$ 7,50',
    detalhes: ['1000mg de ácido ascórbico por dose','Pastilha efervescente sabor laranja','Sem açúcar adicionado','10 pastilhas — caixa vedada'],
  },
  {
    id: 'ibuprofeno', emoji: '💊', categoria: 'referencia',
    nome: 'Ibuprofeno 400mg', descricao: 'Referência · 20 comprimidos',
    descricaoLonga: 'Anti-inflamatório não esteroidal (AINE) indicado para dores musculares, articulares, dores de cabeça e febre. Ação anti-inflamatória, analgésica e antitérmica.',
    preco: 8.90, parcelamento: 'à vista',
    detalhes: ['Ação tripla: inflamação, dor e febre','20 comprimidos revestidos','Referência de laboratório Sanofi','Tomar com alimentos'],
  },
  {
    id: 'hidratante', emoji: '🧴', categoria: 'dermocosmeticos',
    nome: 'Hidratante Corporal', descricao: 'Dermocosmético · 400ml',
    descricaoLonga: 'Creme hidratante corporal enriquecido com manteiga de karité e vitamina E. Hidratação profunda por até 48h, pele mais macia e nutrida desde a primeira aplicação.',
    preco: 32.50, parcelamento: '2x R$ 16,25',
    detalhes: ['Hidratação 48h comprovada','Manteiga de karité + vitamina E','Frasco bomba de 400ml','Para todo tipo de pele'],
  },
];

// Cupons válidos — adicione / remova à vontade
const CUPONS = {
  'BEMSTAR10':   { tipo: 'percentual',  valor: 10,   descricao: '10% de desconto' },
  'SAUDE15':     { tipo: 'percentual',  valor: 15,   descricao: '15% de desconto' },
  'FRETEGRATIS': { tipo: 'frete',       valor: 0,    descricao: 'Frete grátis'    },
  'PRIMEIRACOMPRA': { tipo: 'fixo',     valor: 5,    descricao: 'R$ 5,00 de desconto' },
};

// =============================================================
// UTILITÁRIOS DE ARMAZENAMENTO
// =============================================================
const Store = {
  get:    (k, def = null) => { try { return JSON.parse(localStorage.getItem(k)) ?? def; } catch { return def; } },
  set:    (k, v)          => { try { localStorage.setItem(k, JSON.stringify(v)); } catch {} },
  remove: (k)             => { try { localStorage.removeItem(k); } catch {} },
};

const getCarrinho  = ()   => Store.get('beo_carrinho', []);
const getUsuario   = ()   => Store.get('beo_usuario', null);
const getPedidos   = ()   => Store.get('beo_pedidos', []);
const getUsuarios  = ()   => Store.get('beo_usuarios', []);

// =============================================================
// INTEGRAÇÃO COM BACKEND C# ASP.NET CORE
// =============================================================
// Para testar com a API, execute o backend em http://localhost:5088.
// Caso a API esteja indisponível, o site continua funcionando com localStorage.
const API_BASE = window.BEO_API_BASE || 'http://localhost:5088/api';

async function apiFetch(endpoint, options = {}) {
  try {
    const resp = await fetch(`${API_BASE}${endpoint}`, {
      headers: { 'Content-Type': 'application/json', ...(options.headers || {}) },
      ...options,
    });
    const text = await resp.text();
    const data = text ? JSON.parse(text) : null;
    if (!resp.ok) throw data || { mensagem: 'Erro de comunicação com a API.' };
    return data;
  } catch (erro) {
    console.warn('API indisponível ou retornou erro:', erro);
    return null;
  }
}

function produtoApiParaLocal(p) {
  return {
    id: String(p.idMedicamento),
    emoji: p.exigeReceita ? '💊' : '🌿',
    categoria: p.categoriaSlug,
    nome: p.nome,
    descricao: `${p.tipo}${p.dosagem ? ' · ' + p.dosagem : ''}`,
    descricaoLonga: p.descricao,
    preco: Number(p.preco),
    parcelamento: Number(p.preco) >= 20 ? `3x R$ ${(Number(p.preco)/3).toFixed(2).replace('.', ',')}` : 'à vista',
    detalhes: [
      `Categoria: ${p.categoria}`,
      `Fabricante: ${p.fabricante}`,
      `Estoque disponível: ${p.estoque}`,
      p.exigeReceita ? 'Venda mediante apresentação de receita' : 'Produto sem exigência de receita'
    ]
  };
}

async function carregarProdutosDaApi() {
  const produtos = await apiFetch('/produtos');
  if (Array.isArray(produtos) && produtos.length) {
    PRODUTOS.splice(0, PRODUTOS.length, ...produtos.map(produtoApiParaLocal));
    Store.set('beo_api_online', true);
  } else {
    Store.set('beo_api_online', false);
  }
}

function salvarCarrinho(c) { Store.set('beo_carrinho', c); atualizarBadge(); }

function formatarPreco(v) {
  return `R$ ${Number(v).toFixed(2).replace('.', ',')}`;
}

// =============================================================
// DARK MODE
// =============================================================
function initTema() {
  const salvo = Store.get('beo_tema', 'claro');
  document.documentElement.setAttribute('data-tema', salvo);
}

function toggleTema() {
  const atual = document.documentElement.getAttribute('data-tema') || 'claro';
  const novo  = atual === 'escuro' ? 'claro' : 'escuro';
  document.documentElement.setAttribute('data-tema', novo);
  Store.set('beo_tema', novo);
  const btn = document.getElementById('btn-tema');
  if (btn) btn.textContent = novo === 'escuro' ? '☀️' : '🌙';
}

// =============================================================
// CABEÇALHO DINÂMICO
// =============================================================
function injetarCabecalho() {
  const el = document.getElementById('app-header');
  if (!el) return;

  const usuario  = getUsuario();
  const pagAtual = window.location.pathname.split('/').pop() || 'index.html';
  const links    = [
    { href:'index.html',        label:'Início'       },
    { href:'medicamentos.html', label:'Medicamentos' },
    { href:'assinatura.html',   label:'Assinatura'   },
    { href:'delivery.html',     label:'Delivery'     },
  ];

  const menuHTML = links.map(l =>
    `<a href="${l.href}" class="${pagAtual === l.href ? 'ativo' : ''}">${l.label}</a>`
  ).join('');

  const usuarioHTML = usuario
    ? `<div class="usuario-logado">
         <a href="conta.html" style="text-decoration:none;color:inherit">
           👤 <strong>${usuario.nome.split(' ')[0]}</strong>
         </a>
         <button class="btn-sair" onclick="logout()">Sair</button>
       </div>`
    : `<a href="login.html" class="link-login" aria-label="Conta e Login">👤 Conta / Login</a>`;

  const qtd  = getCarrinho().reduce((a, i) => a + i.quantidade, 0);
  const tema = Store.get('beo_tema', 'claro');

  el.innerHTML = `
    <header class="cabecalho" role="banner">
      <a href="index.html" class="logo" aria-label="Início">
        <span class="icone-logo" aria-hidden="true">🌿</span>
        <div class="texto-logo">Bem-Estar <br><span class="destaque-logo">Online</span></div>
      </a>

      <nav class="menu" id="menu-nav" role="navigation" aria-label="Menu principal">
        ${menuHTML}
      </nav>

      <div class="acoes-cabecalho">
        <div class="wrap-busca">
          <div class="barra-busca" role="search">
            <label for="campo-busca" class="sr-only">Buscar produto</label>
            <input type="text" id="campo-busca" placeholder="Buscar produto..."
                   autocomplete="off" aria-label="Buscar produto"
                   oninput="autocompleteBusca(this.value)"
                   onkeydown="buscarProduto(event)"
                   onfocus="mostrarAutocomplete()"
                   onblur="setTimeout(()=>esconderAutocomplete(),200)">
            <button aria-label="Buscar" onclick="buscarProduto({key:'Enter'})">🔍</button>
          </div>
          <div class="autocomplete-lista" id="autocomplete-lista"></div>
        </div>

        ${usuarioHTML}

        <button class="btn-tema" id="btn-tema" onclick="toggleTema()"
                aria-label="Alternar tema claro/escuro">
          ${tema === 'escuro' ? '☀️' : '🌙'}
        </button>

        <button class="btn-carrinho" onclick="verCarrinho()"
                aria-label="Abrir carrinho">
          🛒 <span id="contador" class="badge" aria-live="polite">${qtd}</span>
        </button>
      </div>

      <button class="btn-hamburguer" id="btn-hamburguer"
              aria-label="Abrir menu" aria-expanded="false"
              onclick="toggleMenu()">
        <span></span><span></span><span></span>
      </button>
    </header>`;
}

function toggleMenu() {
  const menu = document.getElementById('menu-nav');
  const btn  = document.getElementById('btn-hamburguer');
  const open = menu.classList.toggle('aberto');
  btn.setAttribute('aria-expanded', String(open));
  btn.classList.toggle('ativo');
}

// =============================================================
// AUTOCOMPLETE
// =============================================================
function autocompleteBusca(termo) {
  const lista = document.getElementById('autocomplete-lista');
  if (!lista) return;

  if (!termo.trim()) { lista.classList.remove('visivel'); lista.innerHTML=''; return; }

  const resultados = PRODUTOS.filter(p =>
    p.nome.toLowerCase().includes(termo.toLowerCase()) ||
    p.descricao.toLowerCase().includes(termo.toLowerCase())
  ).slice(0, 5);

  if (!resultados.length) { lista.classList.remove('visivel'); return; }

  lista.innerHTML = resultados.map(p => `
    <div class="autocomplete-item" onclick="irParaProduto('${p.id}')">
      <span class="emoji" aria-hidden="true">${p.emoji}</span>
      <div class="info">
        ${p.nome}
        <small>${formatarPreco(p.preco)}</small>
      </div>
    </div>`).join('');
  lista.classList.add('visivel');
}

function mostrarAutocomplete() {
  const v = document.getElementById('campo-busca')?.value;
  if (v) autocompleteBusca(v);
}
function esconderAutocomplete() {
  document.getElementById('autocomplete-lista')?.classList.remove('visivel');
}

function irParaProduto(id) {
  window.location.href = `produto.html?id=${id}`;
}

function buscarProduto(event) {
  if (event.key && event.key !== 'Enter') return;
  const termo    = document.getElementById('campo-busca')?.value.trim();
  const pagAtual = window.location.pathname.split('/').pop();
  if (pagAtual === 'medicamentos.html') {
    renderizarCatalogo([], termo);
  } else if (termo) {
    window.location.href = `medicamentos.html?busca=${encodeURIComponent(termo)}`;
  }
}

// =============================================================
// CARRINHO
// =============================================================
function adicionarAoCarrinho(id, qtd = 1) {
  const prod = PRODUTOS.find(p => p.id === id);
  if (!prod) return;
  const carrinho  = getCarrinho();
  const existente = carrinho.find(i => i.id === id);
  if (existente) existente.quantidade += qtd;
  else carrinho.push({ ...prod, quantidade: qtd });
  salvarCarrinho(carrinho);
  mostrarAviso(`${prod.nome} adicionado! 🛒`, 'sucesso');
  animarBotaoAdd(id);
}

function animarBotaoAdd(id) {
  const btn = document.querySelector(`[data-add="${id}"]`);
  if (!btn) return;
  btn.textContent = '✓ Adicionado';
  btn.disabled = true;
  setTimeout(() => { btn.textContent = 'Adicionar ao Carrinho'; btn.disabled = false; }, 1800);
}

function removerDoCarrinho(id) {
  salvarCarrinho(getCarrinho().filter(i => i.id !== id));
  renderizarCarrinho();
}

function alterarQuantidade(id, delta) {
  const c    = getCarrinho();
  const item = c.find(i => i.id === id);
  if (!item) return;
  item.quantidade += delta;
  if (item.quantidade <= 0) salvarCarrinho(c.filter(i => i.id !== id));
  else salvarCarrinho(c);
  renderizarCarrinho();
}

function atualizarBadge() {
  const b = document.getElementById('contador');
  if (b) b.textContent = getCarrinho().reduce((a, i) => a + i.quantidade, 0);
}

function verCarrinho() { window.location.href = 'carrinho.html'; }

// =============================================================
// CUPOM DE DESCONTO
// =============================================================
let cupomAtivo = null;

function aplicarCupom() {
  const input = document.getElementById('input-cupom');
  const ok    = document.getElementById('cupom-ok');
  const err   = document.getElementById('cupom-err');
  const codigo = input?.value.trim().toUpperCase();
  if (ok)  ok.textContent  = '';
  if (err) err.textContent = '';

  if (!codigo) { if(err) err.textContent='Digite um código de cupom.'; return; }

  const cupom = CUPONS[codigo];
  if (!cupom) { if(err) err.textContent='Cupom inválido ou expirado.'; cupomAtivo=null; renderizarCarrinho(); return; }

  cupomAtivo = { ...cupom, codigo };
  if (ok) ok.textContent = `✓ ${cupom.descricao} aplicado!`;
  renderizarCarrinho();
}

function calcularDesconto(subtotal, frete) {
  if (!cupomAtivo) return { desconto:0, freteCalc:frete };
  if (cupomAtivo.tipo === 'percentual') return { desconto: subtotal * (cupomAtivo.valor/100), freteCalc: frete };
  if (cupomAtivo.tipo === 'fixo')       return { desconto: cupomAtivo.valor, freteCalc: frete };
  if (cupomAtivo.tipo === 'frete')      return { desconto: frete, freteCalc: 0 };
  return { desconto:0, freteCalc:frete };
}

// =============================================================
// CÁLCULO DE FRETE POR CEP (ViaCEP)
// =============================================================
let freteCustom = null;

async function calcularFrete() {
  const input  = document.getElementById('input-cep');
  const info   = document.getElementById('frete-info');
  const cep    = input?.value.replace(/\D/g,'');

  if (!cep || cep.length !== 8) {
    if(info) info.textContent = 'CEP inválido. Digite 8 dígitos.';
    return;
  }
  if(info) info.textContent = '🔍 Consultando...';

  try {
    const r = await fetch(`https://viacep.com.br/ws/${cep}/json/`);
    const d = await r.json();
    if (d.erro) throw new Error('CEP não encontrado.');

    // Regra de frete por UF
    const fretes = { SP:8, RJ:10, MG:10, ES:12, PR:12, SC:12, RS:14 };
    const valor  = fretes[d.uf] ?? 18;
    freteCustom  = valor;

    if(info) info.innerHTML =
      `📍 ${d.localidade}/${d.uf} · Frete: <strong>${formatarPreco(valor)}</strong>`;

    renderizarCarrinho();
  } catch(e) {
    if(info) info.textContent = e.message || 'Não foi possível calcular o frete.';
  }
}

function mascaraCEP(v) {
  return v.replace(/\D/g,'').replace(/^(\d{5})(\d)/,'$1-$2').substring(0,9);
}

// =============================================================
// RENDERIZAR CARRINHO
// =============================================================
function renderizarCarrinho() {
  const lista    = document.getElementById('lista-itens-carrinho');
  const resumoEl = document.getElementById('resumo-pedido');
  if (!lista) return;

  const carrinho = getCarrinho();

  if (!carrinho.length) {
    lista.innerHTML = `
      <div class="carrinho-vazio">
        <span aria-hidden="true">🛒</span>
        <h3>Seu carrinho está vazio</h3>
        <p>Explore nossos produtos e faça boas escolhas.</p>
        <a href="medicamentos.html" class="btn-primario-azul"
           style="display:inline-flex;width:auto;padding:12px 28px;
                  text-decoration:none;margin-top:16px;border-radius:10px;">
          Ver Produtos
        </a>
      </div>`;
    if (resumoEl) resumoEl.style.display = 'none';
    return;
  }

  if (resumoEl) resumoEl.style.display = '';

  lista.innerHTML = carrinho.map(item => `
    <div class="item-carrinho" id="item-${item.id}">
      <div class="info-produto-carrinho">
        <span class="icone-grande" aria-hidden="true">${item.emoji}</span>
        <div>
          <h4><a href="produto.html?id=${item.id}"
               style="text-decoration:none;color:inherit">${item.nome}</a></h4>
          <p>${item.descricao}</p>
        </div>
      </div>
      <div class="controles-quantidade">
        <button class="btn-qtd" onclick="alterarQuantidade('${item.id}',-1)"
                aria-label="Diminuir">−</button>
        <input type="text" value="${item.quantidade}" readonly
               class="input-qtd" aria-label="Quantidade">
        <button class="btn-qtd" onclick="alterarQuantidade('${item.id}',1)"
                aria-label="Aumentar">+</button>
      </div>
      <div class="preco-item">
        <p>${formatarPreco(item.preco * item.quantidade)}</p>
        <small>und. ${formatarPreco(item.preco)}</small>
      </div>
      <button class="btn-remover" onclick="removerDoCarrinho('${item.id}')"
              aria-label="Remover ${item.nome}">🗑️</button>
    </div>`).join('');

  const subtotal   = carrinho.reduce((a,i) => a + i.preco * i.quantidade, 0);
  const freteBruto = freteCustom ?? 8;
  const totalItens = carrinho.reduce((a,i) => a + i.quantidade, 0);
  const { desconto, freteCalc } = calcularDesconto(subtotal, freteBruto);
  const total = subtotal - desconto + freteCalc;

  const set = (id, v) => { const el=document.getElementById(id); if(el) el.textContent=v; };
  set('resumo-itens',    `Subtotal (${totalItens} ${totalItens===1?'item':'itens'})`);
  set('resumo-subtotal', formatarPreco(subtotal));
  set('resumo-frete',    formatarPreco(freteCalc));
  set('resumo-total',    formatarPreco(total));

  const linhaDesc = document.getElementById('linha-desconto');
  const valDesc   = document.getElementById('resumo-desconto');
  if (desconto > 0 && linhaDesc && valDesc) {
    linhaDesc.style.display = '';
    valDesc.textContent = `− ${formatarPreco(desconto)}`;
  } else if (linhaDesc) {
    linhaDesc.style.display = 'none';
  }
}

// =============================================================
// FINALIZAR COMPRA (abre modal de pagamento)
// =============================================================
function finalizarCompra() {
  const carrinho = getCarrinho();
  if (!carrinho.length) { mostrarAviso('Seu carrinho está vazio!','erro'); return; }
  abrirModal();
}

function abrirModal() {
  const subtotal = getCarrinho().reduce((a,i) => a + i.preco * i.quantidade, 0);
  const freteBruto = freteCustom ?? 8;
  const { desconto, freteCalc } = calcularDesconto(subtotal, freteBruto);
  const total = subtotal - desconto + freteCalc;

  const el = document.getElementById('total-modal');
  if (el) el.textContent = `Total: ${formatarPreco(total)}`;
  document.getElementById('overlay-modal')?.classList.add('visivel');
}
function fecharModal() {
  document.getElementById('overlay-modal')?.classList.remove('visivel');
}
function ativarTab(tab) {
  document.querySelectorAll('.tab-pag').forEach(t => t.classList.remove('ativo'));
  document.querySelectorAll('.painel-pagamento').forEach(p => p.classList.remove('ativo'));
  document.querySelector(`[data-tab="${tab}"]`)?.classList.add('ativo');
  document.getElementById(`painel-${tab}`)?.classList.add('ativo');
}

function confirmarPagamento() {
  const btn = document.getElementById('btn-pagar');
  if (btn) {
    btn.innerHTML = '<span class="spinner"></span> Processando...';
    btn.disabled = true;
  }
  setTimeout(concluirPedido, 2000);
}

function concluirPedido() {
  const carrinho  = getCarrinho();
  const subtotal  = carrinho.reduce((a,i) => a + i.preco * i.quantidade, 0);
  const freteBruto = freteCustom ?? 8;
  const { desconto, freteCalc } = calcularDesconto(subtotal, freteBruto);
  const total     = subtotal - desconto + freteCalc;

  const pedido = {
    numero:  Math.floor(Math.random()*900000)+100000,
    itens:   carrinho,
    subtotal, desconto, frete: freteCalc, total,
    cupom:   cupomAtivo?.codigo || null,
    data:    new Date().toLocaleDateString('pt-BR'),
    hora:    new Date().toLocaleTimeString('pt-BR',{hour:'2-digit',minute:'2-digit'}),
    status:  'Processando',
  };

  const historico = getPedidos();
  historico.unshift(pedido);
  Store.set('beo_pedidos', historico);
  Store.set('beo_ultimo_pedido', pedido);
  Store.remove('beo_carrinho');

  cupomAtivo  = null;
  freteCustom = null;

  fecharModal();
  window.location.href = 'confirmacao.html';
}

// =============================================================
// CATÁLOGO E FILTROS
// =============================================================
function renderizarCatalogo(filtros = [], busca = '') {
  const grade = document.getElementById('grade-produtos');
  if (!grade) return;

  const filtrados = PRODUTOS.filter(p => {
    const passaCat  = !filtros.length || filtros.includes(p.categoria);
    const passaBusc = !busca ||
      p.nome.toLowerCase().includes(busca.toLowerCase()) ||
      p.descricao.toLowerCase().includes(busca.toLowerCase());
    return passaCat && passaBusc;
  });

  if (!filtrados.length) {
    grade.innerHTML = `
      <div class="carrinho-vazio" style="grid-column:1/-1">
        <span>🔍</span>
        <h3>Nenhum produto encontrado</h3>
        <p>Tente outros filtros ou termos de busca.</p>
      </div>`;
    return;
  }

  grade.innerHTML = filtrados.map((p, i) => `
    <div class="produto-card" style="animation-delay:${i*0.05}s">
      <div class="produto-card-img" aria-hidden="true">${p.emoji}</div>
      <div class="produto-card-corpo">
        <h4>${p.nome}</h4>
        <p class="descricao-produto">${p.descricao}</p>
        <p class="preco-produto">${formatarPreco(p.preco)}</p>
        <div style="display:flex;gap:8px;flex-direction:column">
          <button class="btn-primario-azul" data-add="${p.id}"
                  onclick="adicionarAoCarrinho('${p.id}')"
                  aria-label="Adicionar ${p.nome} ao carrinho">
            Adicionar ao Carrinho
          </button>
          <a href="produto.html?id=${p.id}"
             style="text-align:center;font-size:13px;color:var(--cor-primaria);
                    font-weight:600;text-decoration:none;">
            Ver detalhes →
          </a>
        </div>
      </div>
    </div>`).join('');
}

function filtrarProdutos() {
  const filtros = Array.from(
    document.querySelectorAll('.lista-filtros input:checked')
  ).map(cb => cb.dataset.categoria);
  const busca = document.getElementById('campo-busca')?.value.trim() || '';
  renderizarCatalogo(filtros, busca);
}

// =============================================================
// PÁGINA DO PRODUTO
// =============================================================
function renderizarProduto() {
  const container = document.getElementById('container-produto');
  if (!container) return;

  const id   = new URLSearchParams(window.location.search).get('id');
  const prod = PRODUTOS.find(p => p.id === id);

  if (!prod) {
    container.innerHTML = `
      <div class="carrinho-vazio" style="width:100%;grid-column:1/-1">
        <span>😕</span>
        <h3>Produto não encontrado</h3>
        <p><a href="medicamentos.html">Voltar ao catálogo</a></p>
      </div>`;
    return;
  }

  document.title = `${prod.nome} — Bem-Estar Online`;

  const categorias = {
    genericos:'Genérico', referencia:'Referência',
    higiene:'Higiene', dermocosmeticos:'Dermocosmético',
    'primeiros-socorros':'Primeiros Socorros'
  };

  container.innerHTML = `
    <div class="produto-galeria" aria-hidden="true">${prod.emoji}</div>
    <div class="produto-info">
      <span class="badge-categoria">${categorias[prod.categoria] || prod.categoria}</span>
      <h1>${prod.nome}</h1>
      <p class="descricao-longa">${prod.descricaoLonga}</p>
      <div class="produto-preco-grande">${formatarPreco(prod.preco)}</div>
      <p class="produto-parcelamento">${prod.parcelamento}</p>
      <div class="produto-qtd-wrap">
        <label>Quantidade:</label>
        <div class="produto-qtd">
          <button onclick="ajustarQtdProd(-1)" aria-label="Diminuir">−</button>
          <input type="text" id="qtd-produto" value="1" readonly aria-label="Quantidade">
          <button onclick="ajustarQtdProd(1)" aria-label="Aumentar">+</button>
        </div>
      </div>
      <button class="btn-add-produto" data-add="${prod.id}"
              onclick="adicionarAoCarrinho('${prod.id}', parseInt(document.getElementById('qtd-produto').value))">
        🛒 Adicionar ao Carrinho
      </button>
      <a href="medicamentos.html" style="display:block;text-align:center;font-size:14px;
         color:var(--cor-texto-suave);text-decoration:none;margin-top:6px;">
        ← Voltar ao catálogo
      </a>
      <div class="produto-detalhes">
        <h3>Detalhes do produto</h3>
        <ul>${prod.detalhes.map(d=>`<li>${d}</li>`).join('')}</ul>
      </div>
    </div>`;
}

function ajustarQtdProd(delta) {
  const input = document.getElementById('qtd-produto');
  if (!input) return;
  const nova = Math.max(1, parseInt(input.value) + delta);
  input.value = nova;
}

// =============================================================
// CONFIRMAÇÃO DO PEDIDO
// =============================================================
function renderizarConfirmacao() {
  const container = document.getElementById('resumo-confirmacao');
  if (!container) return;

  const pedido = Store.get('beo_ultimo_pedido', null);
  if (!pedido) {
    container.innerHTML = `<p style="text-align:center;color:var(--cor-texto-suave)">
      Nenhum pedido encontrado. <a href="index.html">Voltar ao início</a>
    </p>`;
    return;
  }

  container.innerHTML = `
    <div class="confirmacao-numero">Pedido #${pedido.numero}</div>
    <p class="confirmacao-data">Realizado em ${pedido.data} às ${pedido.hora}</p>
    <h3>Itens do pedido</h3>
    <ul class="confirmacao-itens">
      ${pedido.itens.map(i=>`
        <li>
          <span><span aria-hidden="true">${i.emoji}</span> ${i.nome} ×${i.quantidade}</span>
          <span>${formatarPreco(i.preco * i.quantidade)}</span>
        </li>`).join('')}
    </ul>
    <div class="confirmacao-totais">
      <div class="linha-resumo"><span>Subtotal</span><span>${formatarPreco(pedido.subtotal)}</span></div>
      ${pedido.desconto > 0
        ? `<div class="linha-resumo desconto">
             <span>Desconto ${pedido.cupom ? `(${pedido.cupom})` : ''}</span>
             <span>− ${formatarPreco(pedido.desconto)}</span>
           </div>`
        : ''}
      <div class="linha-resumo"><span>Frete</span><span>${formatarPreco(pedido.frete)}</span></div>
      <div class="divisor-horizontal"></div>
      <div class="linha-resumo total">
        <span>Total</span><span>${formatarPreco(pedido.total)}</span>
      </div>
    </div>
    <div class="confirmacao-entrega">
      <span aria-hidden="true">🚚</span>
      <p>Previsão de entrega: <strong>hoje, em até 2 horas</strong></p>
    </div>`;
}

// =============================================================
// CONTA DO USUÁRIO
// =============================================================
function renderizarConta() {
  const usuario = getUsuario();
  if (!usuario) { window.location.href = 'login.html'; return; }

  const nomeEl   = document.getElementById('conta-nome');
  const emailEl  = document.getElementById('conta-email');
  const avatarEl = document.getElementById('conta-iniciais');

  if (nomeEl)  nomeEl.textContent  = usuario.nome;
  if (emailEl) emailEl.textContent = usuario.email;
  if (avatarEl) avatarEl.textContent = usuario.nome.split(' ').map(n=>n[0]).slice(0,2).join('');

  renderizarHistorico();
}

function renderizarHistorico() {
  const container = document.getElementById('lista-pedidos');
  if (!container) return;
  const pedidos = getPedidos();

  if (!pedidos.length) {
    container.innerHTML = `
      <div class="carrinho-vazio" style="padding:40px 20px">
        <span>📦</span>
        <h3>Nenhum pedido ainda</h3>
        <p><a href="medicamentos.html">Fazer meu primeiro pedido</a></p>
      </div>`;
    return;
  }

  const statusClasses = { Processando:'status-processando', Enviado:'status-enviado', Entregue:'status-entregue' };

  container.innerHTML = pedidos.map(p => `
    <div class="card-pedido">
      <div class="pedido-header">
        <strong>Pedido #${p.numero}</strong>
        <span class="badge-status ${statusClasses[p.status] || 'status-processando'}">${p.status}</span>
      </div>
      <p class="pedido-itens">${p.itens.map(i=>`${i.nome} ×${i.quantidade}`).join(' · ')}</p>
      <div style="display:flex;justify-content:space-between;align-items:center">
        <span class="pedido-total">${formatarPreco(p.total)}</span>
        <small style="color:var(--cor-texto-suave)">${p.data} às ${p.hora}</small>
      </div>
    </div>`).join('');
}

function mostrarPainelConta(painel) {
  document.querySelectorAll('.painel-conta').forEach(el => el.classList.add('oculto'));
  document.querySelectorAll('.menu-conta a').forEach(a => a.classList.remove('ativo'));
  document.getElementById(`painel-${painel}`)?.classList.remove('oculto');
  document.querySelector(`.menu-conta a[data-painel="${painel}"]`)?.classList.add('ativo');
}

// =============================================================
// AUTENTICAÇÃO
// =============================================================
async function fazerLogin(event) {
  event.preventDefault();
  const emailEl = document.getElementById('email-login');
  const senhaEl = document.getElementById('senha-login');
  let ok = true;

  if (!emailEl.value.trim()) { mostrarErro(emailEl,'Informe seu e-mail.'); ok=false; }
  else limparErro(emailEl);
  if (!senhaEl.value.trim()) { mostrarErro(senhaEl,'Informe sua senha.'); ok=false; }
  else limparErro(senhaEl);
  if (!ok) return;

  const usuarioApi = await apiFetch('/auth/login', {
    method: 'POST',
    body: JSON.stringify({ email: emailEl.value.trim(), senha: senhaEl.value })
  });

  if (usuarioApi?.idUsuario) {
    Store.set('beo_usuario', { idUsuario: usuarioApi.idUsuario, nome: usuarioApi.nome, email: usuarioApi.email, perfil: usuarioApi.perfil, origem: 'api' });
    mostrarAviso(`Bem-vindo de volta, ${usuarioApi.nome.split(' ')[0]}!`, 'sucesso');
    setTimeout(() => window.location.href = 'index.html', 1400);
    return;
  }

  const users = getUsuarios();
  const user  = users.find(u => u.email === emailEl.value.trim());
  if (!user)          { mostrarErro(emailEl,'E-mail não cadastrado ou API indisponível.'); return; }
  if (user.senha !== senhaEl.value) { mostrarErro(senhaEl,'Senha incorreta.'); return; }

  Store.set('beo_usuario', { nome:user.nome, email:user.email, origem: 'local' });
  mostrarAviso(`Bem-vindo de volta, ${user.nome.split(' ')[0]}!`, 'sucesso');
  setTimeout(() => window.location.href = 'index.html', 1400);
}

async function criarConta(event) {
  event.preventDefault();
  const c = {
    nome:         document.getElementById('nome-cadastro'),
    nasc:         document.getElementById('nascimento-cadastro'),
    email:        document.getElementById('email-cadastro'),
    cpf:          document.getElementById('cpf-cadastro'),
    tel:          document.getElementById('telefone-cadastro'),
    senha:        document.getElementById('senha-cadastro'),
    confirmSenha: document.getElementById('confirma-senha'),
  };

  let ok = true;
  const err = (campo, msg) => { mostrarErro(c[campo], msg); ok=false; };

  if (!c.nome.value.trim() || c.nome.value.trim().split(' ').length < 2)
    err('nome','Informe nome e sobrenome.');
  else limparErro(c.nome);

  if (!c.nasc.value) {
    err('nasc','Informe sua data de nascimento.');
  } else {
    const nasc = new Date(c.nasc.value), hoje = new Date();
    const idade = hoje.getFullYear() - nasc.getFullYear()
                - (hoje < new Date(hoje.getFullYear(), nasc.getMonth(), nasc.getDate()) ? 1 : 0);
    if (idade < 18) err('nasc','É necessário ter 18 anos ou mais.');
    else limparErro(c.nasc);
  }

  if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(c.email.value.trim()))
    err('email','Informe um e-mail válido.');
  else limparErro(c.email);

  if (c.cpf.value.replace(/\D/g,'').length !== 11)
    err('cpf','CPF incompleto. Verifique os dígitos.');
  else limparErro(c.cpf);

  if (c.tel.value.replace(/\D/g,'').length < 10)
    err('tel','Informe um telefone com DDD.');
  else limparErro(c.tel);

  if (c.senha.value.length < 6)
    err('senha','A senha deve ter pelo menos 6 caracteres.');
  else limparErro(c.senha);

  if (c.senha.value !== c.confirmSenha.value)
    err('confirmSenha','As senhas não coincidem.');
  else if (c.senha.value.length >= 6) limparErro(c.confirmSenha);

  if (!ok) return;

  const usuarioApi = await apiFetch('/auth/registrar', {
    method: 'POST',
    body: JSON.stringify({
      nome: c.nome.value.trim(),
      dataNascimento: c.nasc.value,
      email: c.email.value.trim(),
      cpf: c.cpf.value,
      telefone: c.tel.value,
      senha: c.senha.value
    })
  });

  if (usuarioApi?.idUsuario) {
    Store.set('beo_usuario', { idUsuario: usuarioApi.idUsuario, nome: usuarioApi.nome, email: usuarioApi.email, perfil: usuarioApi.perfil, origem: 'api' });
    mostrarAviso(`Conta criada! Olá, ${usuarioApi.nome.split(' ')[0]}!`, 'sucesso');
    setTimeout(() => window.location.href = 'index.html', 1400);
    return;
  }

  const users = getUsuarios();
  if (users.find(u => u.email === c.email.value.trim())) {
    mostrarErro(c.email,'Este e-mail já está cadastrado.'); return;
  }

  users.push({
    nome: c.nome.value.trim(), email: c.email.value.trim(),
    cpf:  c.cpf.value, tel:  c.tel.value,
    nasc: c.nasc.value, senha: c.senha.value,
  });
  Store.set('beo_usuarios', users);
  Store.set('beo_usuario', { nome:c.nome.value.trim(), email:c.email.value.trim(), origem: 'local' });
  mostrarAviso(`Conta criada! Olá, ${c.nome.value.trim().split(' ')[0]}!`, 'sucesso');
  setTimeout(() => window.location.href = 'index.html', 1400);
}

function logout() {
  Store.remove('beo_usuario');
  mostrarAviso('Até logo! 👋','info');
  setTimeout(() => window.location.href = 'index.html', 1000);
}

// =============================================================
// FORMULÁRIOS DE DELIVERY E ASSINATURA
// =============================================================
function enviarPedidoDelivery(event) {
  event.preventDefault();
  const end = document.getElementById('endereco');
  const ped = document.getElementById('pedido');
  let ok = true;
  if (!end.value.trim()) { mostrarErro(end,'Informe o endereço completo.'); ok=false; } else limparErro(end);
  if (!ped.value.trim()) { mostrarErro(ped,'Descreva o que você precisa.');  ok=false; } else limparErro(ped);
  if (!ok) return;
  mostrarAviso('Pedido enviado! Motoboy a caminho em até 2 horas. 🚴','sucesso');
  document.getElementById('form-delivery').reset();
  [end,ped].forEach(limparErro);
}

function finalizarAssinatura(event) {
  event.preventDefault();
  const nome = document.getElementById('nome-paciente');
  const med  = document.getElementById('medicamento-assinatura');
  const freq = document.getElementById('frequencia');
  let ok = true;
  if (!nome.value.trim()) { mostrarErro(nome,'Informe o nome do paciente.'); ok=false; } else limparErro(nome);
  if (!med.value.trim())  { mostrarErro(med,'Informe o medicamento e a dosagem.'); ok=false; } else limparErro(med);
  if (!ok) return;
  mostrarAviso(`Assinatura de ${med.value} (${freq.value}) confirmada! 📦`, 'sucesso');
  document.getElementById('form-assinatura').reset();
  [nome,med].forEach(limparErro);
}

// =============================================================
// ADMIN
// =============================================================
function verificarAdmin() {
  const senha = document.getElementById('admin-senha')?.value;
  if (senha === 'admin123') {
    Store.set('beo_admin', true);
    document.getElementById('admin-login-wrap').style.display = 'none';
    document.getElementById('admin-painel').style.display = '';
    carregarAdmin();
  } else {
    mostrarAviso('Senha incorreta.','erro');
  }
}

function carregarAdmin() {
  const pedidos  = getPedidos();
  const usuarios = getUsuarios();
  const receita  = pedidos.reduce((a,p) => a + p.total, 0);

  const set = (id,v) => { const el=document.getElementById(id); if(el) el.textContent=v; };
  set('admin-total-pedidos',  pedidos.length);
  set('admin-total-usuarios', usuarios.length);
  set('admin-receita',        formatarPreco(receita));
  set('admin-produtos',       PRODUTOS.length);

  const tabelaPedidos = document.getElementById('tabela-pedidos');
  if (tabelaPedidos) {
    tabelaPedidos.innerHTML = pedidos.length
      ? pedidos.map(p=>`
          <tr>
            <td>#${p.numero}</td>
            <td>${p.data}</td>
            <td>${p.itens.length} ${p.itens.length===1?'item':'itens'}</td>
            <td>${formatarPreco(p.total)}</td>
            <td><span class="badge-status status-processando">${p.status}</span></td>
          </tr>`).join('')
      : '<tr><td colspan="5" style="text-align:center;color:var(--cor-texto-suave);padding:20px">Nenhum pedido ainda.</td></tr>';
  }

  const tabelaUsuarios = document.getElementById('tabela-usuarios');
  if (tabelaUsuarios) {
    tabelaUsuarios.innerHTML = usuarios.length
      ? usuarios.map(u=>`
          <tr>
            <td>${u.nome}</td>
            <td>${u.email}</td>
            <td>${u.nasc || '—'}</td>
          </tr>`).join('')
      : '<tr><td colspan="3" style="text-align:center;color:var(--cor-texto-suave);padding:20px">Nenhum usuário cadastrado.</td></tr>';
  }
}

// =============================================================
// VALIDAÇÃO VISUAL
// =============================================================
function mostrarErro(campo, msg) {
  campo.classList.add('campo-erro');
  let el = campo.parentElement.querySelector('.mensagem-erro');
  if (!el) {
    el = document.createElement('span');
    el.className = 'mensagem-erro';
    el.setAttribute('role','alert');
    campo.parentElement.appendChild(el);
  }
  el.textContent = '⚠ ' + msg;
}
function limparErro(campo) {
  campo.classList.remove('campo-erro');
  campo.parentElement?.querySelector('.mensagem-erro')?.remove();
}

// =============================================================
// FORÇA DA SENHA
// =============================================================
function avaliarSenha(valor) {
  let score = 0;
  if (valor.length >= 6)  score++;
  if (valor.length >= 10) score++;
  if (/[A-Z]/.test(valor))  score++;
  if (/[0-9]/.test(valor))  score++;
  if (/[^A-Za-z0-9]/.test(valor)) score++;

  const labels  = ['','Fraca','Razoável','Boa','Forte','Muito Forte'];
  const cores   = ['','#E63946','#f97316','#eab308','#22c55e','#16a34a'];
  const larguras = ['0%','20%','40%','60%','80%','100%'];

  const barra  = document.getElementById('barra-forca-fill');
  const rotulo = document.getElementById('forca-label');
  if (barra)  { barra.style.width = larguras[score]; barra.style.backgroundColor = cores[score]; }
  if (rotulo) { rotulo.textContent = labels[score]; rotulo.style.color = cores[score]; }
}

// =============================================================
// MÁSCARAS
// =============================================================
function mascaraCPF(v) {
  return v.replace(/\D/g,'')
    .replace(/(\d{3})(\d)/,'$1.$2')
    .replace(/(\d{3})(\d)/,'$1.$2')
    .replace(/(\d{3})(\d{1,2})$/,'$1-$2')
    .substring(0,14);
}
function mascaraTelefone(v) {
  const n = v.replace(/\D/g,'');
  return n.length <= 10
    ? n.replace(/^(\d{2})(\d{4})(\d{0,4})/,'($1) $2-$3').trim()
    : n.replace(/^(\d{2})(\d{5})(\d{0,4})/,'($1) $2-$3').substring(0,15);
}

// =============================================================
// TOAST
// =============================================================
let _toastTimer = null;
function mostrarAviso(msg, tipo = 'info') {
  const toast = document.getElementById('toast');
  if (!toast) return;
  toast.textContent = msg;
  toast.className   = `toast mostrar ${tipo}`;
  if (_toastTimer) clearTimeout(_toastTimer);
  _toastTimer = setTimeout(() => { toast.className = 'toast'; }, 3500);
}

// =============================================================
// INICIALIZAÇÃO
// =============================================================
document.addEventListener('DOMContentLoaded', async () => {
  // 1. Tema (antes de injetar header para evitar flash)
  initTema();

  // 2. Tenta carregar produtos reais da API C# antes de renderizar telas
  await carregarProdutosDaApi();

  // 3. Cabeçalho dinâmico
  injetarCabecalho();

  const pag = window.location.pathname.split('/').pop() || 'index.html';

  // 3. Catálogo
  if (pag === 'medicamentos.html') {
    const busca = new URLSearchParams(window.location.search).get('busca') || '';
    if (busca) {
      const cb = document.getElementById('campo-busca');
      if (cb) cb.value = busca;
    }
    // Skeleton enquanto renderiza
    const grade = document.getElementById('grade-produtos');
    if (grade) {
      grade.innerHTML = Array(6).fill(`
        <div class="skeleton-card">
          <div class="skeleton skeleton-img"></div>
          <div class="skeleton skeleton-line media"></div>
          <div class="skeleton skeleton-line curta"></div>
          <div class="skeleton skeleton-line"></div>
        </div>`).join('');
      setTimeout(() => {
        renderizarCatalogo([], busca);
        document.querySelectorAll('.lista-filtros input').forEach(cb =>
          cb.addEventListener('change', filtrarProdutos)
        );
      }, 400);
    }
  }

  // 4. Produto individual
  if (pag === 'produto.html') renderizarProduto();

  // 5. Carrinho
  if (pag === 'carrinho.html') {
    renderizarCarrinho();
    const inputCep = document.getElementById('input-cep');
    if (inputCep) inputCep.addEventListener('input', e => { e.target.value = mascaraCEP(e.target.value); });
  }

  // 6. Confirmação
  if (pag === 'confirmacao.html') renderizarConfirmacao();

  // 7. Conta
  if (pag === 'conta.html') renderizarConta();

  // 8. Admin
  if (pag === 'admin.html') {
    if (Store.get('beo_admin')) {
      document.getElementById('admin-login-wrap').style.display = 'none';
      document.getElementById('admin-painel').style.display = '';
      carregarAdmin();
    }
  }

  // 9. Máscaras
  const cpfEl = document.getElementById('cpf-cadastro');
  if (cpfEl) cpfEl.addEventListener('input', e => e.target.value = mascaraCPF(e.target.value));
  const telEl = document.getElementById('telefone-cadastro');
  if (telEl) telEl.addEventListener('input', e => e.target.value = mascaraTelefone(e.target.value));

  // 10. Força da senha
  const senhaEl = document.getElementById('senha-cadastro');
  if (senhaEl) senhaEl.addEventListener('input', e => avaliarSenha(e.target.value));

  // 11. Limpar erro ao digitar
  document.querySelectorAll('.grupo-input input,.grupo-input textarea,.grupo-input select')
    .forEach(el => {
      el.addEventListener('input',  () => limparErro(el));
      el.addEventListener('change', () => limparErro(el));
    });

  // 12. Fechar modal ao clicar fora
  document.getElementById('overlay-modal')?.addEventListener('click', e => {
    if (e.target.id === 'overlay-modal') fecharModal();
  });

  // 13. Fecha menu mobile ao clicar em link
  document.querySelectorAll('.menu a').forEach(a =>
    a.addEventListener('click', () => {
      document.getElementById('menu-nav')?.classList.remove('aberto');
      document.getElementById('btn-hamburguer')?.classList.remove('ativo');
    })
  );
});