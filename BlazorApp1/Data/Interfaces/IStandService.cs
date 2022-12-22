namespace BlazorApp1.Data.Interfaces;

public interface IStandService
{
    public Task<Stand> GetStand(int id);
    public Task<List<Stand>> GetStandsInMarket(int id);
    public Task<List<ProductInStand>> GetProductsInStand(int id);

    public Task AddProductToStand(int productId, int standId, int amount);
}