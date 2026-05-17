using BemEstarOnline.Api.Data;
using BemEstarOnline.Api.Dtos;
using BemEstarOnline.Api.Models;
using BemEstarOnline.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BemEstarOnline.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly BemEstarDbContext _db;
    private readonly CupomService _cupomService;
    public PedidosController(BemEstarDbContext db, CupomService cupomService) { _db = db; _cupomService = cupomService; }

    [HttpGet("usuario/{idUsuario:int}")]
    public async Task<IActionResult> PorUsuario(int idUsuario)
    {
        var pedidos = await _db.Pedidos.Where(p => p.IdUsuario == idUsuario).OrderByDescending(p => p.CriadoEm).Select(p => new PedidoResumoDto(p.IdPedido, p.Status, p.Total, p.CriadoEm)).ToListAsync();
        return Ok(pedidos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Obter(int id)
    {
        var pedido = await _db.Pedidos.Include(p => p.Itens).Include(p => p.Pagamentos).FirstOrDefaultAsync(p => p.IdPedido == id);
        return pedido is null ? NotFound() : Ok(pedido);
    }

    [HttpPost]
    public async Task<IActionResult> Criar(PedidoCriarDto dto)
    {
        var carrinho = await _db.Carrinhos.Include(c => c.Itens).ThenInclude(i => i.Medicamento).FirstOrDefaultAsync(c => c.IdUsuario == dto.IdUsuario);
        if (carrinho is null || !carrinho.Itens.Any()) return BadRequest(new { mensagem = "Carrinho vazio." });
        if (!await _db.Enderecos.AnyAsync(e => e.IdEndereco == dto.IdEndereco && e.IdUsuario == dto.IdUsuario)) return BadRequest(new { mensagem = "Endereço inválido." });

        var subtotal = carrinho.Itens.Sum(i => i.PrecoUnitario * i.Quantidade);
        decimal desconto = 0;
        decimal frete = dto.Frete;
        Cupom? cupom = null;
        if (!string.IsNullOrWhiteSpace(dto.CodigoCupom))
        {
            var resultado = await _cupomService.ValidarAsync(dto.CodigoCupom.ToUpper(), subtotal, frete);
            if (resultado.Valido)
            {
                desconto = resultado.Desconto;
                frete = resultado.FreteCalculado;
                cupom = await _db.Cupons.FirstAsync(c => c.Codigo == dto.CodigoCupom.ToUpper());
                cupom.UsosRealizados++;
            }
        }
        var total = Math.Max(0, subtotal - desconto + frete);
        var pedido = new Pedido { IdUsuario = dto.IdUsuario, IdEndereco = dto.IdEndereco, IdCupom = cupom?.IdCupom, Status = "AGUARDANDO_PAGAMENTO", Subtotal = subtotal, Desconto = desconto, Frete = frete, Total = total, Observacao = dto.Observacao };
        foreach (var item in carrinho.Itens)
        {
            pedido.Itens.Add(new PedidoItem { IdMedicamento = item.IdMedicamento, NomeProduto = item.Medicamento?.Nome ?? "Produto", Quantidade = item.Quantidade, PrecoUnitario = item.PrecoUnitario, Subtotal = item.PrecoUnitario * item.Quantidade });
            var estoque = await _db.Estoques.FirstOrDefaultAsync(e => e.IdMedicamento == item.IdMedicamento);
            if (estoque != null) estoque.Quantidade = Math.Max(0, estoque.Quantidade - item.Quantidade);
        }
        pedido.Pagamentos.Add(new Pagamento { Metodo = dto.MetodoPagamento, Status = "APROVADO", Valor = total, PagoEm = DateTime.UtcNow, CodigoTransacao = $"BEO-{DateTime.UtcNow:yyyyMMddHHmmss}" });
        pedido.Status = "PAGO";
        _db.Pedidos.Add(pedido);
        _db.CarrinhoItens.RemoveRange(carrinho.Itens);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Obter), new { id = pedido.IdPedido }, new { pedido.IdPedido, pedido.Total, pedido.Status });
    }
}
