using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SolforbTest.Application.Interfaces;
using SolforbTest.EfContext.Context;

namespace SolforbTest.EfContext.SqlServer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSqlServerDb(
            this IServiceCollection services,
            string dbConnectionString
        )
        {
            services.AddDbContext<SolforbDbContext>(
                opt =>
                    opt.UseSqlServer(
                        dbConnectionString,
                        x => x.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
                    )
            );
            services.AddScoped<ISolforbDbContext, SolforbDbContext>(
                provider => provider.GetRequiredService<SolforbDbContext>()
            );
            return services;
        }
    }
}
