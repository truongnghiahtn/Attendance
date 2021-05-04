using AppAttendance.Data.EF;
using AppAttendance.Data.Entities;
using AppAttendance.ViewModel.AttendanceStudents;
using AppAttendance.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AppAttendance.Application.AttendanceStudent
{
    public class AttendanceStudentService : IAttendanceStudentService
    {
        private readonly AppAttendanceDbContext _context;

        public AttendanceStudentService(AppAttendanceDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreatebyStudent(CreateAttendancebyStudent request)
        {
            var schdule = await _context.Schedules.FindAsync(request.Id_Schedule);
            if (schdule == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy dữ liệu");
            }
            var repo = await _context.HistoryAttendances.FindAsync(request.Id_Schedule);
            if (repo == null)
            {
                return new ApiErrorResult<bool>("Lỗi điểm danh");
            }

            var queryStudent = from e in _context.Equipment
                               join st in _context.Students on e.Id_Student equals st.Id
                               join rc in _context.RegisterCourses on st.Id equals rc.Id_Student
                               where schdule.Id_Course == rc.Id_Course
                               select new { e, st, rc };

            var dataStudent = await queryStudent.Select(x => x.e.Id_BLE).ToListAsync();

            var query = from d in _context.DetailHAs
                        where d.Id_HistoryAttendance == request.Id_Schedule
                        select new { d };
            var data = await query.Select(x => x.d.Id_Student).ToListAsync();


            var equipment = await _context.Equipment.FindAsync(request.Id_BLE);
            int check = data.IndexOf(equipment.Id_Student);
            int checkStudent = dataStudent.IndexOf(request.Id_BLE);
            if (check == -1 && checkStudent > -1)
            {
                var detailHa = new DetailHA()
                {
                    
                    Id_HistoryAttendance = request.Id_Schedule,
                    Id_Student = equipment.Id_Student,
                    DateCreate = DateTime.UtcNow.AddHours(7)
                };
                _context.DetailHAs.Add(detailHa);
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>("Đã được điểm danh ");
            }
            return new ApiSuccessResult<bool>("Điểm danh thành công");
        }

        public async Task<ApiResult<bool>> CreatebyTeacher(CreateAttendancebyTeacher request)
        {
            var schdule = await _context.Schedules.FindAsync(request.Id_Schedule);
            if (schdule == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy dữ liệu");
            }
            var repo = await _context.HistoryAttendances.FindAsync(request.Id_Schedule);
            if (repo == null)
            {
                return new ApiErrorResult<bool>("Lỗi điểm danh");
            }


            var queryStudent = from e in _context.Equipment
                        join st in _context.Students on e.Id_Student equals st.Id
                        join rc in _context.RegisterCourses on st.Id equals rc.Id_Student
                        where schdule.Id_Course == rc.Id_Course
                        select new { e, st, rc };

            var dataStudent = await queryStudent.Select(x => x.e.Id_BLE).ToListAsync();

            var query = from d in _context.DetailHAs
                        where d.Id_HistoryAttendance == request.Id_Schedule
                        select new { d };
            var data = await query.Select(x => x.d.Id_Student).ToListAsync();


            foreach (var item in request.Content)
            {
                var equipment = await _context.Equipment.FindAsync(item.Id_BLE);
                int check = data.IndexOf(equipment.Id_Student);
                int checkStudent = dataStudent.IndexOf(item.Id_BLE);
                if (check == -1&&checkStudent>-1)
                {
                    var detailHa = new DetailHA()
                    {
                        Id_HistoryAttendance = request.Id_Schedule,
                        Id_Student = equipment.Id_Student,
                        DateCreate = DateTime.UtcNow.AddHours(7)
                    };
                    data.Add(equipment.Id_Student);
                    _context.DetailHAs.Add(detailHa);
                }
            };
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Điểm danh thành công");
        }
    }
}
