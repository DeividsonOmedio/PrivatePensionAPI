using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class ContextBase : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Clients { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Contribution> Contributions { get; set; }

        public ContextBase(DbContextOptions<ContextBase> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Data Seeding
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Produto A", Price = 100, Description = "Descrição do Produto A" },
                new Product { Id = 2, Name = "Produto B", Price = 200, Description = "Descrição do Produto B" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "Admin 1", Email = "admin@admin.com", Password = "Admin@123", Role = Domain.Enums.UserRoles.admin },
                new User { Id = 2, Username = "Cliente 1", Email = "cliente2@client.com", Password = "Client@123", Role = Domain.Enums.UserRoles.client , WalletBalance = 300 }
            );
        }
    }
}
