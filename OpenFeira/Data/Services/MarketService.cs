using OpenFeira.Data.Context;
using OpenFeira.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OpenFeira.Data.Services;

public class MarketService : IMarketService
{
    private readonly OpenFeiraDbContext _context = new OpenFeiraDbContext();

    public MarketService(OpenFeiraDbContext context)
    {
        _context = context;
    }

    public async Task<Market> GetMarket(int id)
    {
        Market? m = _context.Markets.Where(m => m.MarketId == id).Include("Stands").First();
        if (m == null) throw new Exception("No market with the given id exists");
        return m;
    }
    public List<Market> GetMarkets()
    {
        List<Market> m = _context.Markets.ToList();
        return m;
    }

    public async Task CreateMarket(string name,DateTime start,DateTime end,string description,string? location,int standsN,string photoPath,string organizerEmail)
    {
        Organizer? o = await _context.Organizers.FindAsync(organizerEmail);
        if (o == null) throw new Exception("Organizer with the given email doesn't exist");
        Market m = new Market
        {
            MarketName = name,
            StartingTime = start,
            EndingTime = end,
            MarketDescription = description,
            MarketLocation = location,
            TotalStands = standsN,
            MarketPhotoPath = photoPath,
            Organizer =  o
        };
        await _context.Markets.AddAsync(m);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Stand>> GetStandsInMarket(int id)
    {
        Market m = await GetMarket(id);
        return m.Stands.ToList();

    }
}