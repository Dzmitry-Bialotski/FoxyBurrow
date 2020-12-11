using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoxyBurrow.Core.Entity
{
    public class User : IdentityUser
    {
        public UserInformation UserInformation { get; set; }
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Message> Messages { get; set; }
        public List<Request> OutgoingRequests { get; set; }
        public List<Request> IncomingRequests { get; set; }
        public User()
        {
            Posts = new List<Post>();
            Comments = new List<Comment>();
            Messages = new List<Message>();
            OutgoingRequests = new List<Request>();
            IncomingRequests = new List<Request>();
        }

    }
}
