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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(p => p.Products).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
