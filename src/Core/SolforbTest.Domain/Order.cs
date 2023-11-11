namespace SolforbTest.Domain
{
    public class Order : IEntity
    {
        public ICollection<OrderItem>? OrderItems { get; init; }

        public int Id { get; init; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public int ProviderId { get; set; }

        public Provider? Provider { get; set; }

        public Order(string number, DateTime date, int providerId)
        {
            Number = number;
            Date = date;
            ProviderId = providerId;
        }
    }
}
