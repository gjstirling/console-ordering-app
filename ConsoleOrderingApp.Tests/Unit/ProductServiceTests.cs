using ConsoleOrderingApp.Services;

public class ProductServiceTests
{
    private ProductService CreateServiceWithSeedData(bool useEf = false)
    {
        var repo = TestHelpers.CreateRepository(useEf);

        repo.Add(new Product { Id = 1, Name = "Keyboard", Price = 50m, Stock = 10 });
        repo.Add(new Product { Id = 2, Name = "Mouse", Price = 20m, Stock = 0 });
        repo.Add(new Product { Id = 3, Name = "Monitor", Price = 120m, Stock = 5 });
        repo.Add(new Product { Id = 99, Name = "Faulty", Price = 10m, Stock = -5 });

        return new ProductService(repo);
    }

    [Fact]
    public void GetById_ValidId_ReturnsProduct()
    {
        var service = CreateServiceWithSeedData();
        var product = service.GetById(1);

        Assert.NotNull(product);
        Assert.Equal("Keyboard", product!.Name);
    }

    [Fact]
    public void GetById_InvalidId_ReturnsNull()
    {
        var service = CreateServiceWithSeedData();
        var product = service.GetById(999);

        Assert.Null(product);
    }

    [Fact]
    public void GetAvailableProducts_ReturnsOnlyInStock()
    {
        var service = CreateServiceWithSeedData();
        var available = service.GetAvailableProducts();

        Assert.DoesNotContain(available, p => p.Stock <= 0);
        Assert.Contains(available, p => p.Id == 1);
        Assert.Contains(available, p => p.Id == 3);
    }
}
