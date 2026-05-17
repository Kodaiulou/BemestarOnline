using System.ComponentModel.DataAnnotations;

namespace BemEstarOnline.Api.Models;

public class Categoria
{
    public int IdCategoria { get; set; }
    [Required, MaxLength(80)] public string Nome { get; set; } = string.Empty;
    [Required, MaxLength(80)] public string Slug { get; set; } = string.Empty;
    [MaxLength(255)] public string? Descricao { get; set; }
    public bool Ativo { get; set; } = true;
    public ICollection<Medicamento> Medicamentos { get; set; } = new List<Medicamento>();
}
