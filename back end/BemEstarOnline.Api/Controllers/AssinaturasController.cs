using BemEstarOnline.Api.Data;
using BemEstarOnline.Api.Dtos;
using BemEstarOnline.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BemEstarOnline.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssinaturasController : ControllerBase
{
    private readonly BemEstarDbContext _db;
    public AssinaturasController(BemEstarDbContext db) => _db = db;

    [HttpGet("usuario/{idUsuario:int}")]
    public async Task<IActionResult> PorUsuario(int idUsuario) => Ok(await _db.Assinaturas.Where(a => a.IdUsuario == idUsuario).ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Criar(AssinaturaCriarDto dto)
    {
        if (!await _db.Usuarios.AnyAsync(u => u.IdUsuario == dto.IdUsuario)) return BadRequest(new { mensagem = "Usuário inválido." });
        var assinatura = new Assinatura { IdUsuario = dto.IdUsuario, Plano = dto.Plano, Periodicidade = dto.Periodicidade, Valor = dto.Valor, ProximaCobranca = DateTime.UtcNow.AddMonths(1) };
        _db.Assinaturas.Add(assinatura);
        await _db.SaveChangesAsync();
        return Created("", assinatura);
    }
}
