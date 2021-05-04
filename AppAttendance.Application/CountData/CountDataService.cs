using AppAttendance.Data.EF;
using AppAttendance.Data.Entities;
using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.CountData;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AppAttendance.Application.CountData
{
    public class CountDataService : ICountDataService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppAttendanceDbContext _context;
        public CountDataService(UserManager<AppUser> userManager, AppAttendanceDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<ApiResult<CountDataVm>> GetCountDataVm()
        {
            var queryCourses = from c in _context.Courses
                        select new { c };
            var queryStudent = from s in _context.Students
                               select new { s };
            var queryTeacher = from t in _context.Teachers
                               select new { t };
            var querySubject = from sb in _context.Subjects
                               select new { sb };

            int countCourse = await queryCourses.CountAsync();
            int countStudent = await queryStudent.CountAsync();
            int countTeacher = await queryTeacher.CountAsync();
            int countSubject = await querySubject.CountAsync();

            var data = new CountDataVm()
            {
                Courses = countCourse,
                Student = countStudent,
                Teacher = countTeacher,
                Subject = countSubject
            };
            return new ApiSuccessResult<CountDataVm>(data);
        }
    }
}
