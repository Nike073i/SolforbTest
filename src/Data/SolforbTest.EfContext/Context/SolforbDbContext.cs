using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Interfaces;
using SolforbTest.Domain;

namespace SolforbTest.EfContext.Context
{
    public class SolforbDbContext : DbContext, ISolforbDbContext
    {
        public SolforbDbContext(
            DbContextOptions<SolforbDbContext> dbContextOptions,
            bool migrate = true
        )
            : base(dbContextOptions)
        {
            if (migrate)
                Database.Migrate();
        }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Provider> Providers => Set<Provider>();

        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
