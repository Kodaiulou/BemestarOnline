using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BemEstarOnline.Api.Models;

public class Pagamento
{
    public int IdPagamento { get; set; }
    public int IdPedido { get; set; }
    [Required, MaxLength(30)] public string Metodo { get; set; } = "PIX";
    [Required, MaxLength(20)] public string Status { get; set; } = "PENDENTE";
    [Column(TypeName = "decimal(10,2)")] public decimal Valor { get; set; }
    [MaxLength(120)] public string? CodigoTransacao { get; set; }
    public DateTime? PagoEm { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public Pedido? Pedido { get; set; }
}
