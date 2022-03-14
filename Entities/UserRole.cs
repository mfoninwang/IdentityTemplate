using Microsoft.AspNetCore.Identity;


namespace WebApplication1.Entities
{
    public class UserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }

        public virtual ApplicationRole Role { get; set; }
    }
}
