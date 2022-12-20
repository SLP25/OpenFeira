namespace BlazorApp1.Data.Context;

public class Seeder
{
    public static void Main()
    {
        var userSeeder = new UserSeeder(); 
        userSeeder.SeedUsers();
    }
}