using ConsoleOrderingApp.Services;
using ConsoleOrderingApp.Models;

namespace ConsoleOrderingApp.Tests.NewFolder
{
    public class OrderServiceTests
    {
        [Fact]
        public void CreateOrder_ValidOrder_CalculatesTotalAndReducesStock()
        {
            var user = new User { Username = "alice", Email = "alice@test.com" };
            var product = new Product { Name = "Keyboard", Price = 50.0m, Stock = 10 };
            var service = new OrderService();

            var order = service.CreateOrder(user, product, 2);

            Assert.Equal(100m, order.Total);
            Assert.Equal(8, product.Stock);
        }

        [Fact]
        public void CreateOrder_ExceedStock_ThrowsException()
        {
            var user = new User { Username = "alice", Email = "alice@test.com" };
            var product = new Product { Name = "Monitor", Price = 120m, Stock = 1 };
            var service = new OrderService();

            Assert.Throws<InvalidOperationException>(() =>
                service.CreateOrder(user, product, 2));
        }
    }
}
