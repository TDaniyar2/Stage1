using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Stage1;

namespace Stage1
{
    public class DbTaskContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbTaskContext()
        {
           // Database.EnsureCreated(); 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host = localhost; Port = 5432; Database = taskdb; Username = postgres; Password = 1234");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().UseTpcMappingStrategy();

        }
    }
}