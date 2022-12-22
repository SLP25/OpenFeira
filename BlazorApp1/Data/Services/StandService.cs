using BlazorApp1.Data.Context;
using BlazorApp1.Data.Interfaces;

namespace BlazorApp1.Data.Services;

public class StandService : IStandService
{
    private readonly OpenFeiraDbContext _context = new OpenFeiraDbContext();

    public async Task<Stand> GetStand(int id)
    {
        Stand? s = await _context.Stands.FindAsync(id);
        if (s == null) throw new Exception("No stand exists with the given id");
        return s;
    }

    public async Task<List<Stand>> GetStandsInMarket(int id)
    {
        return _context.Stands.Where(s => s.MarketId == id).ToList();
    }

    public async Task<List<ProductInStand>> GetProductsInStand(int id)
    {
        Stand s = await GetStand(id);
        return s.ProductInStands.ToList();
    }
}