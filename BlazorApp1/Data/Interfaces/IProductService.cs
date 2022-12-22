namespace BlazorApp1.Data.Interfaces;

public interface IProductService
{
    public Task<Product> GetProduct(int id);
}