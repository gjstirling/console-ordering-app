using ConsoleOrderingApp.Config;
using ConsoleOrderingApp.Models;
using System.Net;
using System.Net.Mail;

namespace ConsoleOrderingApp.Services;

public class EmailService
{
    private readonly SmtpConfig _config;
    private readonly ISmtpClient _smtpClient;

    public EmailService(SmtpConfig config, ISmtpClient smtpClient)
    {
        _config = config;
        _smtpClient = smtpClient;
    }

    public void SendOrderReceipt(Order order, string recipientEmail)
    {
        if (string.IsNullOrWhiteSpace(recipientEmail))
            throw new ArgumentException("Recipient email cannot be null or empty", nameof(recipientEmail));

        _smtpClient.Credentials = new NetworkCredential(_config.SenderEmail, _config.SenderPassword);
        _smtpClient.EnableSsl = true;

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_config.SenderEmail),
            Subject = $"Order Receipt - {order.Product.Name}",
            Body = GenerateBody(order)
        };
        mailMessage.To.Add(new MailAddress(recipientEmail));

        _smtpClient.Send(mailMessage);
    }

    private string GenerateBody(Order order)
    {
        return
$@"Hello {order.User.Username},

Thank you for your order!

Product: {order.Product.Name}
Quantity: {order.Quantity}
Total: ${order.Total}

Have a great day!
";
    }
}
