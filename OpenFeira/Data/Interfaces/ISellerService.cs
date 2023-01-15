namespace OpenFeira.Data.Interfaces;

public interface ISellerService
{
    public Task<Seller> GetSeller(string email);
    public Task<List<Sale>> GetSellerSales(string email);
    public Task<List<Bid>> GetSellerActiveBids(string email);
    public Task AcceptBid(int id);
    public Task CreateSeller(string email, string password,int nif,string name,string? companyName,string? phoneNumber,string? website);
    public Task RegisterSellerInMarket(string email, int id,string standPhoto);

    public Task RegisterSellerInMarket(string email, int id, string standPhoto, List<int> products,
        List<int> quantities);
}