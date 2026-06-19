using EFC.Data.Configuration;
using EFC.Domain;
using EFC.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            .UseSqlServer("Data source=(localdb)\\MSSQLLocalDB;Initial Catalog=EFC;Integrated Security=true",
                p=>p.EnableRetryOnFailure(
                    maxRetryCount: 2, 
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null
                ).MigrationsHistoryTable("EFC"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new ClienteConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
    
    private void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
    {
        foreach(var entity in modelBuilder.Model.GetEntityTypes()){
            var properties = entity.GetProperties().Where(p=>p.ClrType == typeof(string));

            foreach(var property in properties)
            {
                if(string.IsNullOrEmpty(property.GetColumnType()) && !property.GetMaxLength().HasValue)
                    property.SetColumnType("VARCHAR(100)");
            }
        }
    }
}