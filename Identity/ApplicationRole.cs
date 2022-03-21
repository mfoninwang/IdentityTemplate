using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using PermissionBasedTemplate.Models;

namespace PermissionBasedTemplate.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string? Code { get; set; }

        [Required]
        public override string Name { get => base.Name; set => base.Name = value; }

        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public virtual ICollection<IdentityUserRole<Guid>>? Users { get; set; }

        public virtual ICollection<RolePermission>? Permissions { get; set; }

        public virtual ICollection<IdentityRoleClaim<Guid>>? Claims { get; set; }


    }
}
