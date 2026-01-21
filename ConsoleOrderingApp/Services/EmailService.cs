using ConsoleOrderingApp.Models;
using System.Net;
using System.Net.Mail;
using DotNetEnv;

namespace ConsoleOrderingApp.Services;

public class EmailService
{
    private readonly string smtpHost;
    private readonly int smtpPort;
    private readonly string senderEmail;
    private readonly string senderPassword;

    public EmailService()
    {
        // Load .env from current directory (should be bin/Debug/net8.0)
        DotNetEnv.Env.Load();

        Console.WriteLine("SMTP_HOST from .env: " + DotNetEnv.Env.GetString("SMTP_HOST"));

        smtpHost = Env.GetString("SMTP_HOST")
            ?? throw new InvalidOperationException("SMTP_HOST not set in .env");
        string portStr = Env.GetString("SMTP_PORT")
            ?? throw new InvalidOperationException("SMTP_PORT not set in .env");

        if (!int.TryParse(portStr, out smtpPort))
            throw new InvalidOperationException("SMTP_PORT must be a valid number");

        senderEmail = Env.GetString("SMTP_EMAIL")
            ?? throw new InvalidOperationException("SMTP_EMAIL not set in .env");

        senderPassword = Env.GetString("SMTP_PASSWORD")
            ?? throw new InvalidOperationException("SMTP_PASSWORD not set in .env");
    }

    public void SendOrderReceipt(Order order, string recipientEmail)
    {
        if (string.IsNullOrWhiteSpace(recipientEmail))
            throw new ArgumentException("Recipient email cannot be null or empty", nameof(recipientEmail));

        using var client = new SmtpClient(smtpHost, smtpPort)
        {
            Credentials = new NetworkCredential(senderEmail, senderPassword),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(senderEmail),
            Subject = $"Order Receipt - {order.Product.Name}",
            Body = GenerateBody(order)
        };

        mailMessage.To.Add(recipientEmail);

        client.Send(mailMessage);
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
