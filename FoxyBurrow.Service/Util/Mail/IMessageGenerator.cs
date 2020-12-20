using System;
using System.Collections.Generic;
using System.Text;

namespace FoxyBurrow.Service.Util.Mail
{
    public interface IMessageGenerator
    {
        string GenerateConfirmationMessage(string link);
    }
}
