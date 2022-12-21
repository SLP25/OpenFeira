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

    public async void AcceptBid(int id)
    {
        //TODO:: Change
    }
}