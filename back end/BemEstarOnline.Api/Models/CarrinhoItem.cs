using System.ComponentModel.DataAnnotations.Schema;

namespace BemEstarOnline.Api.Models;

public class CarrinhoItem
{
    public int IdItemCarrinho { get; set; }
    public int IdCarrinho { get; set; }
    public int IdMedicamento { get; set; }
    public int Quantidade { get; set; }
    [Column(TypeName = "decimal(10,2)")] public decimal PrecoUnitario { get; set; }
    public DateTime AdicionadoEm { get; set; } = DateTime.UtcNow;
    public Carrinho? Carrinho { get; set; }
    public Medicamento? Medicamento { get; set; }
}
