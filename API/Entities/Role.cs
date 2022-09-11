using Microsoft.AspNetCore.Identity;
namespace API.Entities
{
    public class Role : IdentityRole<int>
    {
        public ICollection<AdminRole> Roles { get; set; }
    }
}
