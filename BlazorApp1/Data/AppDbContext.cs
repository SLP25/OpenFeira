using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users;
        public DbSet<Organizer> Organizers;
        public DbSet<Buyer> Buyers;
        public DbSet<Seller> Sellers;
        public DbSet<Market> Markets;
        public DbSet<Sale> Sales;
        public DbSet<Stand> Stands;
        public DbSet<Bid> Bids;
        public DbSet<Product> Products;
        public DbSet<ProductDelivery> ProductDeliverys;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(b => b.Email);
        }
    }
}