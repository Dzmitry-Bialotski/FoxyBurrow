using FoxyBurrow.Core.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        [MaxLength(30)]
        public string City { get; set; }
        public Gender Gender { get; set; }
        [MaxLength(100)]
        public string PlaceOfStudy { get; set; }
        [MaxLength(100)]
        public string PlaceOfWork { get; set; }
        [MaxLength(300)]
        public string AboutMyself { get; set; }
        [MaxLength(140)]
        public string Status { get; set; }
    }
}
