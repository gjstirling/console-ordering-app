namespace ConsoleOrderingApp.Models;

public class Order
{
    public User User { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }

    public decimal Total => Product.Price * Quantity;
}
