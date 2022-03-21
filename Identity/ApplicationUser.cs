using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermissionBasedTemplate.Identity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    [Table("User")]
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? LastName { get; set; }

        public string? FirstName { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        // Navigation Properties
        public virtual ICollection<IdentityUserRole<Guid>>? Roles { get; set; }
        public virtual ICollection<IdentityUserClaim<Guid>>? Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<Guid>>? Logins { get; set; }
        public virtual ICollection<IdentityUserToken<Guid>>? Tokens { get; set; }
    }

}