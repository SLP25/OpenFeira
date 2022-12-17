namespace BlazorApp1.Data;

public interface IUserService
{
    public User get(string Email);
}