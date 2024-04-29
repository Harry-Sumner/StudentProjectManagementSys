using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Data;

namespace Project_Management_System.Data
{
    public class Project_Management_SystemContext : DbContext
    {
        public Project_Management_SystemContext (DbContextOptions<Project_Management_SystemContext> options)
            : base(options)
        {
        }

        public DbSet<Topic> Topics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>().ToTable("Topic");
        }
    }
}
