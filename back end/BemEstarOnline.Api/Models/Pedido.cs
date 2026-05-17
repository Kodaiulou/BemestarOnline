using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BemEstarOnline.Api.Models;

public class Pedido
{
    public int IdPedido { get; set; }
    public int IdUsuario { get; set; }
    public int IdEndereco { get; set; }
    public int? IdCupom { get; set; }
    [Required, MaxLength(30)] public string Status { get; set; } = "CRIADO";
    [Column(TypeName = "decimal(10,2)")] public decimal Subtotal { get; set; }
    [Column(TypeName = "decimal(10,2)")] public decimal Desconto { get; set; }
    [Column(TypeName = "decimal(10,2)")] public decimal Frete { get; set; }
    [Column(TypeName = "decimal(10,2)")] public decimal Total { get; set; }
    [MaxLength(255)] public string? Observacao { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime? AtualizadoEm { get; set; }
    public Usuario? Usuario { get; set; }
    public Endereco? Endereco { get; set; }
    public Cupom? Cupom { get; set; }
    public ICollection<PedidoItem> Itens { get; set; } = new List<PedidoItem>();
    public ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
}
