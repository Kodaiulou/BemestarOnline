using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BemEstarOnline.Api.Models;

public class PedidoItem
{
    public int IdItemPedido { get; set; }
    public int IdPedido { get; set; }
    public int IdMedicamento { get; set; }
    [Required, MaxLength(180)] public string NomeProduto { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    [Column(TypeName = "decimal(10,2)")] public decimal PrecoUnitario { get; set; }
    [Column(TypeName = "decimal(10,2)")] public decimal Subtotal { get; set; }
    public Pedido? Pedido { get; set; }
    public Medicamento? Medicamento { get; set; }
}
