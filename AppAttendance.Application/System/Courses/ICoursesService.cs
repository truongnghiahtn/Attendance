using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.System.Courses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppAttendance.Application.System.Courses
{
    public interface ICoursesService
    {
        Task<ApiResult<bool>> CreateCourse(CreateCourseRequest request);
        Task<ApiResult<bool>> UpdateCourse(UpdateCourseRequest request);
        Task<ApiResult<CourseVm>> GetCourseById(int id);
        Task<ApiResult<bool>> DeleteCourse(int id);
        Task<ApiResult<PagedResult<CourseVm>>> GetAllPaging(GetCoursePagingRequest request);
        Task<ApiResult<PagedResult<CourseVm>>> GetAllPagingByDay(GetCourseByDayRequest request);
        Task<ApiResult<PagedResult<CourseVm>>> GetAllByIdStudent(GetCourseByStudentRequest request);
        Task<ApiResult<PagedResult<CourseVm>>> GetAllByIdTeacher(GetCourseByStudentRequest request);
        Task<ApiResult<PagedResult<CourseVm>>> GetAllBySubject(GetPagingBySubjectRequest request);
        Task<ApiResult<PagedResult<CourseVm>>> GetAllByKey(string keyword);

    }
}
