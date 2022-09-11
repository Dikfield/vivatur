using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class Admin:IdentityUser<int>
    {
        public ICollection<AdminRole> Roles { get; set; }
    }
}
