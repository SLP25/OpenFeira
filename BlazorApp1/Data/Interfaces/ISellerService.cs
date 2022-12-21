namespace BlazorApp1.Data.Interfaces;

public interface ISellerService
{
    public Task<Seller> GetSeller(string email);
    public Task<List<Sale>> GetSellerSales(string email);

    public Task<List<Bid>> GetSellerActiveBids(string email);

    public Task AcceptBid(int id);
}