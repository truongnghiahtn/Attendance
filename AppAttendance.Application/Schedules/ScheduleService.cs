using AppAttendance.Data.EF;
using AppAttendance.Data.Entities;
using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.Schedules;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AppAttendance.Application.Schedules
{
    public class ScheduleService : IScheduleService
    {
        private readonly AppAttendanceDbContext _context;

        public ScheduleService(AppAttendanceDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateSchedule(CreateSchduleRequest request)
        {
            var course = await _context.Courses.FindAsync(request.Id_Course);
            if (course == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy dữ liệu");
            }
            var subject = await _context.Subjects.FindAsync(course.Id_Subject);
            if (subject == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy dữ liệu");
            }
            var query = from s in _context.Schedules
                        where s.Id_Course == request.Id_Course && s.Date == request.DateBegin.Date &&s.TimeBegin== request.TimeBegin
                        select new { s };
            var scheduleData = await _context.Schedules.FirstOrDefaultAsync(x => x.Id_Course == request.Id_Course && x.Date == request.DateBegin.Date && x.TimeBegin == request.TimeBegin);
            if (scheduleData!=null)
            {
                return new ApiErrorResult<bool>("Lịch trình đã tồn tại");
            }
            var schedule = new Schedule()
            {
                Id_Course = request.Id_Course,
                Id_Class = request.Id_Class,
                Date = request.DateBegin.Date,
                TimeBegin = request.TimeBegin,
                TimeEnd = request.TimeEnd,
                DateCreate = DateTime.UtcNow.AddHours(7),
                DateUpdate = DateTime.UtcNow.AddHours(7)
            };
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Tạo mới thành công");
        }

        public async Task<ApiResult<bool>> CreateScheduleAuto(CreateSchduleRequest request)
        {
            var course = await _context.Courses.FindAsync(request.Id_Course);
            if (course == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy dữ liệu");
            }
            var subject = await _context.Subjects.FindAsync(course.Id_Subject);
            if (subject == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy dữ liệu");
            }
            if(request.DateBegin>course.DateBegin&&request.DateBegin<course.DateBegin.AddDays(7))
            {
                 for(int i = 0; i < subject.Lesson; i++)
                {
                    var schedule = new Schedule()
                    {
                        Id_Course = request.Id_Course,
                        Id_Class = request.Id_Class,
                        Date = request.DateBegin.AddDays(7*i).Date,
                        TimeBegin=request.TimeBegin,
                        TimeEnd=request.TimeEnd,
                        DateCreate = DateTime.UtcNow.AddHours(7),
                        DateUpdate = DateTime.UtcNow.AddHours(7)
                    };
                    _context.Schedules.Add(schedule);
                }
                await _context.SaveChangesAsync();
            }
            return new ApiSuccessResult<bool>("Tạo mới thành công");
        }

        public async Task<ApiResult<bool>> DeleteSchdule(int id)
        {
            var schdule = await _context.Schedules.FindAsync(id);
            var his = await _context.HistoryAttendances.FindAsync(id);
            if(his!=null)
            {
                _context.HistoryAttendances.Remove(his);
            }
            if (schdule == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy ");
            }
            _context.Schedules.Remove(schdule);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Xóa thành công");
        }

        public async Task<ApiResult<PagedResult<ScheduleVm>>> GetAllByTeacher(GetScheduleByTeacherRequest request)
        {
            var query = from s in _context.Schedules
                        join c in _context.Courses on s.Id_Course equals c.Id_Course
                        join cl in _context.Classes on s.Id_Class equals cl.Id_Class
                        where c.Id_Course == request.Id_Course 
                        select new { s, c,cl };
            int totalRow = await query.CountAsync();
            var data = await query
                .Select(x => new ScheduleVm()
                {
                     Id_Schedule=x.s.Id_Schedule,
                     NameClass=x.cl.Name,
                     NameCourse=x.c.Name,
                     Date=x.s.Date,
                     TimeBegin=x.s.TimeBegin,
                     TimeEnd=x.s.TimeEnd
                }).ToListAsync();
            var pageData = new PagedResult<ScheduleVm>()
            {
                PageIndex = 1,
                PageSize = totalRow,
                Items = data,
                TotalRecords = totalRow,
            };
            return new ApiSuccessResult<PagedResult<ScheduleVm>>(pageData);
        }

        public Task<ApiResult<PagedResult<PageSchedule>>> GetAllPaging(GetSchedulePagingRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<PagedResult<ScheduleVmDetail>>> GetByDate( DateTime date,Guid id_Student)
        {
            var query = from s in _context.Schedules
                        join c in _context.Courses on s.Id_Course equals c.Id_Course
                        join cl in _context.Classes on s.Id_Class equals cl.Id_Class
                        join rc in _context.RegisterCourses on c.Id_Course equals rc.Id_Course
                        join st in _context.Students on rc.Id_Student equals st.Id
                        where st.Id == id_Student 
                        select new { s, c, cl };
            var data = await query.Where(x=> x.s.Date.Date == date.Date)
                .Select(x => new ScheduleVmDetail()
                {
                    Id_Schedule = x.s.Id_Schedule,
                    Id_Course = x.c.Id_Course,
                    NameClass = x.cl.Name,
                    NameCourse = x.c.Name,
                    Date = x.s.Date,
                    TimeBegin = x.s.TimeBegin,
                    TimeEnd = x.s.TimeEnd
                }).ToListAsync();
            var pageData = new PagedResult<ScheduleVmDetail>()
            {
                PageIndex = 1,
                PageSize = data.Count(),
                Items = data,
                TotalRecords = data.Count(),
            };
            return new ApiSuccessResult<PagedResult<ScheduleVmDetail>>(pageData);
        }

        public async Task<ApiResult<bool>> UpdateSchdule(UpdateSchduleRequest request)
        {
            var schedule = await _context.Schedules.FindAsync(request.Id_Schedule);
            if (schedule == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy lịch trình");
            }
            schedule.Id_Course = request.Id_Course;
            schedule.Id_Class = request.Id_Class;
            schedule.Date = request.DateBegin.Date;
            schedule.TimeBegin = request.TimeBegin;
            schedule.TimeEnd = request.TimeEnd;
            schedule.DateUpdate = DateTime.UtcNow.AddHours(7);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Cập nhật thành công");
        }
    }
}
