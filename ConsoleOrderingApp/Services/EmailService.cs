using ConsoleOrderingApp.Models;
using System.Net;
using System.Net.Mail;
using DotNetEnv;

namespace ConsoleOrderingApp.Services
{
    public interface ISmtpClient : IDisposable
    {
        NetworkCredential Credentials { get; set; }
        bool EnableSsl { get; set; }
        void Send(MailMessage message);
    }

    public class SmtpClientWrapper : ISmtpClient
    {
        private readonly SmtpClient _client;

        public SmtpClientWrapper(string host, int port)
        {
            _client = new SmtpClient(host, port);
        }

        public NetworkCredential Credentials
        {
            get => _client.Credentials as NetworkCredential ?? new NetworkCredential();
            set => _client.Credentials = value;
        }

        public bool EnableSsl
        {
            get => _client.EnableSsl;
            set => _client.EnableSsl = value;
        }

        public void Send(MailMessage message)
        {
            _client.Send(message);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }

    public interface IEmailService
    {
        void SendOrderReceipt(Order order, string recipientEmail);
    }

    public class EmailService : IEmailService
    {
        private readonly string smtpHost;
        private readonly int smtpPort;
        private readonly string senderEmail;
        private readonly string senderPassword;
        private readonly Func<ISmtpClient> _smtpClientFactory;

        public EmailService(Func<ISmtpClient>? smtpClientFactory = null)
        {
            Env.Load();

            smtpHost = Env.GetString("SMTP_HOST") ?? throw new InvalidOperationException("SMTP_HOST not set in .env");
            string portStr = Env.GetString("SMTP_PORT") ?? throw new InvalidOperationException("SMTP_PORT not set in .env");
            if (!int.TryParse(portStr, out smtpPort))
                throw new InvalidOperationException("SMTP_PORT must be a valid number");

            senderEmail = Env.GetString("SMTP_EMAIL") ?? throw new InvalidOperationException("SMTP_EMAIL not set in .env");
            senderPassword = Env.GetString("SMTP_PASSWORD") ?? throw new InvalidOperationException("SMTP_PASSWORD not set in .env");

            _smtpClientFactory = smtpClientFactory ?? (() => new SmtpClientWrapper(smtpHost, smtpPort));
        }

        public void SendOrderReceipt(Order order, string recipientEmail)
        {
            if (string.IsNullOrWhiteSpace(recipientEmail))
                throw new ArgumentException("Recipient email cannot be null or empty", nameof(recipientEmail));

            using var client = _smtpClientFactory();
            client.Credentials = new NetworkCredential(senderEmail, senderPassword);
            client.EnableSsl = true;

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
}
