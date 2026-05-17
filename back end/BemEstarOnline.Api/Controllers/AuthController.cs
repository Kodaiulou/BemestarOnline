using BemEstarOnline.Api.Data;
using BemEstarOnline.Api.Dtos;
using BemEstarOnline.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BemEstarOnline.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly BemEstarDbContext _db;
    public AuthController(BemEstarDbContext db) => _db = db;

    [HttpPost("login")]
    public async Task<ActionResult<UsuarioResumoDto>> Login(LoginDto dto)
    {
        var hash = DbSeeder.Hash(dto.Senha);
        var user = await _db.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email && u.SenhaHash == hash && u.Ativo);
        if (user is null) return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });
        return Ok(new UsuarioResumoDto(user.IdUsuario, user.Nome, user.Email, user.Perfil));
    }

    [HttpPost("registrar")]
    public async Task<ActionResult<UsuarioResumoDto>> Registrar(RegistroDto dto)
    {
        if (await _db.Usuarios.AnyAsync(u => u.Email == dto.Email)) return Conflict(new { mensagem = "E-mail já cadastrado." });
        if (await _db.Usuarios.AnyAsync(u => u.Cpf == dto.Cpf)) return Conflict(new { mensagem = "CPF já cadastrado." });
        var user = new Usuario { Nome = dto.Nome, DataNascimento = dto.DataNascimento, Email = dto.Email, Cpf = dto.Cpf, Telefone = dto.Telefone, SenhaHash = DbSeeder.Hash(dto.Senha), Perfil = "CLIENTE" };
        _db.Usuarios.Add(user);
        await _db.SaveChangesAsync();
        _db.Carrinhos.Add(new Carrinho { IdUsuario = user.IdUsuario });
        await _db.SaveChangesAsync();
        return Created("", new UsuarioResumoDto(user.IdUsuario, user.Nome, user.Email, user.Perfil));
    }
}
