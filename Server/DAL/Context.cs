using Microsoft.EntityFrameworkCore;

public class Context : DbContext
{
    public DbSet<Prioridades> Prioridades { get; set; }
    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Sistemas> Sistemas { get; set; }
    public DbSet<Tickets> Tickets { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    {
        
    }
}