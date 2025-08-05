using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using freelanceProjectEgypt03.data.freelanceProjectEgypt03.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=Egypt;User Id=sa;Password=123456;TrustServerCertificate=True;");

        return new AppDbContext(optionsBuilder.Options);
    }
}
