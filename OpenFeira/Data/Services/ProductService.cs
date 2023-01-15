using OpenFeira.Data.Context;
using OpenFeira.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OpenFeira.Data.Services;

public class ProductService : IProductService
{
    private readonly OpenFeiraDbContext _context = new OpenFeiraDbContext();

    public ProductService(OpenFeiraDbContext context)
    {
        _context = context;
    }

    public async Task<Product> GetProduct(int id)
    {
        Product? p = _context.Products.Where(p => p.ProductId == id).Include("ProductPhotos").First();
        if (p == null) throw new Exception("No product with the given id exists");
        return p;
    }

    public async Task CreateProduct(string seller, string desc, decimal price, string name, int stock,
        List<String> imagePaths)
    {
        Seller? s = _context.Sellers.Find(seller);
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

        _context.SaveChanges();
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

    public async Task<List<Product>> getProductsOfSeller(string seller)
    {
        List<Product> p = _context.Products.Where(p => p.ProductSeller == seller).Include("ProductPhotos").ToList();
        return p;
    }

}