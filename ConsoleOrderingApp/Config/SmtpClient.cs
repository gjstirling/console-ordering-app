using System.Net;
using System.Net.Mail;

namespace ConsoleOrderingApp.Services;

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

    public void Send(MailMessage message) => _client.Send(message);

    public void Dispose() => _client.Dispose();
}
