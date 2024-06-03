using Xunit;
using Microsoft.AspNetCore.Mvc;
using API.Controllers;
using API.Models;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace TesteHistoria;

public class HistoriaControllerTest
{
    private BancoDeDadosContext _context;

    public HistoriaControllerTest()
    {
        var options = new DbContextOptionsBuilder<BancoDeDadosContext>().UseInMemoryDatabase(databaseName: "InMemoryDb")
            .Options;

        _context = new BancoDeDadosContext(options);
        _context.Historia.Add(new Historia { Id = 1, Titulo = "Hist�ria 1", Descricao = "Descri��o 1", Categoria = "Categoria 1" });
        _context.SaveChanges();
    }

    // Teste para verificar se o m�todo GetHistorias retorna um OkObjectResult com uma lista de hist�rias
    [Fact]
    public void GetHistorias_ReturnsOkResult_WithAListOfHistorias()
    {
        var controller = new HistoriaController(_context);

        var result = controller.GetHistorias();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnHistories = Assert.IsType<List<Historia>>(okResult.Value);
        Assert.Single(returnHistories);
    }

    // Teste para verificar se o m�todo GetHistoriasPorId retorna um OkObjectResult com uma hist�ria
    [Fact]
    public void GetHistoriasPorId_ReturnsOkResult_WithAHistoria()
    {
        var controller = new HistoriaController(_context);

        var result = controller.GetHistoriasPorId(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnHistoria = Assert.IsType<Historia>(okResult.Value);
        Assert.Equal("Hist�ria 1", returnHistoria.Titulo);
    }

    // Teste para verificar se o m�todo PostHistoria retorna um CreatedAtActionResult com a hist�ria criada
    [Fact]
    public void PostHistoria_ReturnsCreatedAtActionResult_WithCreatedHistoria()
    {
        var controller = new HistoriaController(_context);

        var result = controller.PostHistoria(new Historia { Id = 2, Titulo = "Hist�ria 2", Descricao = "Descri��o 2", Categoria = "Categoria 2" });

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnHistoria = Assert.IsType<Historia>(createdAtActionResult.Value);
        Assert.Equal("Hist�ria 2", returnHistoria.Titulo);
    }

}
