using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.Entities
{
    public class ApplicationRole : IdentityRole<string>
    {
        public string? Code { get; set; }

        [Required]
        public override string Name { get => base.Name; set => base.Name = value; }

        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public virtual ICollection<UserRole>? Users { get; set; }

        public virtual ICollection<RolePermission>? Permissions { get; set; }

    }
}
