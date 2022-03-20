using PermissionBasedTemplate.Entities.Commom;
using System.ComponentModel.DataAnnotations;

namespace PermissionBasedTemplate.Entities
{
    public class Category:AuditableEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
