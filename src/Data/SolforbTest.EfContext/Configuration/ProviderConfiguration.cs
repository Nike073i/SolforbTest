using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolforbTest.Domain;

namespace SolforbTest.EfContext.Configuration
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        private readonly List<Provider> InitialData = new List<Provider>
        {
            new("Провайдер 1") { Id = 1 },
            new("Провайдер 2") { Id = 2 },
            new("Провайдер 3") { Id = 3 },
            new("Провайдер 4") { Id = 4 },
            new("Провайдер 5") { Id = 5 }
        };

        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasData(InitialData);
        }
    }
}
