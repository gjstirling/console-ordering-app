using ConsoleOrderingApp.Data;
using ConsoleOrderingApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleOrderingApp.Services;

public class ProductService
{
    private readonly List<Product> _products;

    public ProductService(List<Product>? products = null)
    {
        _products = products ?? SeedData.Products;
    }

    public List<Product> GetAvailableProducts()
    {
        return _products.Where(p => p.Stock > 0).ToList();
    }

    public Product? GetById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }
}
