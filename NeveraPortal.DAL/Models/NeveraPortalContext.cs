using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeveraPortal.DAL.Models
{
    public class NeveraPortalContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=DESKTOP-EET2RGT;Database=NeveraPortalDB;Trusted_Connection=True;");
            optionsBuilder.UseSqlServer("Server=DESKTOP-13V6IU8;Database=NeveraPortalDB;Trusted_Connection=True;");

        }

        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Country> Countries { get; set;}
        public DbSet<City> Cities { get; set; }
        public DbSet<Job> Jobs { get; set; }
        
        public DbSet<Blog> Blogs { get; set; }
    }
}
