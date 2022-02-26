using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ApplicationRole:IdentityRole
    {
        [Required]
        public override string Name { get => base.Name ; set => base.Name = value; }

        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public virtual ICollection<UserRole> UserRoles { get; set; }

    }
}
