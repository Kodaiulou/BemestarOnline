using System.ComponentModel.DataAnnotations;

namespace BemEstarOnline.Api.Models;

public class DeliverySolicitacao
{
    public int IdDelivery { get; set; }
    public int? IdUsuario { get; set; }
    [Required, MaxLength(120)] public string NomeCliente { get; set; } = string.Empty;
    [Required, MaxLength(20)] public string Telefone { get; set; } = string.Empty;
    [Required, MaxLength(255)] public string EnderecoTexto { get; set; } = string.Empty;
    public string? Observacao { get; set; }
    [MaxLength(255)] public string? ReceitaArquivo { get; set; }
    [Required, MaxLength(20)] public string Status { get; set; } = "RECEBIDO";
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public Usuario? Usuario { get; set; }
}
