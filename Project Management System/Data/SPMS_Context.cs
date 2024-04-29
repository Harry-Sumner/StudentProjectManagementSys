using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Project_Management_System.Data
{
    public class SPMS_Context : IdentityDbContext<SPMS_User>
    {
        public SPMS_Context(DbContextOptions<SPMS_Context> options) : base(options) { }

        public DbSet<SPMS_User> SPMS_User { get; set; }

        public DbSet<Topic> Topics { get; set; } = default!;

        public DbSet<Division> Divisions { get; set; } = default!;

        public DbSet<School> Schools { get; set; } = default!;

        public DbSet<Course> Courses { get; set; } = default;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SPMS_User>().Ignore(e => e.Name);
          
            
        }
    }
}
