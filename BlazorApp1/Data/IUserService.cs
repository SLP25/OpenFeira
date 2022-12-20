namespace BlazorApp1.Data;

public interface IUserService
{
    public Task<User> GetUser(string email);
}