using ConsoleOrderingApp.Data;
using ConsoleOrderingApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleOrderingApp.Services;

public class ProductService
{
    // Returns all products that have stock
    public List<Product> GetAvailableProducts()
    {
        return SeedData.Products.Where(p => p.Stock > 0).ToList();
    }

    // Get product by ID
    public Product? GetById(int id)
    {
        return SeedData.Products.FirstOrDefault(p => p.Id == id);
    }
}
