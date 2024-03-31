using System.Collections.Generic;
using myvetjob.Roles.Dto;

namespace myvetjob.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
