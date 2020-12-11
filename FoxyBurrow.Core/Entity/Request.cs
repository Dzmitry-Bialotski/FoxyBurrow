using FoxyBurrow.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoxyBurrow.Core.Entity
{
    public class Request : Base
    {
        public RequestType RequestType { get; set; }
        public User UserSender { get; set; }
        public string UserSenderId { get; set; }
        public User UserReceiver { get; set; }
        public string UserReceiverId { get; set; }
    }
}
