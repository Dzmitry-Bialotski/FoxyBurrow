using System;
using System.Collections.Generic;
using System.Text;

namespace FoxyBurrow.Core.Entity
{
    public class Message : Base
    {
        public DateTime MessageDate { get; set; }
        public string Text { get; set; }
        public long ChatId { get; set; }
        public Chat Chat { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public Message()
        {
            MessageDate = DateTime.UtcNow;
        }
    }
}
