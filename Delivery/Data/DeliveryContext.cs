using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Delivery;
using Delivery.Model;

namespace Delivery.Data
{
    public class DeliveryContext : DbContext
    {
        public DbSet<DeliveryRequest> DeliveryRequests { get; set; }
        public DeliveryContext()
        {
             //Database.EnsureCreated(); 
        }

        public DeliveryContext(DbContextOptions<DeliveryContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host = localhost; Port = 5432; Database = Orders; Username = postgres; Password = 1234");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeliveryRequest>().UseTpcMappingStrategy();

        }
    }
}
