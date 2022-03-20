using System.ComponentModel.DataAnnotations;

namespace PermissionBasedTemplate.Enums
{
    public enum PermissionsEnum : short
    {
        [Display(GroupName = "ApplicationRole", Name = "View")] RoleView,
        [Display(GroupName = "ApplicationRole", Name = "Create")] RoleCreate,
        [Display(GroupName = "ApplicationRole", Name = "Approve Level 1")] RoleApproveLevel1,
        [Display(GroupName = "ApplicationRole", Name = "Approve Level 2")] RoleApproveLevel2,

        [Display(GroupName = "User", Name = "View")] ViewUser,
        [Display(GroupName = "User", Name = "Create")] CreateUser,
        [Display(GroupName = "User", Name = "Approve Level 1")] ApproveUserLevel1,
        [Display(GroupName = "User", Name = "Approve Level 2")] ApproveUserLevel2

    }

    public class PermissionTest
    {
        public void Test()
        {
            
        }
    }
}
