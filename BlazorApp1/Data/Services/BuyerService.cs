using System.Data.Entity;
using BlazorApp1.Data.Context;
using BlazorApp1.Data.Interfaces;

namespace BlazorApp1.Data.Services;

public class BuyerService : IBuyerService
{
    private readonly OpenFeiraDbContext _context;

    public BuyerService(OpenFeiraDbContext context)
    {
        _context = context;
    }

    public async Task<Buyer> GetBuyer(string email)
    {
        var buyer = await _context.Buyers.FindAsync(email);
        
        if (buyer == null) throw new Exception("Seller doesn't exist");
        
        return buyer;
    }

    public async Task MakeBid(string email, decimal price,int amount, int standId, int productId)
    {
        Buyer b = await GetBuyer(email);

        ProductInStand? piss = await _context.ProductInStands.FindAsync(productId, standId);
        if (piss == null) throw new Exception("Product doesn't exist in the stand");
        if (amount > piss.Stock) throw new Exception("There isn't enough of that product in the stand");

        Product p = await _context.Products.FindAsync(productId);

        Sale sale = null;
        Bid bid = new Bid { BidAmount = amount, BidProduct = productId, BidStand = standId, BuyerId = email, Price = price };

        Stand stand = await _context.Stands.FindAsync(standId);
        if (price >= p.ProductBasePrice)
        {
            sale = new Sale
            {
                Bid = bid,
                SellerId = stand.SellerId
            };
            _context.Add(sale);
        }

        _context.Add(bid);
        await _context.SaveChangesAsync();
    }

    public async Task CreateBuyer(string email, string password, int nif, string name)
    {
        User? u = await _context.Users.FindAsync(email);
        if (u != null) throw new Exception("A User with that username already exists");
        Buyer b = new Buyer
        {
            UserEmailNavigation = new User
            {

                Nif = nif,
                UserEmail = email,
                PasswordHash = User.EncryptPassword(password)
            },
            Name = name
        };
        b.UserEmailNavigation.Buyer = b;
        await _context.Buyers.AddAsync(b);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Sale>> GetBuyerSales(string email)
    {
        return _context.Sales.Where(s => s.Bid.BuyerId == email).ToList();
    }
}