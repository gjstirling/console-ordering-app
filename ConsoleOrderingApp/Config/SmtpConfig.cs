using DotNetEnv;

namespace ConsoleOrderingApp.Config;

public class SmtpConfig
{
    public virtual string SmtpHost { get; }
    public virtual int SmtpPort { get; }
    public virtual string SenderEmail { get; }
    public virtual string SenderPassword { get; }

    public SmtpConfig()
    {
        Env.Load();

        SmtpHost = Env.GetString("SMTP_HOST")
            ?? throw new InvalidOperationException("SMTP_HOST not set in .env");

        var portStr = Env.GetString("SMTP_PORT")
            ?? throw new InvalidOperationException("SMTP_PORT not set in .env");

        if (!int.TryParse(portStr, out var port))
            throw new InvalidOperationException("SMTP_PORT must be a number");

        SmtpPort = port;

        SenderEmail = Env.GetString("SMTP_EMAIL")
            ?? throw new InvalidOperationException("SMTP_EMAIL not set in .env");

        SenderPassword = Env.GetString("SMTP_PASSWORD")
            ?? throw new InvalidOperationException("SMTP_PASSWORD not set in .env");
    }
}
