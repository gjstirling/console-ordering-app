using ConsoleOrderingApp.Data;
using ConsoleOrderingApp.Services;

namespace ConsoleOrderingApp.Tests;

public class ProductServiceTests
{
    [Fact]
    public void GetById_ValidId_ReturnsProduct()
    {
        var repo = new InMemoryProductRepository();
        repo.Add(new Product { Id = 1, Name = "Keyboard", Price = 50m, Stock = 10 });

        var service = new ProductService(repo);

        var product = service.GetById(1);

        Assert.NotNull(product);
        Assert.Equal("Keyboard", product!.Name);
    }

    [Fact]
    public void GetById_InvalidId_ReturnsNull()
    {
        var repo = new InMemoryProductRepository();
        var service = new ProductService(repo);

        var product = service.GetById(999);

        Assert.Null(product);
    }

    [Fact]
    public void GetAvailableProducts_ReturnsOnlyInStock()
    {
        var repo = new InMemoryProductRepository();
        repo.Add(new Product { Id = 1, Name = "Keyboard", Price = 50m, Stock = 10 });
        repo.Add(new Product { Id = 2, Name = "Mouse", Price = 20m, Stock = 0 });
        repo.Add(new Product { Id = 3, Name = "Monitor", Price = 120m, Stock = 5 });

        var service = new ProductService(repo);

        var available = service.GetAvailableProducts();

        Assert.DoesNotContain(available, p => p.Stock <= 0);
        Assert.Contains(available, p => p.Id == 1);
        Assert.Contains(available, p => p.Id == 3);
    }

    [Fact]
    public void GetAvailableProducts_DoesNotReturnNegativeStock()
    {
        var repo = new InMemoryProductRepository();
        repo.Add(new Product { Id = 1, Name = "Keyboard", Price = 50m, Stock = 10 });
        repo.Add(new Product { Id = 99, Name = "Faulty", Price = 10m, Stock = -5 });

        var service = new ProductService(repo);

        var available = service.GetAvailableProducts();

        Assert.DoesNotContain(available, p => p.Stock < 0);
        Assert.Contains(available, p => p.Id == 1);
    }
}
