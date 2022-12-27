using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<Admin> userManager, RoleManager<Role> roleManager, DataContext context)
        {
            if (await context.Destinations.AnyAsync()) return;

            var destData = await System.IO.File.ReadAllTextAsync("Data/DestinationData.json");

            var about1 = new About
            {
                Title = "VivaTur",
                Description = "Descrição para a empresa viva turismo",
                VivaInfo = true
            };

            var about2 = new About
            {
                Title = "Viva",
                Description = "Descrição para a pessoa Vivian",
                VivaInfo = true,
            };

            context.Abouts.Add(about1);
            context.Abouts.Add(about2);

            await context.SaveChangesAsync();

            var feedback = new Feedback
            {
                Name = "Perola",
                Description = "A tia viva me deu muito matinho e muitos petiscos",
                AboutId = 2
            };

            context.Feedbacks.Add(feedback);

            await context.SaveChangesAsync();



            if (await userManager.Users.AnyAsync()) return;

            await roleManager.CreateAsync(new Role { Name = "Admin" });

            var admin = new Admin
            {
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "Dik@1234");
            await userManager.AddToRoleAsync(admin,"Admin");

        }
    }
}