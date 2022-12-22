using BlazorApp1.Data.Context;
using BlazorApp1.Data.Interfaces;

namespace BlazorApp1.Data.Services;

public class ProductService : IProductService
{
    private readonly OpenFeiraDbContext _context = new OpenFeiraDbContext();

    public async Task<Product> GetProduct(int id)
    {
        Product? p = await _context.Products.FindAsync(id);
        if (p == null) throw new Exception("No product with the given id exists");
        return p;
    }

}