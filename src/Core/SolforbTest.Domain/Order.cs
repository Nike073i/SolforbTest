namespace SolforbTest.Domain
{
    public class Order
    {
        private List<OrderItem>? _orderItems;

        public int Id { get; private set; }

        public string Number { get; set; }

        public DateTimeOffset Date { get; set; }

        public int ProviderId { get; set; }

        public Provider? Provider { get; set; }

        public IEnumerable<OrderItem> OrderItems => _orderItems ?? Enumerable.Empty<OrderItem>();

        public Order(string number, DateTimeOffset date, int providerId)
        {
            Number = number;
            Date = date;
            ProviderId = providerId;
        }
    }
}
