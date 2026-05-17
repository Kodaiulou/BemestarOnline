using System.ComponentModel.DataAnnotations;

namespace BemEstarOnline.Api.Models;

public class Endereco
{
    public int IdEndereco { get; set; }
    public int IdUsuario { get; set; }
    [MaxLength(60)] public string? Apelido { get; set; }
    [Required, MaxLength(10)] public string Cep { get; set; } = string.Empty;
    [Required, MaxLength(120)] public string Logradouro { get; set; } = string.Empty;
    [Required, MaxLength(20)] public string Numero { get; set; } = string.Empty;
    [MaxLength(120)] public string? Complemento { get; set; }
    [Required, MaxLength(80)] public string Bairro { get; set; } = string.Empty;
    [Required, MaxLength(80)] public string Cidade { get; set; } = string.Empty;
    [Required, MaxLength(2)] public string Estado { get; set; } = string.Empty;
    public bool Principal { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public Usuario? Usuario { get; set; }
}
