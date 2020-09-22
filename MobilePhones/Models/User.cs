using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePhones.Models
{
    public class User : IdentityUser
    {
        public DateTime DateOfBirth { get; set; }
    }
}
