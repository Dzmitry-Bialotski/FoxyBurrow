using FoxyBurrow.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoxyBurrow.Models
{
    public class ProfileViewModel
    {
        public string Id { get; set; }
        public User User { get; set; }
    }
}
