using OpenFeira.Data.Context;
using OpenFeira.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace OpenFeira.Data.Services;

public class UserService : IUserService
{
    private readonly OpenFeiraDbContext _context;

    public UserService(OpenFeiraDbContext context)
    {
        _context = context;
    }

    public async Task<Dictionary<string, User>> GetUsersAsDict()
    {
        var userDictionary = await _context.Users.ToDictionaryAsync(user => user.UserEmail);
        return userDictionary;
    }

    public async Task<List<User>> GetUsersAsList()
    {
        var userList = await _context.Users.ToListAsync();
        return userList;
    }

    public EntityEntry<User> AddUser(User user)
    {
        return _context.Users.Add(user);
    }
    
    public async Task<User> GetUser(string email)
    {
        var user = await _context.Users.FindAsync(email);
        
        if (user == null) throw new Exception("User doesn't exist");
        
        return user;
    }
}