namespace OpenFeira.Data.Interfaces;

public interface IMarketService
{
    public Task<Market> GetMarket(int id);
    public List<Market> GetMarkets();

    public Task CreateMarket(string name, DateTime start, DateTime end, string description, string? location,
        int standsN, string photoPath, string organizerEmail);

    public Task<List<Stand>> GetStandsInMarket(int id);
}