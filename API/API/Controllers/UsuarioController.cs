using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private BancoDeDadosContext _context;

    public UsuarioController(BancoDeDadosContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetUsuarios()
    {
        return Ok(_context.Usuario.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetUsuariosPorId(int id)
    {
        return Ok(_context.Usuario.Find(id));
    }

    [HttpPost]
    public IActionResult PostUsuario(Usuario usuario)
    {
        _context.Usuario.Add(usuario);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetUsuariosPorId), new { id = usuario.Id }, usuario);
    }

    // Post de login 
    [HttpPost("login")]
    public IActionResult PostLogin(Usuario usuario)
    {
        var usuarioLogin = _context.Usuario.FirstOrDefault(u => u.Email == usuario.Email && u.Senha == usuario.Senha);

        if (usuarioLogin == null)
        {
            return NotFound();
        }

        return Ok(usuarioLogin);
    }

    [HttpPut("{id}")]
    public IActionResult PutUsuario(int id, Usuario usuario)
    {
        if (id != usuario.Id)
        {
            return BadRequest();
        }

        _context.Entry(usuario).State = EntityState.Modified;
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUsuario(int id)
    {
        var usuario = _context.Usuario.Find(id);

        if (usuario == null)
        {
            return NotFound();
        }

        _context.Usuario.Remove(usuario);
        _context.SaveChanges();
        return NoContent();
    }   
}
