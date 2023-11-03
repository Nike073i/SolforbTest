using System;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Domain;

namespace SolforbTest.Application.Interfaces
{
    public interface ISolforbDbContext
    {
        DbSet<Order> Orders { get; }
        DbSet<Provider> Providers { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
