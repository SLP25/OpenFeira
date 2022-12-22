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

    public async Task CreateProduct(string seller, string desc, decimal price, string name, int stock,
        List<String> imagePaths)
    {
        Seller? s = await _context.Sellers.FindAsync(seller);
        if (s == null) throw new Exception("There is no seller with the given email");
        Product p = new Product
        {
            ProductStock = stock,
            ProductDescription = desc,
            ProductName = name,
            ProductBasePrice = price,
            ProductSeller = seller
        };
        _context.Products.Add(p);
        foreach (var image in imagePaths)
        {
            _context.ProductPhotos.Add(new ProductPhoto { Product = p, ProductPhotoPath = image });
        }

        await _context.SaveChangesAsync();

    }



    public async Task EditProduct(int id,string? desc,decimal? price,string? name,int? stock,List<String>? imagePaths)
    {
        Product p = await GetProduct(id);
        p.ProductDescription = desc != null ? desc : p.ProductDescription;
        p.ProductName = name != null ? name : p.ProductName;
        p.ProductStock = stock != null ? stock.Value : p.ProductStock;
        p.ProductBasePrice = price != null ? price.Value : p.ProductBasePrice;
        _context.Products.Update(p);
        if (imagePaths != null)
        {
            foreach (var pp in p.ProductPhotos)
            {
                _context.ProductPhotos.Remove(pp);    
            }
            foreach (var image in imagePaths)
            {
                _context.ProductPhotos.Add(new ProductPhoto { ProductId = id, ProductPhotoPath = image });
            }
        }
        await _context.SaveChangesAsync();
    }

}