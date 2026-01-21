using ConsoleOrderingApp.Services;
using DotNetEnv;

var authService = new AuthService();
var productService = new ProductService();
var orderService = new OrderService();

Env.Load();
Console.WriteLine("SMTP_HOST from .env: " + Env.GetString("SMTP_HOST"));

// --- Login ---
Console.Write("Enter username: ");
var username = Console.ReadLine();
var user = authService.Login(username ?? "");

if (user == null)
{
    Console.WriteLine("Login failed: user not found.");
    return;
}

Console.WriteLine($"Login successful! Welcome, {user.Username}");

// --- List Products ---
Console.WriteLine("\nAvailable products:");
foreach (var product in productService.GetAvailableProducts())
{
    Console.WriteLine($"{product.Id}. {product.Name} - ${product.Price} (Stock: {product.Stock})");
}

// --- Select Product ---
Console.Write("\nSelect product ID: ");
if (!int.TryParse(Console.ReadLine(), out int productId))
{
    Console.WriteLine("Invalid input.");
    return;
}

var selectedProduct = productService.GetById(productId);
if (selectedProduct == null)
{
    Console.WriteLine("Product not found.");
    return;
}

// --- Enter Quantity ---
Console.Write("Enter quantity: ");
if (!int.TryParse(Console.ReadLine(), out int quantity))
{
    Console.WriteLine("Invalid quantity.");
    return;
}

// --- Create Order ---
try
{
    var order = orderService.CreateOrder(user, selectedProduct, quantity);

    Console.WriteLine("\n--- RECEIPT ---");
    Console.WriteLine($"User: {order.User.Username}");
    Console.WriteLine($"Product: {order.Product.Name}");
    Console.WriteLine($"Quantity: {order.Quantity}");
    Console.WriteLine($"Total: ${order.Total}");
    Console.WriteLine("----------------");

    var emailService = new EmailService();
    try
    {
        emailService.SendOrderReceipt(order, user.Email);
        Console.WriteLine($"Receipt emailed to {user.Email}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to send email: {ex.Message}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Order failed: {ex.Message}");
}
