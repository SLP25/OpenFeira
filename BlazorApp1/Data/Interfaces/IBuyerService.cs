namespace BlazorApp1.Data.Interfaces;

public interface IBuyerService
{
    public Task<Buyer> GetBuyer(string email);
    public Task MakeBid(string email, Decimal price, int amount, int standId, int productId);

}