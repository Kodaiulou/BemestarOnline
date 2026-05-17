using BemEstarOnline.Api.Data;
using BemEstarOnline.Api.Dtos;
using BemEstarOnline.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BemEstarOnline.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeliveryController : ControllerBase
{
    private readonly BemEstarDbContext _db;
    public DeliveryController(BemEstarDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Listar() => Ok(await _db.DeliverySolicitacoes.OrderByDescending(d => d.CriadoEm).ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Criar(DeliveryCriarDto dto)
    {
        var delivery = new DeliverySolicitacao { IdUsuario = dto.IdUsuario, NomeCliente = dto.NomeCliente, Telefone = dto.Telefone, EnderecoTexto = dto.EnderecoTexto, Observacao = dto.Observacao, ReceitaArquivo = dto.ReceitaArquivo };
        _db.DeliverySolicitacoes.Add(delivery);
        await _db.SaveChangesAsync();
        return Created("", delivery);
    }
}
