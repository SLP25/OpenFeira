namespace BlazorApp1.Data.Interfaces;

public interface IOrganizerService
{
    public Task<Organizer> GetOrganizer(string email);
    public Task CreateOrganizer(string email, string password, int nif, string name);
    public Task<List<Market>> GetOrganizerMarkets(string email);
}