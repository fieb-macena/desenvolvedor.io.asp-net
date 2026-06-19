using EFC.Data.Configuration;
using EFC.Domain;
using EFC.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFC.Data;

public class ApplicationContext : DbContext
{
    private static readonly ILoggerFactory _logger = LoggerFactory.Create(p=>p.AddConsole());
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //base.OnConfiguring(optionsBuilder);
        optionsBuilder
            .UseLoggerFactory(_logger)
            .EnableSensitiveDataLogging()
            .UseSqlServer("Data source=(localdb)\\MSSQLLocalDB;Initial Catalog=EFC;Integrated Security=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new ClienteConfiguration());

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
    
    
}