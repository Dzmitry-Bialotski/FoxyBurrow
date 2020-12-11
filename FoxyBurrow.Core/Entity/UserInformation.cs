using FoxyBurrow.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoxyBurrow.Core.Entity
{
    public class UserInformation : Base
    {
        public User User { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ImagePath { get; set; }
        public DateTime Birthday { get; set; }
        public string City { get; set; }
        public Gender Gender { get; set; }
        public string PlaceOfStudy { get; set; }
        public string PlaceOfWork { get; set; }
        public string AboutMyself { get; set; }
        public string Status { get; set; }
        
    }
}
