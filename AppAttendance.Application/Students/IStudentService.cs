using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.Students;
using AppAttendance.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppAttendance.Application.Students
{
    public interface IStudentService
    {
        Task<ApiResult<ResultStudentLogin>> Authencate(LoginRequest request);
        Task<ApiResult<bool>> Register(RegisterStudentRequest request);
        Task<ApiResult<StudentVm>> GetById(Guid id);

        Task<ApiResult<bool>> RegisterEquipment(AddEquipmentRequest request);
        Task<ApiResult<bool>> DeleteStudent(Guid id);

        Task<ApiResult<bool>> Update(UpdateStudentRequest request);

        Task<ApiResult<bool>> RegisterCourse(RegisterCourseRequest request);
        Task<ApiResult<bool>> RegisterCourseByUser(RegisterCourseRequest request);

        Task<ApiResult<PagedResult<StudentCourseVm>>> GetPagingStudentCourse(GetStudentCource request);
        Task<ApiResult<bool>> Confirm(ConfirmRcRequest request);
        Task<ApiResult<PagedResult<StudentVm>>> GetByKey(string Keyword);
        Task<ApiResult<bool>> DeleteRegisterCourse(int id);

        Task<ApiResult<PagedResult<StudentVm>>> GetAllPaging(GetStudentPagingRequest request);
        

        Task<ApiResult<EquipmentOfTeacher>> GetEquipmentTeacher(int id);

        Task<ApiResult<PagedResult<StudentSchedule>>> GetPagingBySchedule(int id_Schedule);
        Task<ApiResult<PagedResult<StudentAttendanceVm>>> GetPagingByCourse(GetPagingByCourseRequest request);

        Task<ApiResult<StudentSchedule>> GetBySchedule(GetByScheduleRequest request);

        Task<ApiResult<PagedResult<HistoryRcStudentVm>>> GetHistoryRcStudent(GetHistoryRcStudent request);

        Task<ApiResult<HistoryAttendanceVm>> GetHistoryAttendanceVm(historyAttendanceRequest request);
        


    }
}
