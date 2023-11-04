using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SolforbTest.EfContext.Context;

namespace SolforbTest.MigrationsTool
{
    public class DesignDbContext : IDesignTimeDbContextFactory<SolforbDbContext>
    {
        public SolforbDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();

            string connectionString =
                config["DatabaseConnectionString"]
                ?? throw new InvalidOperationException("Database connection string not set");
            string migrationAssembly =
                config["MigrationsAssembly"]
                ?? throw new InvalidOperationException("Assembly with migrations not set");

            var dbOptionsBuilder = new DbContextOptionsBuilder<SolforbDbContext>();
            dbOptionsBuilder.UseSqlServer(
                connectionString,
                x => x.MigrationsAssembly(migrationAssembly)
            );
            return new SolforbDbContext(dbOptionsBuilder.Options);
        }
    }
}
