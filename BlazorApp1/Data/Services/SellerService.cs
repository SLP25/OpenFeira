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
        var seller = _context.Sellers.Where(s => s.UserEmail == email).Include("Products").First();
        
        if (seller == null) throw new Exception("Seller doesn't exist");
        
        return seller;
    }
    
    public async Task<List<Sale>> GetSellerSales(string email)
    {
        return _context.Sales.Where(s => s.SellerId == email).Include("Bid.BidStandNavigation.Market").ToList();
    }
    public async Task<List<Bid>> GetSellerActiveBids(string email)
    {
        return _context.Bids.Where(b => b.Sale == null).Where(b=>b.BidStandNavigation.Seller.UserEmail == email).Include("BidProductNavigation").ToList();
    }

    public async Task AcceptBid(int id)
    {
        var bid = _context.Bids.Where(b => b.BidId == id).Include("BidStandNavigation").First();
        if (bid == null)
            throw new Exception("Bid doesn't exist");
        if (_context.Sales.Count(s => s.BidId == id) != 0)
            throw new Exception("Bid already accepted");
        var piss = await _context.ProductInStands.FindAsync( bid.BidStand,bid.BidProduct);
        if (piss == null) throw new Exception("product does not Exist u did shit in the db to get this error");
        if (bid.BidAmount > piss.Stock)
        {
            _context.Bids.Remove(bid);
            await _context.SaveChangesAsync();
            throw new Exception(
                "The bid was invalid since there isn't enough stock to accept the bid (It was deleted)");
        }
        Sale sale = new Sale
        {
            Bid = bid,
            SellerId = bid.BidStandNavigation.SellerId
        };
        piss.Stock -= bid.BidAmount;
        _context.Update(piss);
        await _context.Sales.AddAsync(sale);
        bid.Sale = sale;
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

    public async Task RegisterSellerInMarket(string email, int id, string standPhoto ,List<int> products ,List<int> quantities)
    {
        _context.Database.BeginTransaction();
        Market? m = await _context.Markets.FindAsync(id);
        if (m == null) throw new Exception("The market with the given id doesn't exist");
        Seller seller = await GetSeller(email);
        if (DateTime.Now > m.StartingTime) throw new Exception("The market has already begun");
        if (m.Stands.Count == m.TotalStands) throw new Exception("The market has reached the maximum number of stands");
        if (_context.Stands.Count(s => s.SellerId==email && s.MarketId==id)!=0)
            throw new Exception("The user is already participating");
        Stand stand = new Stand
        {
            MarketId = id, SellerId = email, StandPhotoPath = standPhoto
        };
        m.Stands.Add(stand);
        _context.Markets.Update(m);
        await _context.SaveChangesAsync();
        Stand s = _context.Stands.Where(s => s.SellerId == email).Where(s => s.MarketId == id).First();
        foreach (var item in products.Select((value, i) => (value, i)))
        {
            int amount = quantities[item.i];
            int productId = item.value;
            if (amount > 0)
            {
                Product? p = await _context.Products.FindAsync(productId);
                if (p == null) throw new Exception("No product exists with the given id");
                if (p.ProductSeller != s.SellerId)
                    throw new Exception("The product doesn't belong to the same user as the stand");
                if (amount > p.ProductStock)
                    throw new Exception("There isn't enough of the product to add to the stand");
                p.ProductStock -= amount;
                _context.Products.Update(p);
                ProductInStand pis = new ProductInStand { ProductId = productId, StandId = s.StandId, Stock = amount };
                await _context.ProductInStands.AddAsync(pis);
            }
        }
        await _context.SaveChangesAsync();
        await _context.Database.CommitTransactionAsync();
    }
    
    
}