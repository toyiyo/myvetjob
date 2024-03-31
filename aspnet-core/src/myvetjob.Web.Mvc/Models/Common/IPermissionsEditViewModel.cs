using System.Collections.Generic;
using myvetjob.Roles.Dto;

namespace myvetjob.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}