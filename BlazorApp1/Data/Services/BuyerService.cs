using BlazorApp1.Data.Context;
using BlazorApp1.Data.Interfaces;

namespace BlazorApp1.Data.Services;

public class BuyerService : IBuyerService
{
    private readonly OpenFeiraDbContext _context;
    
    public async Task<Buyer> GetBuyer(string email)
    {
        var buyer = await _context.Buyers.FindAsync(email);
        
        if (buyer == null) throw new Exception("Seller doesn't exist");
        
        return buyer;
    }

    public async Task MakeBid(string email, Decimal price,int amount, int standId, int productId)
    {
        Buyer b = await GetBuyer(email);
        
        ProductInStand? piss = await _context.ProductInStands.FindAsync(productId,standId);
        if (piss == null) throw new Exception("Product doesn't exist in the stand");
        if (amount > piss.Stock) throw new Exception("There isn't enough of that product in the stand");
        _context.Add<Bid>(new Bid { BidAmount = amount,BidProduct = productId,BidStand = standId,BuyerId = email,Price = price});
        await _context.SaveChangesAsync();
    }
    
}