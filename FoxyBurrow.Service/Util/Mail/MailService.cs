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
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
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
            bodyBuilder.HtmlBody = string.Format($@"<h3><p>Hi, That is FoxyBurrow Support team, that`s your confirmation link.</p>
     <p><a href={link}> {link} </a></p>
     <p>if you didn`t register in our site, please, tell us about that!</p></h3>", link);
            mailMessage.Body = bodyBuilder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);

                await client.AuthenticateAsync(_mailAddress, _mailPassword);

                await client.SendAsync(mailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
