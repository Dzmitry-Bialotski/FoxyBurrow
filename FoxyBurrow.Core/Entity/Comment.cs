using System;
using System.Collections.Generic;
using System.Text;

namespace FoxyBurrow.Core.Entity
{
    public class Comment : Base
    {
        public string Text { get; set; }
        public DateTime MessageDate { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public long PostId { get; set; }
        public Post Post { get; set; }
        public Comment()
        {
            MessageDate = DateTime.UtcNow;
        }
    }
}
