using DataBaseContext;
using Microsoft.AspNetCore.Identity;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBContext
{
    public class Seeder 
    {
        static string baseDir = Directory.GetCurrentDirectory();
        public async static Task Seed(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, DataBase context)
        {
            await context.Database.EnsureCreatedAsync();
            if (context.Users.Any())
            {
                List<string> roles = new List<string>() { "Admin", "Regular" };
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }
                var path = File.ReadAllText(FilePath(baseDir, "JsonFile/User.json"));
                var users = JsonConvert.DeserializeObject<List<User>>(path);
                /*await context.Users.AddRangeAsync(users);*/
                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "James@123");
                    if (user == users[0])
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(user, "Regular");
                    }
                }
            }
        }
        static string FilePath(string folderName, string fileName)
        {
            return Path.Combine(folderName, fileName);
        }
    }
}
