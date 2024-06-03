using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class BancoDeDadosContext : DbContext 
{
    public BancoDeDadosContext(DbContextOptions<BancoDeDadosContext> options) : base(options)
    {
    }

    public DbSet<Historia> Historia { get; set; }
}
