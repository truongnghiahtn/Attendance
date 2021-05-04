using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.Students;
using AppAttendance.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppAttendance.Application.System.Users
{
    public interface IUserService
    {
        Task<ApiResult<ResultLogin>> Authencate(LoginRequest request);
        Task<ApiResult<bool>> Register(RegisterUserRequest request);
        Task<ApiResult<PagedResult<UserVm>>> GetUsersPaging(GetUserPagingRequest request);
        Task<ApiResult<UserVm>> GetById(Guid id);
        Task<ApiResult<bool>> DeleteUser(Guid id);
    }
}
