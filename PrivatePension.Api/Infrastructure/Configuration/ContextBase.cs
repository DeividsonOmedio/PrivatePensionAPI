using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public class ContextBase : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Contribution> Contributions { get; set; }

        public ContextBase(DbContextOptions<ContextBase> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Purchase>()
            .HasOne(p => p.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Client)
                .WithMany()
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            var admin = new User
            {
                Id = 1,
                UserName = "Admin 1",
                Email = "admin@admin.com",
                Role = Domain.Enums.UserRolesEnum.admin
            };
            var client = new User
            {
                Id = 2,
                UserName = "Cliente 1",
                Email = "cliente1@client.com",
                Role = Domain.Enums.UserRolesEnum.client,
                WalletBalance = 1300
            };

            var passwordHasher = new PasswordHasher<User>();
            admin.Password = passwordHasher.HashPassword(admin, "Admin@123");
            client.Password = passwordHasher.HashPassword(client, "Client@123");

            modelBuilder.Entity<User>().HasData(admin, client);

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Crédito Privado II RF", Price = 300, Description = "O Renda Fixa Crédito Privado II é um fundo que tem títulos de crédito privado selecionados pela BRAM ", Available = true },
                new Product { Id = 2, Name = "Crédito Privado Premium RF", Price = 500, Description = "Investir em títulos públicos e privados pós fixados, com rentabilidade atrelada ao CDI", Available = true }
            );
        }
    }
}
