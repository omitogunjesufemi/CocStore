using Api.Models;
using Microsoft.EntityFrameworkCore;
namespace Api.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<CartItem> CartItems { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Address> Address { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<RefreshToken> RefreshTokens {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(p => p.Products).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = Guid.Parse("0eecb374-ddcd-4a45-8b3d-9476b264fc86"), Name = "User", Description = "Regular user role" },
                new Role { Id = Guid.Parse("441b8bbe-ccaa-4f68-bb46-dc2182ed34d8"), Name = "Admin", Description = "Administrator role" },
                new Role { Id = Guid.Parse("97abc384-e98b-4b03-83eb-6afd11501f7c"), Name = "Worker", Description = "Worker Role" }
            );
        }
    }
}
