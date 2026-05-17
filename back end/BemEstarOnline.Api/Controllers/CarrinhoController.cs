using BemEstarOnline.Api.Data;
using BemEstarOnline.Api.Dtos;
using BemEstarOnline.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BemEstarOnline.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarrinhoController : ControllerBase
{
    private readonly BemEstarDbContext _db;
    public CarrinhoController(BemEstarDbContext db) => _db = db;

    [HttpGet("usuario/{idUsuario:int}")]
    public async Task<ActionResult<CarrinhoDto>> Obter(int idUsuario)
    {
        var carrinho = await ObterOuCriarCarrinho(idUsuario);
        await _db.Entry(carrinho).Collection(c => c.Itens).Query().Include(i => i.Medicamento).LoadAsync();
        return Ok(ToDto(carrinho));
    }

    [HttpPost("item")]
    public async Task<ActionResult<CarrinhoDto>> Adicionar(CarrinhoAdicionarDto dto)
    {
        if (dto.Quantidade <= 0) return BadRequest(new { mensagem = "Quantidade deve ser maior que zero." });
        var produto = await _db.Medicamentos.Include(m => m.Estoque).FirstOrDefaultAsync(m => m.IdMedicamento == dto.IdMedicamento && m.Ativo);
        if (produto is null) return NotFound(new { mensagem = "Produto não encontrado." });
        if (produto.Estoque != null && produto.Estoque.Quantidade < dto.Quantidade) return BadRequest(new { mensagem = "Estoque insuficiente." });
        var carrinho = await ObterOuCriarCarrinho(dto.IdUsuario);
        await _db.Entry(carrinho).Collection(c => c.Itens).LoadAsync();
        var item = carrinho.Itens.FirstOrDefault(i => i.IdMedicamento == dto.IdMedicamento);
        if (item is null) carrinho.Itens.Add(new CarrinhoItem { IdMedicamento = dto.IdMedicamento, Quantidade = dto.Quantidade, PrecoUnitario = produto.Preco });
        else item.Quantidade += dto.Quantidade;
        carrinho.AtualizadoEm = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return await Obter(dto.IdUsuario);
    }

    [HttpDelete("usuario/{idUsuario:int}/item/{idMedicamento:int}")]
    public async Task<IActionResult> RemoverItem(int idUsuario, int idMedicamento)
    {
        var carrinho = await _db.Carrinhos.Include(c => c.Itens).FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);
        if (carrinho is null) return NotFound();
        var item = carrinho.Itens.FirstOrDefault(i => i.IdMedicamento == idMedicamento);
        if (item is null) return NotFound();
        _db.CarrinhoItens.Remove(item);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("usuario/{idUsuario:int}")]
    public async Task<IActionResult> Limpar(int idUsuario)
    {
        var carrinho = await _db.Carrinhos.Include(c => c.Itens).FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);
        if (carrinho is null) return NoContent();
        _db.CarrinhoItens.RemoveRange(carrinho.Itens);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    private async Task<Carrinho> ObterOuCriarCarrinho(int idUsuario)
    {
        var carrinho = await _db.Carrinhos.FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);
        if (carrinho != null) return carrinho;
        carrinho = new Carrinho { IdUsuario = idUsuario };
        _db.Carrinhos.Add(carrinho);
        await _db.SaveChangesAsync();
        return carrinho;
    }

    private static CarrinhoDto ToDto(Carrinho carrinho)
    {
        var itens = carrinho.Itens.Select(i => new CarrinhoItemDto(i.IdMedicamento, i.Medicamento?.Nome ?? "Produto", i.PrecoUnitario, i.Quantidade, i.PrecoUnitario * i.Quantidade)).ToList();
        return new CarrinhoDto(carrinho.IdCarrinho, carrinho.IdUsuario, itens, itens.Sum(i => i.Subtotal));
    }
}
