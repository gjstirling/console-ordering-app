using ConsoleOrderingApp.Models;

namespace ConsoleOrderingApp.Data;

public static class SeedData
{

    public static List<User> Users = new()
{
    new User { Id = 1, Username = "alice", Email = "freshmitts@outlook.com" },
    new User { Id = 2, Username = "bob", Email = "bob@example.com" }
};


    public static List<Product> Products = new()
    {
        new Product { Id = 1, Name = "Keyboard", Price = 49.99m, Stock = 10 },
        new Product { Id = 2, Name = "Mouse", Price = 19.99m, Stock = 20 },
        new Product { Id = 3, Name = "Monitor", Price = 129.99m, Stock = 5 }
    };
}
