using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BemEstarOnline.Api.Models;

public class Medicamento
{
    public int IdMedicamento { get; set; }
    public int IdCategoria { get; set; }
    [Required, MaxLength(180)] public string Nome { get; set; } = string.Empty;
    [Required, MaxLength(180)] public string Slug { get; set; } = string.Empty;
    [Required, MaxLength(80)] public string Tipo { get; set; } = string.Empty;
    [Required] public string Descricao { get; set; } = string.Empty;
    [MaxLength(80)] public string? Dosagem { get; set; }
    [Required, MaxLength(150)] public string Fabricante { get; set; } = string.Empty;
    [MaxLength(80)] public string? ViaAdministracao { get; set; }
    [MaxLength(50)] public string? RegistroAnvisa { get; set; }
    public DateTime? ValidadeLote { get; set; }
    [Column(TypeName = "decimal(10,2)")] public decimal Preco { get; set; }
    [MaxLength(255)] public string? ImagemUrl { get; set; }
    public bool ExigeReceita { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime? AtualizadoEm { get; set; }
    public Categoria? Categoria { get; set; }
    public Estoque? Estoque { get; set; }
}
