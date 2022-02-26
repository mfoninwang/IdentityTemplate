using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{

    // Add profile data for application users by adding properties to the ApplicationUser class
    [Table("User")]
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string? LastName { get; set; }

        [PersonalData]
        public string? FirstName { get; set; }

        [PersonalData]
        public string? TaxOffice { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        // Navigation Properties
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
    }

}