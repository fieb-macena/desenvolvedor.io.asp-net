using Microsoft.EntityFrameworkCore;

namespace EFC.Data;

public class ApplicationContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data source=(localdb)\\MSSQLLocalDB;Initial Catalog=EFC;Integrated Security=true");

        //base.OnConfiguring(optionsBuilder);
    }
}