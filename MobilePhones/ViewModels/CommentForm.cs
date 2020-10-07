using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePhones.ViewModels
{
    public class CommentForm
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int PhoneId { get; set; }
    }
}
