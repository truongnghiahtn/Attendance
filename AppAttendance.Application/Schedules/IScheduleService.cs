using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.Schedules;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppAttendance.Application.Schedules
{
    public interface IScheduleService
    {
        Task<ApiResult<bool>> CreateScheduleAuto(CreateSchduleRequest request);
        Task<ApiResult<bool>> CreateSchedule(CreateSchduleRequest request);
        Task<ApiResult<bool>> DeleteSchdule(int id);
        Task<ApiResult<bool>> UpdateSchdule(UpdateSchduleRequest request);
        Task<ApiResult<PagedResult<PageSchedule>>> GetAllPaging(GetSchedulePagingRequest request);
        Task<ApiResult<PagedResult<ScheduleVm>>> GetAllByTeacher(GetScheduleByTeacherRequest request);

        Task<ApiResult<PagedResult<ScheduleVmDetail>>> GetByDate( DateTime date,Guid id_Student);
    }
}
