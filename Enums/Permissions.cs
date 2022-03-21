using System.ComponentModel.DataAnnotations;

namespace PermissionBasedTemplate.Enums
{

    public enum ApplicationPermissions : short
    {
        [Display(GroupName = "Role", Name = "View")]
        RoleView,

        [Display(GroupName = "Role", Name = "Create")]
        RoleCreate,

        [Display(GroupName = "Role", Name = "Approve Level 1")]
        RoleApproveLevel1,

        [Display(GroupName = "Role", Name = "Approve Level 2")]
        RoleApproveLevel2,

        [Display(GroupName = "User", Name = "View")]
        UserView,

        [Display(GroupName = "User", Name = "Create")]
        UserCreate,

        [Display(GroupName = "User", Name = "Approve Level 1")]
        ApproveUserLevel1,

        [Display(GroupName = "User", Name = "Approve Level 2")]
        ApproveUserLevel2
    }

    public class Test
    {
        public Test()
        {
        }
    }
}
