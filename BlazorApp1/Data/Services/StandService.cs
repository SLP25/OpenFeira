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

    public async Task AddProductToStand(int productId, int standId,int amount)
    {
        if (amount <= 0) throw new Exception("The amount must be bigger than 0");
        Stand s = await GetStand(standId);
        Product? p = await _context.Products.FindAsync(productId);
        if (DateTime.Now > s.Market.StartingTime) throw new Exception("The Market has already started");
        if (p == null) throw new Exception("No product exists with the given id");
        if (p.ProductSeller != s.SellerId)
            throw new Exception("The product doesn't belong to the same user as the stand");
        if (amount > p.ProductStock) throw new Exception("There isn't enough of the product to add to the stand");
        p.ProductStock -= amount;
        _context.Products.Update(p);
        ProductInStand pis = new ProductInStand { ProductId = productId, StandId = standId, Stock = amount };
        await _context.ProductInStands.AddAsync(pis);
        await _context.SaveChangesAsync();
    }
}