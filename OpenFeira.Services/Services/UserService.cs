using OpenFeira.BusinessLogic.Interfaces;
using OpenFeira.Data.Models;
using OpenFeira.Data.Models.Context;

namespace OpenFeira.BusinessLogic.Services;

public class UserService: IUserService
{
    // The thing that let's us use the database.
    private readonly OpenFeiraDatabaseContext _database;

    public UserService(OpenFeiraDatabaseContext database)
    {
        _database = database;
    }
    
    public Dictionary<string, User> GetUsers()
    {
        return _database.Users.ToDictionary(user => user.UserEmail);
    }

    public User? GetUser(string userEmail)
    {
        return _database.Users.Find(userEmail);
    }
    
    public void AddUser(User user)
    {
        throw new NotImplementedException();
    }

    public void UpdateUser(User user)
    {
        throw new NotImplementedException();
    }

    public void DeleteUser(string userEmail)
    {
        throw new NotImplementedException();
    }


    public bool CheckUserPassword(string userEmail, string passwordHash)
    {
        var user =  _database.Users.Find(userEmail);
        return user != null && user.PasswordHash.Equals(passwordHash);
    }
}