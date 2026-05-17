using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BemEstarOnline.Api.Models;

public class Assinatura
{
    public int IdAssinatura { get; set; }
    public int IdUsuario { get; set; }
    [Required, MaxLength(80)] public string Plano { get; set; } = string.Empty;
    [Required, MaxLength(20)] public string Periodicidade { get; set; } = "MENSAL";
    [Column(TypeName = "decimal(10,2)")] public decimal Valor { get; set; }
    [Required, MaxLength(20)] public string Status { get; set; } = "ATIVA";
    public DateTime CriadaEm { get; set; } = DateTime.UtcNow;
    public DateTime? ProximaCobranca { get; set; }
    public Usuario? Usuario { get; set; }
}
