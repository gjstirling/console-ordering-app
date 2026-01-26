using ConsoleOrderingApp.Data;

namespace ConsoleOrderingApp.Services;

    public class ProductService
    {
        private readonly IProductRepository _productRepo;

        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public List<Product> GetAvailableProducts()
        {
            return _productRepo.GetAll()
                               .Where(p => p.Stock > 0)
                               .ToList();
        }

        public Product? GetById(int id)
        {
            return _productRepo.GetById(id);
        }

        public void RestockProduct(int productId, int amount)
        {
            var product = _productRepo.GetById(productId);
            if (product is null) return;

            product.Stock += amount;
            _productRepo.Update(product);
        }
    }