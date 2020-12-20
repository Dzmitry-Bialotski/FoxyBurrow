using System;
using System.Collections.Generic;
using System.Text;

namespace FoxyBurrow.Service.Util.Mail
{
    public class MessageGenerator : IMessageGenerator
    {
        public string GenerateConfirmationMessage(string link)
        {
            return string.Format($@"<h3><p>Hi, That is FoxyBurrow Support team, that`s your confirmation link.</p>
     <p><a href={link}> {link} </a></p>
     <p>if you didn`t register in our site, please, tell us about that!</p></h3>", link);
        }
    }
}
