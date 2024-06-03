using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class HistoriaController : ControllerBase
{
    private BancoDeDadosContext _context;

    public HistoriaController(BancoDeDadosContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetHistorias()
    {
        return Ok(_context.Historia.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetHistoriasPorId(int id)
    {
        return Ok(_context.Historia.Find(id));
    }

    [HttpPost]
    public IActionResult PostHistoria(Historia historia)
    {
        _context.Historia.Add(historia);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetHistoriasPorId), new { id = historia.Id }, historia);
    }

    [HttpPut("{id}")]
    public IActionResult PutHistoria(int id, Historia historia)
    {
        if (id != historia.Id)
        {
            return BadRequest();
        }

        _context.Entry(historia).State = EntityState.Modified;
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteHistoria(int id)
    {
        var historia = _context.Historia.Find(id);

        if (historia == null)
        {
            return NotFound();
        }

        _context.Historia.Remove(historia);
        _context.SaveChanges();
        return NoContent();
    }
}
