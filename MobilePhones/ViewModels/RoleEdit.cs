using Microsoft.AspNetCore.Identity;
using MobilePhones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePhones.ViewModels
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<User> NonMembers { get; set; }

    }
}
