using BemEstarOnline.Api.Data;
using BemEstarOnline.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BemEstarOnline.Api.Services;

public class CupomService
{
    private readonly BemEstarDbContext _db;
    public CupomService(BemEstarDbContext db) => _db = db;

    public async Task<CupomResultadoDto> ValidarAsync(string codigo, decimal subtotal, decimal frete)
    {
        var cupom = await _db.Cupons.FirstOrDefaultAsync(c => c.Codigo == codigo.ToUpper());
        if (cupom is null || !cupom.Ativo)
            return new(false, "Cupom inválido ou inativo.", 0, frete, subtotal + frete);
        if (cupom.DataFim.HasValue && cupom.DataFim.Value < DateTime.UtcNow)
            return new(false, "Cupom expirado.", 0, frete, subtotal + frete);
        if (subtotal < cupom.ValorMinimoPedido)
            return new(false, $"Pedido mínimo de R$ {cupom.ValorMinimoPedido:N2} não atingido.", 0, frete, subtotal + frete);
        if (cupom.LimiteUso.HasValue && cupom.UsosRealizados >= cupom.LimiteUso.Value)
            return new(false, "Limite de uso do cupom atingido.", 0, frete, subtotal + frete);

        decimal desconto = 0;
        decimal freteCalculado = frete;
        if (cupom.Tipo == "PERCENTUAL") desconto = subtotal * (cupom.Valor / 100m);
        if (cupom.Tipo == "VALOR_FIXO") desconto = Math.Min(cupom.Valor, subtotal);
        if (cupom.Tipo == "FRETE_GRATIS") freteCalculado = 0;
        var total = Math.Max(0, subtotal - desconto + freteCalculado);
        return new(true, cupom.Descricao, desconto, freteCalculado, total);
    }
}
