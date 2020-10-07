using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePhones.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
        public int PhoneId { get; set; }
        public Phone Phone { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
