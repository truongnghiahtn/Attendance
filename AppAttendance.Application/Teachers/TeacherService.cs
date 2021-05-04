using AppAttendance.Data.EF;
using AppAttendance.Data.Entities;
using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.Teachers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AppAttendance.Application.Teachers
{
    public class TeacherService : ITeacherService
    {
        private readonly AppAttendanceDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public TeacherService(AppAttendanceDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ApiResult<bool>> CreateRepo(CreateRepoRequest request)
        {
          
            var schedule = await _context.Schedules.FindAsync(request.Id_Schedule);
            //if (schedule.Date != DateTime.UtcNow.AddHours(7).Date)
            //{
            //    return new ApiErrorResult<bool>("Ngày tạo Không hợp lệ");
            //}
            if (await _context.HistoryAttendances.FindAsync(request.Id_Schedule) != null)
            {
                return new ApiSuccessResult<bool>("Kho chứa đã được tạo");
            }
            if (schedule.Id_Course!=request.Id_Course)
            {
                return new ApiErrorResult<bool>("Không tồn tại khóa học");
            }
            var data = new HistoryAttendance()
            {
                Id_HistoryAttendace=request.Id_Schedule,
                Id_Schedule=request.Id_Schedule,
                Id_Course = request.Id_Course,
                DateAttendance = DateTime.UtcNow.AddHours(7).Date,
                Id_EquipmentTeacher = request.Id_EquipmentTeacher,
            };
            _context.HistoryAttendances.Add(data);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Bạn đã tạo thành công");
        }

        public async Task<ApiResult<PagedResult<TeacherVm>>> GetAll()
        {
            var queryst = from t in _context.Teachers
                          select new { t };
            var queryCourse = from c in _context.Courses
                              select new { c };

            int totalRow = await queryst.CountAsync();
            var data = await queryst
                .Select(x => new TeacherVm()
                {
                    Id = x.t.Id,
                    FullName = x.t.FullName,
                    Email = x.t.Email,
                }).ToListAsync();
            var pagedResult = new PagedResult<TeacherVm>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = data
            };

            return new ApiSuccessResult<PagedResult<TeacherVm>>(pagedResult);
        }

        public async Task<ApiResult<PagedResult<TeacherVm>>> GetAllPaging(GetTeacherPagingRequest request)
        {
            var queryst = from t in _context.Teachers
                          select new { t };
            var queryCourse = from c in _context.Courses
                              select new { c };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                queryst = queryst.Where(x => x.t.FullName.Contains(request.Keyword) || x.t.Email.Contains(request.Keyword));
            }

            int totalRow = await queryst.CountAsync();
            var data = await queryst.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new TeacherVm()
                {
                    Id = x.t.Id,
                    FullName = x.t.FullName,
                    Email = x.t.Email,
                    TeacherCourses = queryCourse.Where(y => x.t.Id == y.c.Id_Teacher).Select(y => new TeacherCourse()
                    {
                        Id_Course = y.c.Id_Course,
                        NameCourse = y.c.Name,
                        DateBegin = y.c.DateBegin,
                        DateEnd = y.c.DateEnd,
                    }).ToList()
                }).ToListAsync();
            var pagedResult = new PagedResult<TeacherVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };

            return new ApiSuccessResult<PagedResult<TeacherVm>>(pagedResult);

        }

        public async Task<ApiResult<PagedResult<EquipmentOfStudent>>> GetEquipmentOfStudent(int idCourse)
        {
            var query = from e in _context.Equipment
                        join st in _context.Students on e.Id_Student equals st.Id
                        join rc in _context.RegisterCourses on st.Id equals rc.Id_Student
                        where idCourse == rc.Id_Course
                        select new { e, st, rc };

            int totalRow = await query.CountAsync();
            var equipment = await query
                .Select(x => new EquipmentOfStudent()
                {
                    Id_Student=x.st.Id,
                    Name=x.st.FullName,
                    Id_BLE=x.e.Id_BLE
                }).ToListAsync();
            var data = new PagedResult<EquipmentOfStudent>()
            {
                PageIndex = 1,
                PageSize = totalRow,
                Items = equipment,
                TotalRecords = totalRow,
            };
            return new ApiSuccessResult<PagedResult<EquipmentOfStudent>>(data);
        }

        public async Task<ApiResult<TeacherVm>> GetTeacherById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<TeacherVm>("Tài khoản không tồn tại");
            }
            var newStudent = await _context.Students.FindAsync(user.Id);
            var queryCourse = from c in _context.Courses
                              where c.Id_Teacher == id
                              select new { c };
            var dataCourse = await queryCourse.Select(x => new TeacherCourse()
            {
                Id_Course = x.c.Id_Course,
                NameCourse = x.c.Name,
                DateBegin = x.c.DateBegin,
                DateEnd = x.c.DateEnd
            }).ToListAsync();
            var userVm = new TeacherVm()
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                //UrlImg=user.UrlImg,
                TeacherCourses=dataCourse,
            };
            return new ApiSuccessResult<TeacherVm>(userVm);
        }

        public Task<ApiResult<bool>> UpdateTeacher(UpdateTeacherRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
