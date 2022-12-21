using BlazorApp1.Data.Context;
using BlazorApp1.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Data.Services;

public class SellerService : ISellerService
{
    private readonly OpenFeiraDbContext _context;

    public SellerService(OpenFeiraDbContext context)
    {
        _context = context;
    }

    public async Task<Seller> GetSeller(string email)
    {
        var seller = await _context.Sellers.FindAsync(email);
        
        if (seller == null) throw new Exception("Seller doesn't exist");
        
        return seller;
    }
    
    public async Task<List<Sale>> GetSellerSales(string email)
    {
        return _context.Sales.Where(s => s.SellerId == email).Include("Bid.BidStandNavigation.Market").ToList();
    }
    public async Task<List<Bid>> GetSellerActiveBids(string email)
    {
        return _context.Bids.Where(b => b.SaleId == null).Where(b=>b.BidStandNavigation.Seller.UserEmail == email).Include("BidProductNavigation").ToList();
    }

    public async Task AcceptBid(int id)
    {
        var bid = await _context.Bids.FindAsync(id);
        if (bid == null)
            throw new Exception("Bid doesn't exist");
        var piss = await _context.ProductInStands.FindAsync(bid.BidProduct, bid.BidStand);
        if (piss == null) throw new Exception("product does not Exist u did shit in the db to get this error");
        if (bid.BidAmount > piss.Stock)
        {
            _context.Bids.Remove(bid);
            await _context.SaveChangesAsync();
            throw new Exception(
                "The bid was invalid since there isn't enough stock to accept the bid (It was deleted)");
        }
        bid.Sales.Add(new Sale
        {
            Bid = bid,
            Seller = bid.BidStandNavigation.Seller,
        });
        _context.Update(bid);
        await _context.SaveChangesAsync();
    }
}