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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SPMS_User>().Ignore(e => e.Name);
            modelBuilder.Entity<SPMS_Staff>().ToTable("Staff");
            modelBuilder.Entity<SPMS_Student>().ToTable("Student");

        }

        public DbSet<SPMS_User> SPMS_Account { get; set; }
        public DbSet<SPMS_Student> Student { get; set; }
        public DbSet<SPMS_Staff> Staff { get; set; }

        public DbSet<Topic> Topic { get; set; } = default!;

        public DbSet<Division> Division { get; set; } = default!;

        public DbSet<School> School { get; set; } = default!;

        public DbSet<Course> Course { get; set; } = default;
    }
}
