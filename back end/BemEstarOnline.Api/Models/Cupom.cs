using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BemEstarOnline.Api.Models;

public class Cupom
{
    public int IdCupom { get; set; }
    [Required, MaxLength(40)] public string Codigo { get; set; } = string.Empty;
    [Required, MaxLength(180)] public string Descricao { get; set; } = string.Empty;
    [Required, MaxLength(20)] public string Tipo { get; set; } = "PERCENTUAL";
    [Column(TypeName = "decimal(10,2)")] public decimal Valor { get; set; }
    [Column(TypeName = "decimal(10,2)")] public decimal ValorMinimoPedido { get; set; }
    public DateTime DataInicio { get; set; } = DateTime.UtcNow;
    public DateTime? DataFim { get; set; }
    public int? LimiteUso { get; set; }
    public int UsosRealizados { get; set; }
    public bool Ativo { get; set; } = true;
}
