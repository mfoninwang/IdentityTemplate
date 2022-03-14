using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Entities
{
    [Table("Permission")]
    public class Permission
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public string Code { get; set; } 
        
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; } 

        [Required]
        public string Entity { get; set; }

        //Navigation Properties
        public virtual ICollection<RolePermission>? Roles { get; set; }
    }
}
