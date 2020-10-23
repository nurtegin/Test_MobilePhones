using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePhones.Models
{
    public class MobileContext : IdentityDbContext<User>
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FileOnDatabaseModel> FilesOnDatabase { get; set; }
        public DbSet<FileOnFileSystemModel> FilesOnFileSystem { get; set; }

        public MobileContext(DbContextOptions<MobileContext> options)
            : base(options)
        {
        }

    }
}
