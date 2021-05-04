using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.System.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppAttendance.Application.System.Roles
{
    public interface IRoleService
    {
        Task<ApiResult<PagedResult<RoleVm>>>  GetAll();
        Task<ApiResult<bool>> CreateRole(CreateRoleRequest request);

        Task<ApiResult<bool>> UpdateRole(UpdateRoleRequest request);

        Task<ApiResult<bool>> DeleteRole(Guid id);

    }
}
