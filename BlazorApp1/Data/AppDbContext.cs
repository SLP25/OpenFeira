using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Organizer> Organizer { get; set; }
        public DbSet<Buyer> Buyer { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<Market> Market { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<Stand> Stand { get; set; }
        public DbSet<Bid> Bid { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductDelivery> ProductDelivery { get; set; }

    }
}
