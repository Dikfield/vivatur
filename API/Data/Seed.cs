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
            var dest = JsonSerializer.Deserialize<List<Destination>>(destData);
            
            foreach(var d in dest)
            {
                context.Destinations.Add(d);
            }

            await context.SaveChangesAsync();


            var descData = await System.IO.File.ReadAllTextAsync("Data/DescriptionData.json");
            var desc = JsonSerializer.Deserialize<List<DestinationDescription>>(descData);

            foreach (var d in desc)
            {
                context.DestinationDescriptions.Add(d);
            }

            await context.SaveChangesAsync();

            var descPhotoData = await System.IO.File.ReadAllTextAsync("Data/DescriptionPhotosData.json");
            var descPhoto = JsonSerializer.Deserialize<List<DescriptionPhoto>>(descPhotoData);

            foreach (var d in descPhoto)
            {
                context.DescriptionPhotos.Add(d);
            }

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