using BemEstarOnline.Api.Data;
using BemEstarOnline.Api.Dtos;
using BemEstarOnline.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BemEstarOnline.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CuponsController : ControllerBase
{
    private readonly BemEstarDbContext _db;
    private readonly CupomService _service;
    public CuponsController(BemEstarDbContext db, CupomService service) { _db = db; _service = service; }

    [HttpGet]
    public async Task<IActionResult> Listar() => Ok(await _db.Cupons.Where(c => c.Ativo).ToListAsync());

    [HttpPost("validar")]
    public async Task<ActionResult<CupomResultadoDto>> Validar(CupomValidacaoDto dto) => Ok(await _service.ValidarAsync(dto.Codigo, dto.Subtotal, dto.Frete));
}
