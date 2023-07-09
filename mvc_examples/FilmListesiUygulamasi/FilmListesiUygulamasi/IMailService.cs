using MailKit.Net.Smtp;
using MimeKit;

public interface IMailService
{
    void SendEmail(string to, string subject, string body);
}

public class MailService : IMailService
{
    public void SendEmail(string to, string subject, string body)
    {
        // E-posta gönderme işlemlerini gerçekleştir
        MimeMessage message = new MimeMessage();
        message.From.Add(new MailboxAddress("Gönderen", "gonderen@example.com"));
        message.To.Add(new MailboxAddress("", to));
        message.Subject = subject;

        message.Body = new TextPart("plain")
        {
            Text = body
        };

        using (var client = new SmtpClient())
        {
            client.Connect("smtp.mailtrap.io", 587, false);
            client.Authenticate("your-smtp-username", "your-smtp-password");

            client.Send(message);
            client.Disconnect(true);
        }
    }
}
