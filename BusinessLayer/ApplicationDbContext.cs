using ECommerceDomains.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TbCategories> TbCategories { get; set; }
        public DbSet<TbPoducts> TbPoducts { get; set; }
        public DbSet<ApplicationUser> DBApplicationUser { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<OrderHeader> TbOrderHeader { get; set; }
        public DbSet<OrderDetails> TbOrderDetails { get; set; }
        public DbSet<WishlistCart> WishlistCart { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional model configuration can go here

            modelBuilder.Entity<WishlistCart>().HasKey(w => w.Id);
        }
    }
  
}
