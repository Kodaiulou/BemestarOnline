using BemEstarOnline.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BemEstarOnline.Api.Data;

public class BemEstarDbContext : DbContext
{
    public BemEstarDbContext(DbContextOptions<BemEstarDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Endereco> Enderecos => Set<Endereco>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Medicamento> Medicamentos => Set<Medicamento>();
    public DbSet<Estoque> Estoques => Set<Estoque>();
    public DbSet<Cupom> Cupons => Set<Cupom>();
    public DbSet<Carrinho> Carrinhos => Set<Carrinho>();
    public DbSet<CarrinhoItem> CarrinhoItens => Set<CarrinhoItem>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<PedidoItem> PedidoItens => Set<PedidoItem>();
    public DbSet<Pagamento> Pagamentos => Set<Pagamento>();
    public DbSet<Assinatura> Assinaturas => Set<Assinatura>();
    public DbSet<DeliverySolicitacao> DeliverySolicitacoes => Set<DeliverySolicitacao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(e =>
        {
            e.ToTable("usuarios");
            e.HasKey(x => x.IdUsuario);
            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
            e.Property(x => x.Nome).HasColumnName("nome");
            e.Property(x => x.DataNascimento).HasColumnName("data_nascimento");
            e.Property(x => x.Email).HasColumnName("email");
            e.Property(x => x.Cpf).HasColumnName("cpf");
            e.Property(x => x.Telefone).HasColumnName("telefone");
            e.Property(x => x.SenhaHash).HasColumnName("senha_hash");
            e.Property(x => x.Perfil).HasColumnName("perfil");
            e.Property(x => x.Ativo).HasColumnName("ativo");
            e.Property(x => x.CriadoEm).HasColumnName("criado_em");
            e.Property(x => x.AtualizadoEm).HasColumnName("atualizado_em");
            e.HasIndex(x => x.Email).IsUnique();
            e.HasIndex(x => x.Cpf).IsUnique();
        });

        modelBuilder.Entity<Endereco>(e =>
        {
            e.ToTable("enderecos");
            e.HasKey(x => x.IdEndereco);
            e.Property(x => x.IdEndereco).HasColumnName("id_endereco");
            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
            e.Property(x => x.Apelido).HasColumnName("apelido");
            e.Property(x => x.Cep).HasColumnName("cep");
            e.Property(x => x.Logradouro).HasColumnName("logradouro");
            e.Property(x => x.Numero).HasColumnName("numero");
            e.Property(x => x.Complemento).HasColumnName("complemento");
            e.Property(x => x.Bairro).HasColumnName("bairro");
            e.Property(x => x.Cidade).HasColumnName("cidade");
            e.Property(x => x.Estado).HasColumnName("estado");
            e.Property(x => x.Principal).HasColumnName("principal");
            e.Property(x => x.CriadoEm).HasColumnName("criado_em");
            e.HasOne(x => x.Usuario).WithMany(x => x.Enderecos).HasForeignKey(x => x.IdUsuario);
        });

        modelBuilder.Entity<Categoria>(e =>
        {
            e.ToTable("categorias");
            e.HasKey(x => x.IdCategoria);
            e.Property(x => x.IdCategoria).HasColumnName("id_categoria");
            e.Property(x => x.Nome).HasColumnName("nome");
            e.Property(x => x.Slug).HasColumnName("slug");
            e.Property(x => x.Descricao).HasColumnName("descricao");
            e.Property(x => x.Ativo).HasColumnName("ativo");
            e.HasIndex(x => x.Slug).IsUnique();
        });

        modelBuilder.Entity<Medicamento>(e =>
        {
            e.ToTable("medicamentos");
            e.HasKey(x => x.IdMedicamento);
            e.Property(x => x.IdMedicamento).HasColumnName("id_medicamento");
            e.Property(x => x.IdCategoria).HasColumnName("id_categoria");
            e.Property(x => x.Nome).HasColumnName("nome");
            e.Property(x => x.Slug).HasColumnName("slug");
            e.Property(x => x.Tipo).HasColumnName("tipo");
            e.Property(x => x.Descricao).HasColumnName("descricao");
            e.Property(x => x.Dosagem).HasColumnName("dosagem");
            e.Property(x => x.Fabricante).HasColumnName("fabricante");
            e.Property(x => x.ViaAdministracao).HasColumnName("via_administracao");
            e.Property(x => x.RegistroAnvisa).HasColumnName("registro_anvisa");
            e.Property(x => x.ValidadeLote).HasColumnName("validade_lote");
            e.Property(x => x.Preco).HasColumnName("preco");
            e.Property(x => x.ImagemUrl).HasColumnName("imagem_url");
            e.Property(x => x.ExigeReceita).HasColumnName("exige_receita");
            e.Property(x => x.Ativo).HasColumnName("ativo");
            e.Property(x => x.CriadoEm).HasColumnName("criado_em");
            e.Property(x => x.AtualizadoEm).HasColumnName("atualizado_em");
            e.HasIndex(x => x.Slug).IsUnique();
            e.HasOne(x => x.Categoria).WithMany(x => x.Medicamentos).HasForeignKey(x => x.IdCategoria);
        });

        modelBuilder.Entity<Estoque>(e =>
        {
            e.ToTable("estoques");
            e.HasKey(x => x.IdEstoque);
            e.Property(x => x.IdEstoque).HasColumnName("id_estoque");
            e.Property(x => x.IdMedicamento).HasColumnName("id_medicamento");
            e.Property(x => x.Quantidade).HasColumnName("quantidade");
            e.Property(x => x.QuantidadeMinima).HasColumnName("quantidade_minima");
            e.Property(x => x.Localizacao).HasColumnName("localizacao");
            e.Property(x => x.AtualizadoEm).HasColumnName("atualizado_em");
            e.HasIndex(x => x.IdMedicamento).IsUnique();
            e.HasOne(x => x.Medicamento).WithOne(x => x.Estoque).HasForeignKey<Estoque>(x => x.IdMedicamento);
        });

        modelBuilder.Entity<Cupom>(e =>
        {
            e.ToTable("cupons");
            e.HasKey(x => x.IdCupom);
            e.Property(x => x.IdCupom).HasColumnName("id_cupom");
            e.Property(x => x.Codigo).HasColumnName("codigo");
            e.Property(x => x.Descricao).HasColumnName("descricao");
            e.Property(x => x.Tipo).HasColumnName("tipo");
            e.Property(x => x.Valor).HasColumnName("valor");
            e.Property(x => x.ValorMinimoPedido).HasColumnName("valor_minimo_pedido");
            e.Property(x => x.DataInicio).HasColumnName("data_inicio");
            e.Property(x => x.DataFim).HasColumnName("data_fim");
            e.Property(x => x.LimiteUso).HasColumnName("limite_uso");
            e.Property(x => x.UsosRealizados).HasColumnName("usos_realizados");
            e.Property(x => x.Ativo).HasColumnName("ativo");
            e.HasIndex(x => x.Codigo).IsUnique();
        });

        modelBuilder.Entity<Carrinho>(e =>
        {
            e.ToTable("carrinhos");
            e.HasKey(x => x.IdCarrinho);
            e.Property(x => x.IdCarrinho).HasColumnName("id_carrinho");
            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
            e.Property(x => x.CriadoEm).HasColumnName("criado_em");
            e.Property(x => x.AtualizadoEm).HasColumnName("atualizado_em");
            e.HasIndex(x => x.IdUsuario).IsUnique();
            e.HasOne(x => x.Usuario).WithOne(x => x.Carrinho).HasForeignKey<Carrinho>(x => x.IdUsuario);
        });

        modelBuilder.Entity<CarrinhoItem>(e =>
        {
            e.ToTable("carrinho_itens");
            e.HasKey(x => x.IdItemCarrinho);
            e.Property(x => x.IdItemCarrinho).HasColumnName("id_item_carrinho");
            e.Property(x => x.IdCarrinho).HasColumnName("id_carrinho");
            e.Property(x => x.IdMedicamento).HasColumnName("id_medicamento");
            e.Property(x => x.Quantidade).HasColumnName("quantidade");
            e.Property(x => x.PrecoUnitario).HasColumnName("preco_unitario");
            e.Property(x => x.AdicionadoEm).HasColumnName("adicionado_em");
            e.HasIndex(x => new { x.IdCarrinho, x.IdMedicamento }).IsUnique();
            e.HasOne(x => x.Carrinho).WithMany(x => x.Itens).HasForeignKey(x => x.IdCarrinho);
            e.HasOne(x => x.Medicamento).WithMany().HasForeignKey(x => x.IdMedicamento);
        });

        modelBuilder.Entity<Pedido>(e =>
        {
            e.ToTable("pedidos");
            e.HasKey(x => x.IdPedido);
            e.Property(x => x.IdPedido).HasColumnName("id_pedido");
            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
            e.Property(x => x.IdEndereco).HasColumnName("id_endereco");
            e.Property(x => x.IdCupom).HasColumnName("id_cupom");
            e.Property(x => x.Status).HasColumnName("status");
            e.Property(x => x.Subtotal).HasColumnName("subtotal");
            e.Property(x => x.Desconto).HasColumnName("desconto");
            e.Property(x => x.Frete).HasColumnName("frete");
            e.Property(x => x.Total).HasColumnName("total");
            e.Property(x => x.Observacao).HasColumnName("observacao");
            e.Property(x => x.CriadoEm).HasColumnName("criado_em");
            e.Property(x => x.AtualizadoEm).HasColumnName("atualizado_em");
            e.HasOne(x => x.Usuario).WithMany(x => x.Pedidos).HasForeignKey(x => x.IdUsuario);
            e.HasOne(x => x.Endereco).WithMany().HasForeignKey(x => x.IdEndereco);
            e.HasOne(x => x.Cupom).WithMany().HasForeignKey(x => x.IdCupom);
        });

        modelBuilder.Entity<PedidoItem>(e =>
        {
            e.ToTable("pedido_itens");
            e.HasKey(x => x.IdItemPedido);
            e.Property(x => x.IdItemPedido).HasColumnName("id_item_pedido");
            e.Property(x => x.IdPedido).HasColumnName("id_pedido");
            e.Property(x => x.IdMedicamento).HasColumnName("id_medicamento");
            e.Property(x => x.NomeProduto).HasColumnName("nome_produto");
            e.Property(x => x.Quantidade).HasColumnName("quantidade");
            e.Property(x => x.PrecoUnitario).HasColumnName("preco_unitario");
            e.Property(x => x.Subtotal).HasColumnName("subtotal");
            e.HasOne(x => x.Pedido).WithMany(x => x.Itens).HasForeignKey(x => x.IdPedido);
            e.HasOne(x => x.Medicamento).WithMany().HasForeignKey(x => x.IdMedicamento);
        });

        modelBuilder.Entity<Pagamento>(e =>
        {
            e.ToTable("pagamentos");
            e.HasKey(x => x.IdPagamento);
            e.Property(x => x.IdPagamento).HasColumnName("id_pagamento");
            e.Property(x => x.IdPedido).HasColumnName("id_pedido");
            e.Property(x => x.Metodo).HasColumnName("metodo");
            e.Property(x => x.Status).HasColumnName("status");
            e.Property(x => x.Valor).HasColumnName("valor");
            e.Property(x => x.CodigoTransacao).HasColumnName("codigo_transacao");
            e.Property(x => x.PagoEm).HasColumnName("pago_em");
            e.Property(x => x.CriadoEm).HasColumnName("criado_em");
            e.HasOne(x => x.Pedido).WithMany(x => x.Pagamentos).HasForeignKey(x => x.IdPedido);
        });

        modelBuilder.Entity<Assinatura>(e =>
        {
            e.ToTable("assinaturas");
            e.HasKey(x => x.IdAssinatura);
            e.Property(x => x.IdAssinatura).HasColumnName("id_assinatura");
            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
            e.Property(x => x.Plano).HasColumnName("plano");
            e.Property(x => x.Periodicidade).HasColumnName("periodicidade");
            e.Property(x => x.Valor).HasColumnName("valor");
            e.Property(x => x.Status).HasColumnName("status");
            e.Property(x => x.CriadaEm).HasColumnName("criada_em");
            e.Property(x => x.ProximaCobranca).HasColumnName("proxima_cobranca");
            e.HasOne(x => x.Usuario).WithMany().HasForeignKey(x => x.IdUsuario);
        });

        modelBuilder.Entity<DeliverySolicitacao>(e =>
        {
            e.ToTable("delivery_solicitacoes");
            e.HasKey(x => x.IdDelivery);
            e.Property(x => x.IdDelivery).HasColumnName("id_delivery");
            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
            e.Property(x => x.NomeCliente).HasColumnName("nome_cliente");
            e.Property(x => x.Telefone).HasColumnName("telefone");
            e.Property(x => x.EnderecoTexto).HasColumnName("endereco_texto");
            e.Property(x => x.Observacao).HasColumnName("observacao");
            e.Property(x => x.ReceitaArquivo).HasColumnName("receita_arquivo");
            e.Property(x => x.Status).HasColumnName("status");
            e.Property(x => x.CriadoEm).HasColumnName("criado_em");
            e.HasOne(x => x.Usuario).WithMany().HasForeignKey(x => x.IdUsuario);
        });
    }
}
