using Microsoft.EntityFrameworkCore;
using SolforbTest.Domain;

namespace SolforbTest.Application.Interfaces
{
    public interface ISolforbDbContext
    {
        DbSet<Order> Orders { get; }
        DbSet<Provider> Providers { get; }
        DbSet<OrderItem> OrderItems { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
