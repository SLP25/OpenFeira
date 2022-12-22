using BlazorApp1.Data.Context;
using BlazorApp1.Data.Interfaces;

namespace BlazorApp1.Data.Services;

public class SaleService : ISaleService
{
    private readonly OpenFeiraDbContext _context = new OpenFeiraDbContext();
    

}