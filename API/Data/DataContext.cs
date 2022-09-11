using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<Admin, Role, int,
        IdentityUserClaim<int>, AdminRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<VivaPhoto> VivaPhotos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Admin>()
                .HasMany(ur => ur.Roles)
                .WithOne(u => u._Admin)
                .HasForeignKey(u => u.UserId)
                .IsRequired();

            builder.Entity<Role>()
                .HasMany(ur => ur.Roles)
                .WithOne(u => u._Role)
                .HasForeignKey(u => u.RoleId)
                .IsRequired();
        }
    }
}
