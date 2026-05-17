using BemEstarOnline.Api.Data;
using BemEstarOnline.Api.Dtos;
using BemEstarOnline.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BemEstarOnline.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly BemEstarDbContext _db;
    public ProdutosController(BemEstarDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> Listar([FromQuery] string? busca, [FromQuery] string? categoria)
    {
        var query = _db.Medicamentos.Include(m => m.Categoria).Include(m => m.Estoque).Where(m => m.Ativo).AsQueryable();
        if (!string.IsNullOrWhiteSpace(busca))
        {
            var termo = busca.ToLower();
            query = query.Where(m => m.Nome.ToLower().Contains(termo) || m.Descricao.ToLower().Contains(termo) || m.Fabricante.ToLower().Contains(termo));
        }
        if (!string.IsNullOrWhiteSpace(categoria))
            query = query.Where(m => m.Categoria != null && m.Categoria.Slug == categoria);

        var produtos = await query.OrderBy(m => m.Nome).Select(m => new ProdutoDto(
            m.IdMedicamento, m.Nome, m.Slug, m.Categoria!.Nome, m.Categoria.Slug, m.Tipo, m.Descricao,
            m.Dosagem, m.Fabricante, m.Preco, m.ExigeReceita, m.Estoque != null ? m.Estoque.Quantidade : 0)).ToListAsync();
        return Ok(produtos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProdutoDto>> Obter(int id)
    {
        var m = await _db.Medicamentos.Include(x => x.Categoria).Include(x => x.Estoque).FirstOrDefaultAsync(x => x.IdMedicamento == id && x.Ativo);
        if (m is null) return NotFound(new { mensagem = "Produto não encontrado." });
        return Ok(new ProdutoDto(m.IdMedicamento, m.Nome, m.Slug, m.Categoria!.Nome, m.Categoria.Slug, m.Tipo, m.Descricao, m.Dosagem, m.Fabricante, m.Preco, m.ExigeReceita, m.Estoque?.Quantidade ?? 0));
    }

    [HttpPost]
    public async Task<ActionResult> Criar(ProdutoCriarDto dto)
    {
        if (!await _db.Categorias.AnyAsync(c => c.IdCategoria == dto.IdCategoria)) return BadRequest(new { mensagem = "Categoria inválida." });
        var med = new Medicamento { IdCategoria = dto.IdCategoria, Nome = dto.Nome, Slug = dto.Slug, Tipo = dto.Tipo, Descricao = dto.Descricao, Dosagem = dto.Dosagem, Fabricante = dto.Fabricante, ViaAdministracao = dto.ViaAdministracao, RegistroAnvisa = dto.RegistroAnvisa, ValidadeLote = dto.ValidadeLote, Preco = dto.Preco, ExigeReceita = dto.ExigeReceita };
        _db.Medicamentos.Add(med);
        await _db.SaveChangesAsync();
        _db.Estoques.Add(new Estoque { IdMedicamento = med.IdMedicamento, Quantidade = Math.Max(0, dto.EstoqueInicial), QuantidadeMinima = 5 });
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Obter), new { id = med.IdMedicamento }, new { med.IdMedicamento });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, ProdutoCriarDto dto)
    {
        var med = await _db.Medicamentos.Include(m => m.Estoque).FirstOrDefaultAsync(m => m.IdMedicamento == id);
        if (med is null) return NotFound();
        med.Nome = dto.Nome; med.Slug = dto.Slug; med.IdCategoria = dto.IdCategoria; med.Tipo = dto.Tipo; med.Descricao = dto.Descricao; med.Dosagem = dto.Dosagem; med.Fabricante = dto.Fabricante; med.ViaAdministracao = dto.ViaAdministracao; med.RegistroAnvisa = dto.RegistroAnvisa; med.ValidadeLote = dto.ValidadeLote; med.Preco = dto.Preco; med.ExigeReceita = dto.ExigeReceita; med.AtualizadoEm = DateTime.UtcNow;
        if (med.Estoque is null) _db.Estoques.Add(new Estoque { IdMedicamento = id, Quantidade = dto.EstoqueInicial }); else med.Estoque.Quantidade = Math.Max(0, dto.EstoqueInicial);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remover(int id)
    {
        var med = await _db.Medicamentos.FindAsync(id);
        if (med is null) return NotFound();
        med.Ativo = false;
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
