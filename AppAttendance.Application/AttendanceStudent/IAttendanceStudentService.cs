using AppAttendance.ViewModel.AttendanceStudents;
using AppAttendance.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppAttendance.Application.AttendanceStudent
{
    public interface IAttendanceStudentService
    {
        Task<ApiResult<bool>> CreatebyTeacher(CreateAttendancebyTeacher request);
        Task<ApiResult<bool>> CreatebyStudent(CreateAttendancebyStudent request);
    }
}
