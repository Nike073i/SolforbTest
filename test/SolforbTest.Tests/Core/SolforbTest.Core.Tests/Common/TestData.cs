using SolforbTest.Domain;
using System.Globalization;

namespace SolforbTest.Core.Tests.Common
{
    public static class TestData
    {
        private static DateTime CreateDateTime(string dateTime) =>
            DateTime.Parse(dateTime, CultureInfo.CreateSpecificCulture("en-US"));

        public static List<Order> OrderTestList =>
            new()
            {
                new Order("Number 1", CreateDateTime("09/11/2023"), 1)
                {
                    Id = 1,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem("Name 1, Order 1", 15.531m, "kg") { Id = 1001 },
                        new OrderItem("Name 2, Order 1", 16.531m, "vt") { Id = 1002 },
                        new OrderItem("Name 3, Order 1", 16.531m, "vt") { Id = 1003 },
                    }
                },
                new Order("Number 2", CreateDateTime("12/11/2023"), 2)
                {
                    Id = 2,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem("Name 4, Order 2", 0.531m, "cm") { Id = 1004 },
                        new OrderItem("Name 5, Order 2", 4.531m, "kg") { Id = 1005 },
                        new OrderItem("Name 6, Order 2", 140.531m, "kg") { Id = 1006 },
                        new OrderItem("Name 7, Order 2", 150.531m, "vt") { Id = 1007 }
                    }
                },
                new Order("Number 3", CreateDateTime("01/11/2023"), 2) { Id = 3 },
                new Order("Number 4", CreateDateTime("10/11/2023"), 5)
                {
                    Id = 4,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem("Name 8, Order 4", 21.531m, "cm") { Id = 1008 },
                        new OrderItem("Name 9, Order 4", 15.531m, "vt") { Id = 1009 },
                    }
                },
                new Order("Number 5", CreateDateTime("04/11/2023"), 1)
                {
                    Id = 5,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem("Name 10, Order 5", 10.531m, "m") { Id = 1010 },
                        new OrderItem("Name 11, Order 5", 11.531m, "vt") { Id = 1011 },
                    }
                },
            };
    }
}
