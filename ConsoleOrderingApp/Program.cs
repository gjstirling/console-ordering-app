using ConsoleOrderingApp.Services;
using ConsoleOrderingApp.Config;

var authService = new AuthService();
var productService = new ProductService();
var orderService = new OrderService();

var emailConfig = new SmtpConfig();
using var smtpClient = new SmtpClientWrapper(
    emailConfig.SmtpHost,
    emailConfig.SmtpPort
);

var emailService = new EmailService(emailConfig, smtpClient);

Console.Write("Enter username: ");
var username = Console.ReadLine();
var user = authService.Login(username ?? "");

if (user == null)
{
    Console.WriteLine("Login failed: user not found.");
    return;
}

Console.WriteLine($"Login successful! Welcome, {user.Username}");

Console.WriteLine("\nAvailable products:");
foreach (var product in productService.GetAvailableProducts())
{
    Console.WriteLine($"{product.Id}. {product.Name} - ${product.Price} (Stock: {product.Stock})");
}

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

Console.Write("Enter quantity: ");
if (!int.TryParse(Console.ReadLine(), out int quantity))
{
    Console.WriteLine("Invalid quantity.");
    return;
}

try
{
    var order = orderService.CreateOrder(user, selectedProduct, quantity);

    Console.WriteLine("\n--- RECEIPT ---");
    Console.WriteLine($"User: {order.User.Username}");
    Console.WriteLine($"Product: {order.Product.Name}");
    Console.WriteLine($"Quantity: {order.Quantity}");
    Console.WriteLine($"Total: ${order.Total}");
    Console.WriteLine("----------------");

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
