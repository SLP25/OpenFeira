namespace BlazorApp1.Data.Interfaces;

public interface IBuyerService
{
    public Task<Buyer> GetBuyer(string email);
    public Task MakeBid(string email, Decimal price, int amount, int standId, int productId);
    public Task CreateBuyer(string email, string password, int nif, string name);

    public Task<List<Sale>> GetBuyerSales(string email);

}