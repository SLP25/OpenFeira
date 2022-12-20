using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BlazorApp1.Data.Interfaces;

public interface IUserService
{
    public Task<Dictionary<string, User>> GetUsersAsDict();
    public Task<List<User>> GetUsersAsList();
    // public Task<EntityEntry<User>> AddUser(User user);
    public EntityEntry<User> AddUser(User user);
    public Task<User> GetUser(string email);
}