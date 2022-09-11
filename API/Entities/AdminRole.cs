using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AdminRole:IdentityUserRole<int>
    {
        public Role _Role { get; set; }
        public Admin _Admin { get; set; }
    }
}
