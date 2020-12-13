using FoxyBurrow.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoxyBurrow.Models
{
    public class SearchFriendsViewModel
    {
        public ICollection<User> Users { get; set; }
        public string Request { get; set; }
    }
}
