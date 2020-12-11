using System;
using System.Collections.Generic;
using System.Text;

namespace FoxyBurrow.Core.Entity
{
    public class Chat : Base
    {
        public List<Message> Messages { get; set; }
        public User FirstUser { get; set; }
        public string FirstUserId { get; set; }
        public User SecondUser { get; set; }
        public string SecondUserId { get; set; }
        public Chat()
        {
            Messages = new List<Message>();
        }
    }
}
