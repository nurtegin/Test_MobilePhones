﻿using MobilePhones.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePhones.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public int Price { get; set; }
        public List<Comment> Comments { get; set; }

        [NotMapped]
        public CommentForm CommentForm { get; set; }

    }
}
