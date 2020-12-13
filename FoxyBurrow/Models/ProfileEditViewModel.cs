using FoxyBurrow.Core.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoxyBurrow.Models
{
    public class ProfileEditViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }
        public DateTime Birthday { get; set; }
        public string City { get; set; }
        public Gender Gender { get; set; }
        public string PlaceOfStudy { get; set; }
        public string PlaceOfWork { get; set; }
        public string AboutMyself { get; set; }
        public string Status { get; set; }
    }
}
