using Application.Contracts.Infrastructure;
using Application.Dtos;
using Domain.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Services
{
    public class EmailService(IOptions<EmailOption> emailOption) : IEmailService
    {
        private readonly EmailOption emailOption = emailOption.Value;

        // TODO: Sitenin adı değişince burayı da değiştir
        private const string Name = "BlogApp";
        private const string From = "no-reply@blogapp.com.tr";

        public void SendEmail(EmailDto emailDto)
        {
            var host = emailOption.Host;
            var username = emailOption.User;
            var password = emailOption.Password;
            var port = emailOption.Port;

            // Create Email
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(Name, From));
            email.To.Add(MailboxAddress.Parse(emailDto.To));
            email.Subject = emailDto.Subject;

            // Email body
            BodyBuilder bodyBuilder = new() { HtmlBody = emailDto.Body };

            email.Body = bodyBuilder.ToMessageBody();

            using var smptp = new SmtpClient();
            smptp.Connect(host, port, SecureSocketOptions.StartTls);
            smptp.Authenticate(username, password);
            smptp.Send(email);
            smptp.Disconnect(true);
        }
    }
}
