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

    public async Task CreateSeller(string email, string password,int nif,string name,string? companyName,string? phoneNumber,string? website)
    {
        User? s = await _context.Users.FindAsync(email);
        if (s != null) throw new Exception("A user with that username already exists");
        var seller = new Seller { 
            UserEmailNavigation = new User
            {
                Nif = nif,
                UserEmail = email,
                PasswordHash = User.EncryptPassword(password)
            },
            Name = name,
            CompanyName = companyName,
            PhoneNumber = phoneNumber,
            Website = website
        };
        seller.UserEmailNavigation.Seller = seller;
        await _context.Sellers.AddAsync(seller);
        await _context.SaveChangesAsync();
    }

    public async Task RegisterSellerInMarket(string email, int id,string standPhoto)
    {
        Market? m = await _context.Markets.FindAsync(id);
        if (m == null) throw new Exception("The market with the given id doesn't exist");
        Seller s = await GetSeller(email);
        if (DateTime.Now > m.StartingTime) throw new Exception("The market has already begun");
        if (m.Stands.Count == m.TotalStands) throw new Exception("The market has reached the maximum number of stands");
        m.Stands.Add(new Stand
        {
            MarketId = id,SellerId = email,StandPhotoPath = standPhoto
        }
        );
        _context.Markets.Update(m);
        await _context.SaveChangesAsync();
    }
}