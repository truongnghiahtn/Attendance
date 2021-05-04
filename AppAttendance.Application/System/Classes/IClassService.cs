using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.System.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppAttendance.Application.System.Classes
{
    public interface IClassService
    {
        Task<ApiResult<bool>> CreateClass(CreateClassRequest request);
        Task<ApiResult<bool>> DeleteClass(int id);
        Task<ApiResult<bool>> UpdateClass(UpdateClassRequest request);
        Task<ApiResult<PagedResult<ClassVm>>> GetAllPaging(GetClassPagingRequest request);
        Task<ApiResult<ClassVm>> GetClassById(int id);

        Task<ApiResult<PagedResult<ClassVm>>> GetAll();
        //Task<ApiResult<bool>> RegisterClass(RegisterClassRequest request);
        //Task<ApiResult<bool>> UpdateClassIncour
    }
}
