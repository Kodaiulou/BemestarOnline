using System.ComponentModel.DataAnnotations;

namespace BemEstarOnline.Api.Models;

public class Usuario
{
    public int IdUsuario { get; set; }

    [Required, MaxLength(120)]
    public string Nome { get; set; } = string.Empty;

    public DateTime? DataNascimento { get; set; }

    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required, MaxLength(14)]
    public string Cpf { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Telefone { get; set; }

    [Required, MaxLength(255)]
    public string SenhaHash { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string Perfil { get; set; } = "CLIENTE";

    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime? AtualizadoEm { get; set; }

    public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
    public Carrinho? Carrinho { get; set; }
    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
