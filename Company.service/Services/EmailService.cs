using Company.service.Helper;
using Company.service.Interfaces;
using Company.Services.Helper;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Company.service.Services
{
    public class EmailService : IEmailService
    {
        private EmailData _options;

        public EmailService(IOptions<EmailData> options)
        {
            _options = options.Value;
        }

        public void SendEmail(Email input)
        {
            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_options.Email),
                Subject = input.Subject
            };
            mail.To.Add(MailboxAddress.Parse(input.To));
            var builder = new BodyBuilder();
            builder.TextBody = input.Body;

            mail.Body = builder.ToMessageBody();
            mail.From.Add(new MailboxAddress(_options.DisplayName, _options.Email));

            using var smtp = new SmtpClient();
            smtp.Connect(_options.Host, _options.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_options.Email, _options.Password);
            smtp.Send(mail);
            smtp.Disconnect(true);

        }

    }
}
