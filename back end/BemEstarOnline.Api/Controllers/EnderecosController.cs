using BemEstarOnline.Api.Data;
using BemEstarOnline.Api.Dtos;
using BemEstarOnline.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BemEstarOnline.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnderecosController : ControllerBase
{
    private readonly BemEstarDbContext _db;
    public EnderecosController(BemEstarDbContext db) => _db = db;

    [HttpGet("usuario/{idUsuario:int}")]
    public async Task<IActionResult> PorUsuario(int idUsuario) => Ok(await _db.Enderecos.Where(e => e.IdUsuario == idUsuario).ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Criar(EnderecoCriarDto dto)
    {
        if (!await _db.Usuarios.AnyAsync(u => u.IdUsuario == dto.IdUsuario)) return BadRequest(new { mensagem = "Usuário inválido." });
        var endereco = new Endereco { IdUsuario = dto.IdUsuario, Cep = dto.Cep, Logradouro = dto.Logradouro, Numero = dto.Numero, Complemento = dto.Complemento, Bairro = dto.Bairro, Cidade = dto.Cidade, Estado = dto.Estado, Principal = true };
        _db.Enderecos.Add(endereco);
        await _db.SaveChangesAsync();
        return Created("", endereco);
    }
}
