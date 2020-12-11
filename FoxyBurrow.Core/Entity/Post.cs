using System;
using System.Collections.Generic;
using System.Text;

namespace FoxyBurrow.Core.Entity
{
    public class Post : Base
    {
        public DateTime MessageDate { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public string ImagePath { get; set; }
        public Post()
        {
            MessageDate = DateTime.Now;
            Comments = new List<Comment>();
        }
    }
}
