namespace ConsoleOrderingApp.Data;

public class EFProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public EFProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Product? GetById(int id)
    {
        return _context.Products.Find(id);
    }

    public IEnumerable<Product> GetAll()
    {
        return _context.Products.ToList();
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null) return;

        _context.Products.Remove(product);
        _context.SaveChanges();
    }
}
