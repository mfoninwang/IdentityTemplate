using System.ComponentModel.DataAnnotations.Schema;

namespace PermissionBasedTemplate.Identity
{
    public class RolePermission
    {
        //[ForeignKey("Role")]
        public string RoleId { get; set; }

        //[ForeignKey("Permission")]
        public int PermissionId { get; set; }

        public virtual ApplicationRole Role { get; set; }

        public virtual Permission Permission { get; set; }

    }
}
