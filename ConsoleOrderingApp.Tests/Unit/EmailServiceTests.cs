using ConsoleOrderingApp.Config;
using ConsoleOrderingApp.Models;
using ConsoleOrderingApp.Services;
using Moq;
using System.Net.Mail;
using Xunit;

namespace ConsoleOrderingApp.Tests.NewFolder;

public class EmailServiceTests
{
    private readonly Order testOrder = new Order
    {
        User = new User { Username = "alice", Email = "alice@test.com" },
        Product = new Product { Name = "Keyboard", Price = 50m, Stock = 10 },
        Quantity = 1
    };

    private EmailService CreateEmailService(Mock<ISmtpClient>? mockClient = null)
    {
        var config = new SmtpConfigMock(); // See below
        var client = mockClient?.Object ?? new Mock<ISmtpClient>().Object;
        return new EmailService(config, client);
    }

    [Fact]
    public void SendOrderReceipt_NullRecipient_ThrowsArgumentException()
    {
        var service = CreateEmailService();
        var ex = Assert.Throws<ArgumentException>(() => service.SendOrderReceipt(testOrder, null!));
        Assert.Contains("Recipient email cannot be null or empty", ex.Message);
    }

    [Fact]
    public void SendOrderReceipt_EmptyRecipient_ThrowsArgumentException()
    {
        var service = CreateEmailService();
        var ex = Assert.Throws<ArgumentException>(() => service.SendOrderReceipt(testOrder, ""));
        Assert.Contains("Recipient email cannot be null or empty", ex.Message);
    }

    [Fact]
    public void SendOrderReceipt_ValidOrder_CallsSend()
    {
        var mockClient = new Mock<ISmtpClient>();
        var service = CreateEmailService(mockClient);

        service.SendOrderReceipt(testOrder, testOrder.User.Email);

        mockClient.Verify(c => c.Send(It.Is<MailMessage>(m =>
            m.Subject.Contains("Keyboard") &&
            m.Body.Contains("alice") &&
            m.To.Count == 1 &&
            m.To[0].Address == testOrder.User.Email
        )), Times.Once);
    }

    private class SmtpConfigMock : SmtpConfig
    {
        public override string SmtpHost => "smtp.test.com";
        public override int SmtpPort => 999;
        public override string SenderEmail => "test@gmail.com";
        public override string SenderPassword => "password";
    }
}
