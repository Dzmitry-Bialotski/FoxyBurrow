using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoxyBurrow.Service.Util.Mail
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly IMessageGenerator _messageGenerator;
        private readonly bool USE_SSL = true;
        public MailService(IConfiguration configuration, IMessageGenerator messageGenerator)
        {
            _configuration = configuration;
            _messageGenerator = messageGenerator;
        }

        public async Task SendToGmailConfirmLinkAsync(string email, string subject, string link)
        {
            IConfigurationSection mailSection = _configuration.GetSection("EmailSettings");
           

            string _mailAddress = mailSection["Mail"];
            string _mailPassword = mailSection["Password"];
            string _mailName = mailSection["Name"]; ;
            var mailMessage = new MimeMessage();
            
            mailMessage.From.Add(new MailboxAddress(_mailName, _mailAddress));
            mailMessage.To.Add(new MailboxAddress("", email));
            mailMessage.Subject = subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = _messageGenerator.GenerateConfirmationMessage(link);
            mailMessage.Body = bodyBuilder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                
                await client.ConnectAsync(mailSection["EmailHost"], int.Parse(mailSection["EmailPort"]), USE_SSL);

                await client.AuthenticateAsync(_mailAddress, _mailPassword);

                await client.SendAsync(mailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
