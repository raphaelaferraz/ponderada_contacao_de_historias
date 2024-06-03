using API.Controllers;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteUsuario;

public class TestesUsuario
{
    private BancoDeDadosContext _context;

    public TestesUsuario()
    {
        var options = new DbContextOptionsBuilder<BancoDeDadosContext>().UseInMemoryDatabase(databaseName: "InMemoryDb")
            .Options;

        _context = new BancoDeDadosContext(options);
        _context.Usuario.Add(new Usuario { Id = 1, Nome = "Usuário 1", Email = "usuario@email.com", Senha = "123" });
    }

    // Teste para verificar se o método GetUsuariosPorId retorna um OkObjectResult com um usuário
    [Fact]
    public void GetUsuariosPorId_ReturnsOkResult_WithAUsuario()
    {
        var controller = new UsuarioController(_context);

        var result = controller.GetUsuariosPorId(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnUsuario = Assert.IsType<Usuario>(okResult.Value);
        Assert.Equal("Usuário 1", returnUsuario.Nome);
    }
}
