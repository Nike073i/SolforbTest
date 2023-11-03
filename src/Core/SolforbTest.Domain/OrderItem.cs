namespace SolforbTest.Domain
{
    public class OrderItem
    {
        public int Id { get; private set; }

        public int OrderId { get; }

        public string Name { get; set; }

        public decimal Quantity { get; set; }

        public string Unit { get; set; }

        public OrderItem(int orderId, string name, decimal quantity, string unit)
        {
            OrderId = orderId;
            Name = name;
            Quantity = quantity;
            Unit = unit;
        }
    }
}
