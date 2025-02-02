﻿using BlazorApp1.Data;
using BlazorApp1.Data.Context;
using BlazorApp1.Data.Interfaces;
using BlazorApp1.Data.Services;
using System.Data.Entity;

namespace OpenFeira.Seeder;

public class UserSeeder
{

    private readonly Random _random = new();
    
    private string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
    
    private int RandomDigits(int length)
    {
        var value = string.Empty;
        
        for (var i = 0; i < length; i++)
            value = string.Concat(value, _random.Next(10).ToString());
        
        return int.Parse(value);
    }

    private string RandomRole()
    {
        var roles = new List<string>() {"Buyer", "Seller", "Organizer"};
        var randomIndex = _random.Next(roles.Count);
        return roles[randomIndex];
    }

    private User[] GenerateUsers(int amount)
    {
        var users = new User[amount];
        
        foreach (var num in Enumerable.Range(0, amount))
        {
            var username = RandomString(12);
            var nif = RandomDigits(9);
            var passwordHash = RandomString(50);
            var role = RandomRole();

            users[num] = new User { 
                UserEmail = username, 
                PasswordHash = passwordHash, 
                Nif = nif, 
                Role = role
            };
        }

        return users;
    }

    internal void Seed(OpenFeiraDbContext context)
    {
        
        var users = GenerateUsers(50);

        foreach(User? u in users)
        {
            context.Users.Add(u);
        }
        context.SaveChanges();
    }
}

