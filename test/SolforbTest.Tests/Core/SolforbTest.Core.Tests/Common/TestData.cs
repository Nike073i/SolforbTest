using SolforbTest.Domain;
using System.Globalization;

namespace SolforbTest.Core.Tests.Common
{
    public static class TestData
    {
        public static int TestOrderItemId => 1001;
        public static string TestOrderItemName => "Name 1";
        public static string TestOrderItemName2 => "Name 2";
        public static decimal TestOrderItemQuantity => 15.531m;
        public static string TestOrderItemUnit => "kg";

        public static int TestOrderId => 1;
        public static string TestOrderNumber => "Number 1";
        public static DateTime TestOrderDate => CreateDateTime("11/15/2023");
        public static int TestOrderProviderId => 1;

        public static DateTime CreateDateTime(string dateTime) =>
            DateTime.Parse(dateTime, CultureInfo.CreateSpecificCulture("en-US"));

        public static List<Order> OrderTestList =>
            new()
            {
                new Order(TestOrderNumber, TestOrderDate, TestOrderProviderId)
                {
                    Id = TestOrderId,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem(TestOrderItemName, TestOrderItemQuantity, TestOrderItemUnit)
                        {
                            Id = TestOrderItemId
                        },
                        new OrderItem(TestOrderItemName2, 16.531m, "vt") { Id = 1002 },
                        new OrderItem("Name 3", 16.531m, "vt") { Id = 1003 },
                    }
                },
                new Order("Number 1", CreateDateTime("11/09/2023"), 2)
                {
                    Id = 2,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem("Name 1", 0.531m, "cm") { Id = 1004 },
                        new OrderItem("Name 2", 4.531m, "kg") { Id = 1005 },
                        new OrderItem("Name 3", 140.531m, "kg") { Id = 1006 },
                        new OrderItem("Name 4", 150.531m, "vt") { Id = 1007 }
                    }
                },
                new Order("Number 3", CreateDateTime("11/12/2023"), 2) { Id = 3 },
                new Order("Number 4", CreateDateTime("11/04/2023"), 5)
                {
                    Id = 4,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem("Name 1", 21.531m, "cm") { Id = 1008 },
                        new OrderItem("Name 2", 15.531m, "vt") { Id = 1009 },
                    }
                },
                new Order("Number 5", CreateDateTime("11/01/2023"), 1)
                {
                    Id = 5,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem("Name 1", 10.531m, "m") { Id = 1010 },
                        new OrderItem("Name 2", 11.531m, "vt") { Id = 1011 },
                    }
                },
            };
    }
}
