using ConsoleOrderingApp.Models;
using ConsoleOrderingApp.Services;
using Moq;
using System.Linq;
using System.Net.Mail;
using Xunit;

namespace ConsoleOrderingApp.Tests
{
    public class EmailServiceTests
    {
        private readonly Order testOrder = new Order
        {
            User = new User { Username = "alice", Email = "alice@test.com" },
            Product = new Product { Name = "Keyboard", Price = 50m, Stock = 10 },
            Quantity = 1
        };

        [Fact]
        public void SendOrderReceipt_NullRecipient_ThrowsArgumentException()
        {
            var service = new EmailService(() => new Mock<ISmtpClient>().Object);
            Assert.Throws<ArgumentException>(() => service.SendOrderReceipt(testOrder, null!));
        }

        [Fact]
        public void SendOrderReceipt_EmptyRecipient_ThrowsArgumentException()
        {
            var service = new EmailService(() => new Mock<ISmtpClient>().Object);
            Assert.Throws<ArgumentException>(() => service.SendOrderReceipt(testOrder, ""));
        }

        [Fact]
        public void SendOrderReceipt_ValidOrder_CallsSend()
        {
            var mockClient = new Mock<ISmtpClient>();
            var emailService = new EmailService(() => mockClient.Object);

            var testOrder = new Order
            {
                User = new User { Username = "alice", Email = "alice@example.com" },
                Product = new Product { Name = "Keyboard", Price = 50 },
                Quantity = 1
            };

            emailService.SendOrderReceipt(testOrder, testOrder.User.Email);

            mockClient.Verify(c => c.Send(It.Is<MailMessage>(m =>
                m.Subject.Contains("Keyboard") &&
                m.Body.Contains("alice") &&
                m.To.Any(addr => addr.Address == testOrder.User.Email) 
            )), Times.Once);
        }

    }
}
