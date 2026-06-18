using EFC.Data.Configuration;
using EFC.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFC.Data;

public class ApplicationContext : DbContext
{
    //public DbSet<Pedido> Pedidos {get;set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data source=(localdb)\\MSSQLLocalDB;Initial Catalog=EFC;Integrated Security=true");

        //base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new ClienteConfiguration());

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
}