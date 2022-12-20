namespace BlazorApp1.Data;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUser(string email)
    {
        var user = await _context.Users.FindAsync(email);
        if (user == null)
            throw new Exception("User doesn't exist");
        return user;
    }
}