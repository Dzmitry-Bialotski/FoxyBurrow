using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoxyBurrow.Service.Util.Mail
{
    public interface IMailService
    {
         Task SendToGmailConfirmLinkAsync(string email, string subject, string message);
    }
}
