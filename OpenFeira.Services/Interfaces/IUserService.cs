using OpenFeira.Data.Models;

namespace OpenFeira.BusinessLogic.Interfaces;

public interface IUserService
{
    // Returns a dictionary containing every user on the database using the user email as a key.
    Dictionary<string, User> GetUsers();
    
    // Fetch an user from the database, can be done by providing the user email.
    User? GetUser(string userEmail);
    
    void AddUser(User user);
    void UpdateUser(User user);
    void DeleteUser(string userEmail);

    // Check if the given password hash matches the one on the database.
    bool CheckUserPassword(string userEmail, string passwordHash);
}