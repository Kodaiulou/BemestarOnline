using System.ComponentModel.DataAnnotations;

namespace BemEstarOnline.Api.Dtos;

public record UsuarioResumoDto(int IdUsuario, string Nome, string Email, string Perfil);
public record LoginDto([Required, EmailAddress] string Email, [Required] string Senha);
public record RegistroDto([Required] string Nome, DateTime? DataNascimento, [Required, EmailAddress] string Email, [Required] string Cpf, string? Telefone, [Required] string Senha);
public record ProdutoDto(int IdMedicamento, string Nome, string Slug, string Categoria, string CategoriaSlug, string Tipo, string Descricao, string? Dosagem, string Fabricante, decimal Preco, bool ExigeReceita, int Estoque);
public record ProdutoCriarDto([Required] string Nome, [Required] string Slug, int IdCategoria, [Required] string Tipo, [Required] string Descricao, string? Dosagem, [Required] string Fabricante, string? ViaAdministracao, string? RegistroAnvisa, DateTime? ValidadeLote, decimal Preco, bool ExigeReceita, int EstoqueInicial);
public record CarrinhoItemDto(int IdMedicamento, string Nome, decimal PrecoUnitario, int Quantidade, decimal Subtotal);
public record CarrinhoDto(int IdCarrinho, int IdUsuario, IReadOnlyList<CarrinhoItemDto> Itens, decimal Total);
public record CarrinhoAdicionarDto(int IdUsuario, int IdMedicamento, int Quantidade);
public record EnderecoCriarDto(int IdUsuario, string Cep, string Logradouro, string Numero, string? Complemento, string Bairro, string Cidade, string Estado);
public record PedidoCriarDto(int IdUsuario, int IdEndereco, string? CodigoCupom, decimal Frete, string MetodoPagamento, string? Observacao);
public record PedidoResumoDto(int IdPedido, string Status, decimal Total, DateTime CriadoEm);
public record CupomValidacaoDto(string Codigo, decimal Subtotal, decimal Frete);
public record CupomResultadoDto(bool Valido, string Mensagem, decimal Desconto, decimal FreteCalculado, decimal Total);
public record DeliveryCriarDto(int? IdUsuario, string NomeCliente, string Telefone, string EnderecoTexto, string? Observacao, string? ReceitaArquivo);
public record AssinaturaCriarDto(int IdUsuario, string Plano, string Periodicidade, decimal Valor);
