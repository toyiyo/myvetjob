using Abp.Authorization;
using myvetjob.Authorization.Roles;
using myvetjob.Authorization.Users;

namespace myvetjob.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
