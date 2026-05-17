namespace BemEstarOnline.Api.Models;

public class Carrinho
{
    public int IdCarrinho { get; set; }
    public int IdUsuario { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime? AtualizadoEm { get; set; }
    public Usuario? Usuario { get; set; }
    public ICollection<CarrinhoItem> Itens { get; set; } = new List<CarrinhoItem>();
}
