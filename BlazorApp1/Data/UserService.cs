namespace BlazorApp1.Data;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public User get(string Email)
    {
        return _context.Users.Find(Email);
    }
}