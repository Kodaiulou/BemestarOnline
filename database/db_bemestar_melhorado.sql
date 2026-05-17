-- =============================================================
-- Banco de Dados: Bem Estar Online
-- Versão melhorada para farmácia virtual/e-commerce
-- SGBD alvo: MySQL 8+
-- =============================================================

DROP DATABASE IF EXISTS db_bemestar;
CREATE DATABASE db_bemestar CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE db_bemestar;

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- =============================================================
-- Tabela de usuários/clientes
-- =============================================================
CREATE TABLE usuarios (
    id_usuario INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(120) NOT NULL,
    data_nascimento DATE NULL,
    email VARCHAR(150) NOT NULL,
    cpf VARCHAR(14) NOT NULL,
    telefone VARCHAR(20) NULL,
    senha_hash VARCHAR(255) NOT NULL,
    perfil ENUM('CLIENTE','ADMIN') NOT NULL DEFAULT 'CLIENTE',
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    atualizado_em DATETIME NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT uk_usuarios_email UNIQUE (email),
    CONSTRAINT uk_usuarios_cpf UNIQUE (cpf)
) ENGINE=InnoDB;

-- =============================================================
-- Endereços de entrega dos usuários
-- =============================================================
CREATE TABLE enderecos (
    id_endereco INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    apelido VARCHAR(60) NULL,
    cep VARCHAR(10) NOT NULL,
    logradouro VARCHAR(120) NOT NULL,
    numero VARCHAR(20) NOT NULL,
    complemento VARCHAR(120) NULL,
    bairro VARCHAR(80) NOT NULL,
    cidade VARCHAR(80) NOT NULL,
    estado CHAR(2) NOT NULL,
    principal BOOLEAN NOT NULL DEFAULT FALSE,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_enderecos_usuarios FOREIGN KEY (id_usuario)
        REFERENCES usuarios(id_usuario) ON DELETE CASCADE ON UPDATE CASCADE,
    INDEX idx_enderecos_usuario (id_usuario),
    INDEX idx_enderecos_cep (cep)
) ENGINE=InnoDB;

-- =============================================================
-- Categorias de produtos
-- =============================================================
CREATE TABLE categorias (
    id_categoria INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(80) NOT NULL,
    slug VARCHAR(80) NOT NULL,
    descricao VARCHAR(255) NULL,
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    CONSTRAINT uk_categorias_slug UNIQUE (slug)
) ENGINE=InnoDB;

-- =============================================================
-- Medicamentos e produtos farmacêuticos
-- =============================================================
CREATE TABLE medicamentos (
    id_medicamento INT AUTO_INCREMENT PRIMARY KEY,
    id_categoria INT NOT NULL,
    nome VARCHAR(180) NOT NULL,
    slug VARCHAR(180) NOT NULL,
    tipo VARCHAR(80) NOT NULL,
    descricao TEXT NOT NULL,
    dosagem VARCHAR(80) NULL,
    fabricante VARCHAR(150) NOT NULL,
    via_administracao VARCHAR(80) NULL,
    registro_anvisa VARCHAR(50) NULL,
    validade_lote DATE NULL,
    preco DECIMAL(10,2) NOT NULL,
    imagem_url VARCHAR(255) NULL,
    exige_receita BOOLEAN NOT NULL DEFAULT FALSE,
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    atualizado_em DATETIME NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_medicamentos_categorias FOREIGN KEY (id_categoria)
        REFERENCES categorias(id_categoria) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT uk_medicamentos_slug UNIQUE (slug),
    CONSTRAINT ck_medicamentos_preco CHECK (preco >= 0),
    INDEX idx_medicamentos_nome (nome),
    INDEX idx_medicamentos_categoria (id_categoria),
    FULLTEXT INDEX ftx_medicamentos_busca (nome, descricao, fabricante)
) ENGINE=InnoDB;

-- =============================================================
-- Controle de estoque por produto
-- =============================================================
CREATE TABLE estoques (
    id_estoque INT AUTO_INCREMENT PRIMARY KEY,
    id_medicamento INT NOT NULL,
    quantidade INT NOT NULL DEFAULT 0,
    quantidade_minima INT NOT NULL DEFAULT 5,
    localizacao VARCHAR(80) NULL,
    atualizado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_estoques_medicamentos FOREIGN KEY (id_medicamento)
        REFERENCES medicamentos(id_medicamento) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT uk_estoques_medicamento UNIQUE (id_medicamento),
    CONSTRAINT ck_estoques_quantidade CHECK (quantidade >= 0),
    CONSTRAINT ck_estoques_minima CHECK (quantidade_minima >= 0)
) ENGINE=InnoDB;

-- =============================================================
-- Cupons promocionais
-- =============================================================
CREATE TABLE cupons (
    id_cupom INT AUTO_INCREMENT PRIMARY KEY,
    codigo VARCHAR(40) NOT NULL,
    descricao VARCHAR(180) NOT NULL,
    tipo ENUM('PERCENTUAL','VALOR_FIXO','FRETE_GRATIS') NOT NULL,
    valor DECIMAL(10,2) NOT NULL DEFAULT 0,
    valor_minimo_pedido DECIMAL(10,2) NOT NULL DEFAULT 0,
    data_inicio DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    data_fim DATETIME NULL,
    limite_uso INT NULL,
    usos_realizados INT NOT NULL DEFAULT 0,
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    CONSTRAINT uk_cupons_codigo UNIQUE (codigo),
    CONSTRAINT ck_cupons_valor CHECK (valor >= 0),
    CONSTRAINT ck_cupons_minimo CHECK (valor_minimo_pedido >= 0)
) ENGINE=InnoDB;

-- =============================================================
-- Carrinho ativo por usuário
-- =============================================================
CREATE TABLE carrinhos (
    id_carrinho INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    atualizado_em DATETIME NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_carrinhos_usuarios FOREIGN KEY (id_usuario)
        REFERENCES usuarios(id_usuario) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT uk_carrinhos_usuario UNIQUE (id_usuario)
) ENGINE=InnoDB;

CREATE TABLE carrinho_itens (
    id_item_carrinho INT AUTO_INCREMENT PRIMARY KEY,
    id_carrinho INT NOT NULL,
    id_medicamento INT NOT NULL,
    quantidade INT NOT NULL,
    preco_unitario DECIMAL(10,2) NOT NULL,
    adicionado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_carrinho_itens_carrinhos FOREIGN KEY (id_carrinho)
        REFERENCES carrinhos(id_carrinho) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk_carrinho_itens_medicamentos FOREIGN KEY (id_medicamento)
        REFERENCES medicamentos(id_medicamento) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT uk_carrinho_item UNIQUE (id_carrinho, id_medicamento),
    CONSTRAINT ck_carrinho_itens_quantidade CHECK (quantidade > 0),
    CONSTRAINT ck_carrinho_itens_preco CHECK (preco_unitario >= 0)
) ENGINE=InnoDB;

-- =============================================================
-- Pedidos e itens do pedido
-- =============================================================
CREATE TABLE pedidos (
    id_pedido INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    id_endereco INT NOT NULL,
    id_cupom INT NULL,
    status ENUM('CRIADO','AGUARDANDO_PAGAMENTO','PAGO','EM_SEPARACAO','SAIU_PARA_ENTREGA','ENTREGUE','CANCELADO') NOT NULL DEFAULT 'CRIADO',
    subtotal DECIMAL(10,2) NOT NULL DEFAULT 0,
    desconto DECIMAL(10,2) NOT NULL DEFAULT 0,
    frete DECIMAL(10,2) NOT NULL DEFAULT 0,
    total DECIMAL(10,2) NOT NULL DEFAULT 0,
    observacao VARCHAR(255) NULL,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    atualizado_em DATETIME NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_pedidos_usuarios FOREIGN KEY (id_usuario)
        REFERENCES usuarios(id_usuario) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT fk_pedidos_enderecos FOREIGN KEY (id_endereco)
        REFERENCES enderecos(id_endereco) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT fk_pedidos_cupons FOREIGN KEY (id_cupom)
        REFERENCES cupons(id_cupom) ON DELETE SET NULL ON UPDATE CASCADE,
    CONSTRAINT ck_pedidos_valores CHECK (subtotal >= 0 AND desconto >= 0 AND frete >= 0 AND total >= 0),
    INDEX idx_pedidos_usuario (id_usuario),
    INDEX idx_pedidos_status (status),
    INDEX idx_pedidos_criado (criado_em)
) ENGINE=InnoDB;

CREATE TABLE pedido_itens (
    id_item_pedido INT AUTO_INCREMENT PRIMARY KEY,
    id_pedido INT NOT NULL,
    id_medicamento INT NOT NULL,
    nome_produto VARCHAR(180) NOT NULL,
    quantidade INT NOT NULL,
    preco_unitario DECIMAL(10,2) NOT NULL,
    subtotal DECIMAL(10,2) NOT NULL,
    CONSTRAINT fk_pedido_itens_pedidos FOREIGN KEY (id_pedido)
        REFERENCES pedidos(id_pedido) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk_pedido_itens_medicamentos FOREIGN KEY (id_medicamento)
        REFERENCES medicamentos(id_medicamento) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT ck_pedido_itens_quantidade CHECK (quantidade > 0),
    CONSTRAINT ck_pedido_itens_valores CHECK (preco_unitario >= 0 AND subtotal >= 0),
    INDEX idx_pedido_itens_pedido (id_pedido)
) ENGINE=InnoDB;

-- =============================================================
-- Pagamentos
-- =============================================================
CREATE TABLE pagamentos (
    id_pagamento INT AUTO_INCREMENT PRIMARY KEY,
    id_pedido INT NOT NULL,
    metodo ENUM('PIX','CARTAO_CREDITO','CARTAO_DEBITO','BOLETO','DINHEIRO') NOT NULL,
    status ENUM('PENDENTE','APROVADO','RECUSADO','ESTORNADO') NOT NULL DEFAULT 'PENDENTE',
    valor DECIMAL(10,2) NOT NULL,
    codigo_transacao VARCHAR(120) NULL,
    pago_em DATETIME NULL,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_pagamentos_pedidos FOREIGN KEY (id_pedido)
        REFERENCES pedidos(id_pedido) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT ck_pagamentos_valor CHECK (valor >= 0),
    INDEX idx_pagamentos_pedido (id_pedido),
    INDEX idx_pagamentos_status (status)
) ENGINE=InnoDB;

-- =============================================================
-- Assinaturas e delivery expresso
-- =============================================================
CREATE TABLE assinaturas (
    id_assinatura INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    plano VARCHAR(80) NOT NULL,
    periodicidade ENUM('MENSAL','TRIMESTRAL','SEMESTRAL','ANUAL') NOT NULL DEFAULT 'MENSAL',
    valor DECIMAL(10,2) NOT NULL,
    status ENUM('ATIVA','PAUSADA','CANCELADA') NOT NULL DEFAULT 'ATIVA',
    criada_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    proxima_cobranca DATE NULL,
    CONSTRAINT fk_assinaturas_usuarios FOREIGN KEY (id_usuario)
        REFERENCES usuarios(id_usuario) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT ck_assinaturas_valor CHECK (valor >= 0),
    INDEX idx_assinaturas_usuario (id_usuario)
) ENGINE=InnoDB;

CREATE TABLE delivery_solicitacoes (
    id_delivery INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NULL,
    nome_cliente VARCHAR(120) NOT NULL,
    telefone VARCHAR(20) NOT NULL,
    endereco_texto VARCHAR(255) NOT NULL,
    observacao TEXT NULL,
    receita_arquivo VARCHAR(255) NULL,
    status ENUM('RECEBIDO','EM_ANALISE','EM_ROTA','CONCLUIDO','CANCELADO') NOT NULL DEFAULT 'RECEBIDO',
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_delivery_usuarios FOREIGN KEY (id_usuario)
        REFERENCES usuarios(id_usuario) ON DELETE SET NULL ON UPDATE CASCADE,
    INDEX idx_delivery_status (status),
    INDEX idx_delivery_criado (criado_em)
) ENGINE=InnoDB;

SET FOREIGN_KEY_CHECKS = 1;

-- =============================================================
-- Dados iniciais
-- Senha de demonstração armazenada como texto apenas para ambiente acadêmico.
-- Em produção, a API deve salvar hash seguro.
-- =============================================================
INSERT INTO usuarios (nome, data_nascimento, email, cpf, telefone, senha_hash, perfil)
VALUES
('Administrador Bem Estar', '1990-01-01', 'admin@bemestaronline.com', '000.000.000-00', '(11) 90000-0000', 'Admin@123', 'ADMIN'),
('Cliente Demonstração', '1998-05-20', 'cliente@bemestaronline.com', '111.111.111-11', '(11) 98888-7777', 'Cliente@123', 'CLIENTE');

INSERT INTO categorias (nome, slug, descricao)
VALUES
('Genéricos', 'genericos', 'Medicamentos genéricos aprovados pelos órgãos reguladores.'),
('Referência', 'referencia', 'Medicamentos de referência e marcas reconhecidas.'),
('Higiene', 'higiene', 'Produtos de higiene pessoal e cuidados diários.'),
('Dermocosméticos', 'dermocosmeticos', 'Produtos para cuidado com pele, cabelo e proteção solar.'),
('Primeiros Socorros', 'primeiros-socorros', 'Itens de apoio para pequenos cuidados e emergências domésticas.');

INSERT INTO medicamentos (id_categoria, nome, slug, tipo, descricao, dosagem, fabricante, via_administracao, registro_anvisa, validade_lote, preco, exige_receita)
VALUES
(1, 'Losartana Potássica 50mg', 'losartana-potassica-50mg', 'Genérico', 'Anti-hipertensivo utilizado no tratamento da hipertensão arterial.', '50mg', 'EMS Sigma Pharma', 'Oral', '1234567890', '2027-12-31', 12.90, TRUE),
(4, 'Protetor Solar FPS 50', 'protetor-solar-fps-50', 'Dermocosmético', 'Protetor solar de amplo espectro com proteção UVA e UVB.', '200ml', 'Bem Care', 'Tópica', NULL, '2027-08-31', 54.90, FALSE),
(1, 'Dipirona Sódica 500mg', 'dipirona-sodica-500mg', 'Genérico', 'Analgésico e antitérmico indicado para dores leves a moderadas e febre.', '500mg', 'Neo Química', 'Oral', '9876543210', '2027-10-30', 4.50, FALSE),
(3, 'Sabonete Líquido Neutro', 'sabonete-liquido-neutro', 'Higiene Pessoal', 'Sabonete líquido de pH neutro para peles sensíveis.', '250ml', 'Bem Care', 'Tópica', NULL, '2028-01-31', 18.20, FALSE),
(5, 'Kit Curativos Variados', 'kit-curativos-variados', 'Primeiros Socorros', 'Kit com curativos de diferentes tamanhos e formatos.', '40 unidades', 'Health First', 'Tópica', NULL, '2028-06-30', 9.90, FALSE),
(1, 'Vitamina C 1g', 'vitamina-c-1g', 'Suplemento', 'Suplemento de ácido ascórbico em pastilhas efervescentes.', '1g', 'VitaBem', 'Oral', NULL, '2027-11-30', 15.00, FALSE),
(2, 'Ibuprofeno 400mg', 'ibuprofeno-400mg', 'Referência', 'Anti-inflamatório não esteroidal com ação analgésica e antitérmica.', '400mg', 'Sanofi', 'Oral', '1122334455', '2027-09-30', 8.90, FALSE),
(4, 'Hidratante Corporal', 'hidratante-corporal', 'Dermocosmético', 'Creme hidratante corporal com manteiga de karité e vitamina E.', '400ml', 'Bem Care', 'Tópica', NULL, '2028-04-30', 32.50, FALSE);

INSERT INTO estoques (id_medicamento, quantidade, quantidade_minima, localizacao)
SELECT id_medicamento, 100, 10, 'Prateleira A' FROM medicamentos;

INSERT INTO cupons (codigo, descricao, tipo, valor, valor_minimo_pedido, data_fim)
VALUES
('BEMSTAR10', '10% de desconto em produtos selecionados', 'PERCENTUAL', 10.00, 20.00, '2027-12-31 23:59:59'),
('SAUDE15', '15% de desconto para campanha de saúde', 'PERCENTUAL', 15.00, 50.00, '2027-12-31 23:59:59'),
('FRETEGRATIS', 'Frete grátis para pedidos elegíveis', 'FRETE_GRATIS', 0.00, 40.00, '2027-12-31 23:59:59'),
('PRIMEIRACOMPRA', 'R$ 5,00 de desconto na primeira compra', 'VALOR_FIXO', 5.00, 15.00, '2027-12-31 23:59:59');

INSERT INTO enderecos (id_usuario, apelido, cep, logradouro, numero, complemento, bairro, cidade, estado, principal)
VALUES (2, 'Casa', '01001-000', 'Praça da Sé', '100', 'Apto 10', 'Sé', 'São Paulo', 'SP', TRUE);

-- =============================================================
-- Visão auxiliar para listagem de produtos com estoque e categoria
-- =============================================================
CREATE OR REPLACE VIEW vw_produtos_catalogo AS
SELECT
    m.id_medicamento,
    m.nome,
    m.slug,
    c.nome AS categoria,
    c.slug AS categoria_slug,
    m.tipo,
    m.descricao,
    m.dosagem,
    m.fabricante,
    m.preco,
    m.exige_receita,
    COALESCE(e.quantidade, 0) AS estoque,
    m.ativo
FROM medicamentos m
JOIN categorias c ON c.id_categoria = m.id_categoria
LEFT JOIN estoques e ON e.id_medicamento = m.id_medicamento;
