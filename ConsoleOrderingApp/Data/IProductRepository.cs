using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleOrderingApp.Data
{
    public interface IProductRepository
    {
        Product? GetById(int id);
        IEnumerable<Product> GetAll();
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }

    public class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new();

        public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);
        public IEnumerable<Product> GetAll() => _products;
        public void Add(Product product) => _products.Add(product);
        public void Update(Product product)
        {
            var index = _products.FindIndex(p => p.Id == product.Id);
            if (index >= 0) _products[index] = product;
        }
        public void Delete(int id) => _products.RemoveAll(p => p.Id == id);
    }

}
