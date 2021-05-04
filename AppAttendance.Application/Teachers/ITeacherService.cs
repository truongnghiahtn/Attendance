using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.Teachers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppAttendance.Application.Teachers
{
    public interface ITeacherService
    {
        Task<ApiResult<bool>> UpdateTeacher(UpdateTeacherRequest request);
        Task<ApiResult<TeacherVm>> GetTeacherById(Guid id);
        Task<ApiResult<PagedResult<TeacherVm>>> GetAllPaging(GetTeacherPagingRequest request);
        Task<ApiResult<PagedResult<TeacherVm>>> GetAll();
        Task<ApiResult<PagedResult<EquipmentOfStudent>>> GetEquipmentOfStudent(int idCourse);

        Task<ApiResult<bool>> CreateRepo(CreateRepoRequest request);
    }
}
