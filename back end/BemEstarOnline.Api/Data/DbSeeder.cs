using System.Security.Cryptography;
using System.Text;
using BemEstarOnline.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BemEstarOnline.Api.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(BemEstarDbContext db)
    {
        await db.Database.EnsureCreatedAsync();

        if (!await db.Categorias.AnyAsync())
        {
            db.Categorias.AddRange(
                new Categoria { Nome = "Genéricos", Slug = "genericos", Descricao = "Medicamentos genéricos aprovados." },
                new Categoria { Nome = "Referência", Slug = "referencia", Descricao = "Medicamentos de referência." },
                new Categoria { Nome = "Higiene", Slug = "higiene", Descricao = "Produtos de higiene pessoal." },
                new Categoria { Nome = "Dermocosméticos", Slug = "dermocosmeticos", Descricao = "Produtos para pele e cuidado diário." },
                new Categoria { Nome = "Primeiros Socorros", Slug = "primeiros-socorros", Descricao = "Itens para cuidados emergenciais." }
            );
            await db.SaveChangesAsync();
        }

        if (!await db.Medicamentos.AnyAsync())
        {
            var categorias = await db.Categorias.ToDictionaryAsync(c => c.Slug, c => c.IdCategoria);
            var produtos = new[]
            {
                new Medicamento { IdCategoria = categorias["genericos"], Nome = "Losartana Potássica 50mg", Slug = "losartana-potassica-50mg", Tipo = "Genérico", Descricao = "Anti-hipertensivo utilizado no tratamento da hipertensão arterial.", Dosagem = "50mg", Fabricante = "EMS Sigma Pharma", ViaAdministracao = "Oral", RegistroAnvisa = "1234567890", ValidadeLote = new DateTime(2027,12,31), Preco = 12.90m, ExigeReceita = true },
                new Medicamento { IdCategoria = categorias["dermocosmeticos"], Nome = "Protetor Solar FPS 50", Slug = "protetor-solar-fps-50", Tipo = "Dermocosmético", Descricao = "Protetor solar de amplo espectro com proteção UVA e UVB.", Dosagem = "200ml", Fabricante = "Bem Care", ViaAdministracao = "Tópica", ValidadeLote = new DateTime(2027,8,31), Preco = 54.90m },
                new Medicamento { IdCategoria = categorias["genericos"], Nome = "Dipirona Sódica 500mg", Slug = "dipirona-sodica-500mg", Tipo = "Genérico", Descricao = "Analgésico e antitérmico indicado para dores leves a moderadas e febre.", Dosagem = "500mg", Fabricante = "Neo Química", ViaAdministracao = "Oral", RegistroAnvisa = "9876543210", ValidadeLote = new DateTime(2027,10,30), Preco = 4.50m },
                new Medicamento { IdCategoria = categorias["higiene"], Nome = "Sabonete Líquido Neutro", Slug = "sabonete-liquido-neutro", Tipo = "Higiene Pessoal", Descricao = "Sabonete líquido de pH neutro para peles sensíveis.", Dosagem = "250ml", Fabricante = "Bem Care", ViaAdministracao = "Tópica", ValidadeLote = new DateTime(2028,1,31), Preco = 18.20m },
                new Medicamento { IdCategoria = categorias["primeiros-socorros"], Nome = "Kit Curativos Variados", Slug = "kit-curativos-variados", Tipo = "Primeiros Socorros", Descricao = "Kit com curativos de diferentes tamanhos e formatos.", Dosagem = "40 unidades", Fabricante = "Health First", ViaAdministracao = "Tópica", ValidadeLote = new DateTime(2028,6,30), Preco = 9.90m },
                new Medicamento { IdCategoria = categorias["genericos"], Nome = "Vitamina C 1g", Slug = "vitamina-c-1g", Tipo = "Suplemento", Descricao = "Suplemento de ácido ascórbico em pastilhas efervescentes.", Dosagem = "1g", Fabricante = "VitaBem", ViaAdministracao = "Oral", ValidadeLote = new DateTime(2027,11,30), Preco = 15.00m },
                new Medicamento { IdCategoria = categorias["referencia"], Nome = "Ibuprofeno 400mg", Slug = "ibuprofeno-400mg", Tipo = "Referência", Descricao = "Anti-inflamatório não esteroidal com ação analgésica e antitérmica.", Dosagem = "400mg", Fabricante = "Sanofi", ViaAdministracao = "Oral", RegistroAnvisa = "1122334455", ValidadeLote = new DateTime(2027,9,30), Preco = 8.90m },
                new Medicamento { IdCategoria = categorias["dermocosmeticos"], Nome = "Hidratante Corporal", Slug = "hidratante-corporal", Tipo = "Dermocosmético", Descricao = "Creme hidratante corporal com manteiga de karité e vitamina E.", Dosagem = "400ml", Fabricante = "Bem Care", ViaAdministracao = "Tópica", ValidadeLote = new DateTime(2028,4,30), Preco = 32.50m }
            };
            db.Medicamentos.AddRange(produtos);
            await db.SaveChangesAsync();
            db.Estoques.AddRange(produtos.Select(p => new Estoque { IdMedicamento = p.IdMedicamento, Quantidade = 100, QuantidadeMinima = 10, Localizacao = "Prateleira A" }));
            await db.SaveChangesAsync();
        }

        if (!await db.Usuarios.AnyAsync())
        {
            db.Usuarios.AddRange(
                new Usuario { Nome = "Administrador Bem Estar", DataNascimento = new DateTime(1990,1,1), Email = "admin@bemestaronline.com", Cpf = "000.000.000-00", Telefone = "(11) 90000-0000", SenhaHash = Hash("Admin@123"), Perfil = "ADMIN" },
                new Usuario { Nome = "Cliente Demonstração", DataNascimento = new DateTime(1998,5,20), Email = "cliente@bemestaronline.com", Cpf = "111.111.111-11", Telefone = "(11) 98888-7777", SenhaHash = Hash("Cliente@123"), Perfil = "CLIENTE" }
            );
            await db.SaveChangesAsync();
            db.Enderecos.Add(new Endereco { IdUsuario = 2, Apelido = "Casa", Cep = "01001-000", Logradouro = "Praça da Sé", Numero = "100", Complemento = "Apto 10", Bairro = "Sé", Cidade = "São Paulo", Estado = "SP", Principal = true });
            await db.SaveChangesAsync();
        }

        if (!await db.Cupons.AnyAsync())
        {
            db.Cupons.AddRange(
                new Cupom { Codigo = "BEMSTAR10", Descricao = "10% de desconto", Tipo = "PERCENTUAL", Valor = 10, ValorMinimoPedido = 20, DataFim = new DateTime(2027,12,31,23,59,59) },
                new Cupom { Codigo = "SAUDE15", Descricao = "15% de desconto", Tipo = "PERCENTUAL", Valor = 15, ValorMinimoPedido = 50, DataFim = new DateTime(2027,12,31,23,59,59) },
                new Cupom { Codigo = "FRETEGRATIS", Descricao = "Frete grátis", Tipo = "FRETE_GRATIS", Valor = 0, ValorMinimoPedido = 40, DataFim = new DateTime(2027,12,31,23,59,59) },
                new Cupom { Codigo = "PRIMEIRACOMPRA", Descricao = "R$ 5,00 de desconto", Tipo = "VALOR_FIXO", Valor = 5, ValorMinimoPedido = 15, DataFim = new DateTime(2027,12,31,23,59,59) }
            );
            await db.SaveChangesAsync();
        }
    }

    public static string Hash(string input)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
}
