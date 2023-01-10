namespace BlazorApp1.Data.Interfaces;

public interface IProductService
{
    public Task<Product> GetProduct(int id);
    public Task CreateProduct(string seller, string desc, decimal price, string name, int stock, List<String> imagePaths);
    public Task EditProduct(int id,string? desc,decimal? price,string? name,int? stock,List<String>? imagePaths);
    public Task<List<Product>> getProductsOfSeller(string seller);
}