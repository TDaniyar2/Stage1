using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Stage1;
using Stage1.Model;

namespace Stage1.Data
{
    public class DbTaskContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbTaskContext()
        {
            // Database.EnsureCreated(); 
        }

        public DbTaskContext(DbContextOptions<DbTaskContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host = localhost; Port = 5432; Database = Orders; Username = postgres; Password = 1234");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().UseTpcMappingStrategy();

        }
    }
}