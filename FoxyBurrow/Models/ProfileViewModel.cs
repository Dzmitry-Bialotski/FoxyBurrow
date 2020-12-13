using FoxyBurrow.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoxyBurrow.Models
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public string currentUserId { get; set; }
    }
}
