using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolforbTest.Domain;

namespace SolforbTest.EfContext.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        private const string OrderItemsFieldName = "_orderItems";

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.OrderItems).HasField(OrderItemsFieldName);
        }
    }
}
