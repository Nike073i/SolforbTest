using Microsoft.EntityFrameworkCore;
using SolforbTest.EfContext.Context;

namespace SolforbTest.Core.Tests.Common
{
    public class SolforbDbContextFactory
    {
        public static SolforbDbContext Create()
        {
            var options = new DbContextOptionsBuilder<SolforbDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new SolforbDbContext(options, false);
            context.Database.EnsureCreated();
            context.AddRange(TestData.OrderTestList);
            context.SaveChanges();
            return context;
        }

        public static void Destroy(SolforbDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
