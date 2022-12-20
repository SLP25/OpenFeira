using BlazorApp1.Data.Context;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OpenFeira.Seeder;

public class Seeder
{
    public static void run()
    {
        using (var context = new OpenFeiraDbContext())
        {
            object p = Database.SetInitializer<OpenFeiraDbContext>(new UserSeeder());

 
            Console.ReadKey();
        }
    }
}