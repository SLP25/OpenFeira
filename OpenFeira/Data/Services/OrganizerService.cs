using OpenFeira.Data.Context;
using OpenFeira.Data.Interfaces;

namespace OpenFeira.Data.Services;

public class OrganizerService : IOrganizerService
{
    private readonly OpenFeiraDbContext _context = new OpenFeiraDbContext();
    public async Task<Organizer> GetOrganizer(string email)
    {
        Organizer? o = await _context.Organizers.FindAsync(email);
        if (o == null) throw new Exception("There is no organizer with the given email");
        return o;
    }

    public async Task CreateOrganizer(string email, string password, int nif, string name)
    {
        User? u = await _context.Users.FindAsync(email);
        if (u != null) throw new Exception("A User with the given email already exists");
        Organizer o = new Organizer
        {
            UserEmailNavigation = new User
            {
                UserEmail = email,
                PasswordHash = User.EncryptPassword(password),
                Nif = nif
            },
            OrganizerName = name
        };
        o.UserEmailNavigation.Organizer = o;
        await _context.Organizers.AddAsync(o);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Market>> GetOrganizerMarkets(string email)
    {
        Organizer o = await GetOrganizer(email);
        return o.Markets.ToList();
    }
    
}