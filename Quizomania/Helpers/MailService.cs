using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MimeKit.Utils;
using Quizomania.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.Helpers
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendVerificationTokenAsync(string token, User user)
        {
            //Load email template
            string filepath = Directory.GetCurrentDirectory() + "\\EmailTemplates\\EmailVerificationToken.html";
            StreamReader reader = new StreamReader(filepath);
            string mailBody = reader.ReadToEnd();
            reader.Close();

            // Create email
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = "Welcome to Quizomania, please verify your email.";

            //Create email body
            var builder = new BodyBuilder();

            //Add logo
            filepath = Directory.GetCurrentDirectory() + "\\EmailTemplates\\quizomaniaLogo.jpg";
            var logo = builder.LinkedResources.Add(filepath);
            logo.ContentId = MimeUtils.GenerateMessageId();

            //Format body
            builder.HtmlBody = mailBody
                                .Replace("[cid]", logo.ContentId)
                                .Replace("[username]", user.Username)
                                .Replace("[code]", token);

            //Add body to email
            email.Body = builder.ToMessageBody();

            //Send email
            await SendMailAsync(email);

        }

        private async Task SendMailAsync(MimeMessage message)
        {
            // Send email
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(message);
            smtp.Disconnect(true);
        }
    }
}
