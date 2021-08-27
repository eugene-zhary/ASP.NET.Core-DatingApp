using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsersAsync(DataContext context)
        {
            bool isUsersExist = await context.Users.AnyAsync();

            if(!isUsersExist)
            {
                var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

                var users = JsonSerializer.Deserialize<IEnumerable<AppUser>>(userData);

                foreach (var user in users)
                {
                    using var hmac = new HMACSHA512();

                    user.UserName = user.UserName.ToLower();
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
                    user.PasswordSalt = hmac.Key;

                    context.Add(user);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}