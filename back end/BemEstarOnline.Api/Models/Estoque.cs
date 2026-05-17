using System.ComponentModel.DataAnnotations;

namespace BemEstarOnline.Api.Models;

public class Estoque
{
    public int IdEstoque { get; set; }
    public int IdMedicamento { get; set; }
    public int Quantidade { get; set; }
    public int QuantidadeMinima { get; set; } = 5;
    [MaxLength(80)] public string? Localizacao { get; set; }
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
    public Medicamento? Medicamento { get; set; }
}
