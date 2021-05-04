using AppAttendance.Data.EF;
using AppAttendance.Data.Entities;
using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.System.Courses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AppAttendance.Application.System.Courses
{
    public class CourseService : ICoursesService
    {
        private readonly AppAttendanceDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CourseService(AppAttendanceDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<ApiResult<bool>> CreateCourse(CreateCourseRequest request)
        {
            if (await _context.Teachers.FindAsync(request.Id_Teacher) == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy Giáo viên  ");
            }
            if (await _context.Subjects.FindAsync(request.Id_Subject) == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy Môn học phù hợp ");
            }
            var subject= await _context.Subjects.FindAsync(request.Id_Subject);
            
            var course = new Course()
            {
                Id_Subject=request.Id_Subject,
                Id_Teacher=request.Id_Teacher,
                Name = request.Name,
                Semester=request.Semester,
                SchoolYear=request.SchoolYear,
                DateBegin=request.DateBegin.Date,
                DateEnd= request.DateBegin.AddDays(subject.Lesson*7).Date,
                DateCreate = DateTime.UtcNow.AddHours(7),
                DateUpdate = DateTime.UtcNow.AddHours(7),
            };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>("Tạo thành công");
        }

        public async Task<ApiResult<bool>> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy ");
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Xóa thành công");
        }
        public async Task<ApiResult<PagedResult<CourseVm>>> GetAllByIdStudent(GetCourseByStudentRequest request)
        {
            var query = from c in _context.Courses
                        join rc in _context.RegisterCourses on c.Id_Course equals rc.Id_Course
                        join t in _context.Teachers on c.Id_Teacher equals t.Id
                        join s in _context.Subjects on c.Id_Subject equals s.Id_Subject
                        where rc.Id_Student==request.Id
                        select new { c, t, s };
            //filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.c.Name.Contains(request.Keyword) || x.s.Name.Contains(request.Keyword));
            }

            //Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CourseVm()
                {
                    Id_Course = x.c.Id_Course,
                    Name = x.c.Name,
                    NameTeacher = x.t.FullName,
                    NameSubject = x.s.Name,
                    DateBegin = x.c.DateBegin,
                    DateEnd = x.c.DateEnd,
                    SchoolYear = x.c.SchoolYear,
                    Semester = x.c.Semester,
                    DateCreate = x.c.DateCreate,
                    DateUpdate = x.c.DateUpdate,
                }).ToListAsync();

            var pagedResult = new PagedResult<CourseVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };

            return new ApiSuccessResult<PagedResult<CourseVm>>(pagedResult);
        }

        public async Task<ApiResult<PagedResult<CourseVm>>> GetAllByIdTeacher(GetCourseByStudentRequest request)
        {
            var query = from c in _context.Courses
                        join t in _context.Teachers on c.Id_Teacher equals t.Id
                        join s in _context.Subjects on c.Id_Subject equals s.Id_Subject
                        where c.Id_Teacher == request.Id
                        select new { c, t, s };
            //filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.c.Name.Contains(request.Keyword) || x.s.Name.Contains(request.Keyword));
            }

            //Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CourseVm()
                {
                    Id_Course = x.c.Id_Course,
                    Name = x.c.Name,
                    NameTeacher = x.t.FullName,
                    NameSubject = x.s.Name,
                    DateBegin = x.c.DateBegin,
                    DateEnd = x.c.DateEnd,
                    SchoolYear=x.c.SchoolYear,
                    Semester=x.c.Semester,
                    DateCreate = x.c.DateCreate,
                    DateUpdate = x.c.DateUpdate,
                }).ToListAsync();

            var pagedResult = new PagedResult<CourseVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };

            return new ApiSuccessResult<PagedResult<CourseVm>>(pagedResult);
        }

        public async Task<ApiResult<PagedResult<CourseVm>>> GetAllByKey(string keyword)
        {
            var query = from c in _context.Courses
                        join t in _context.Teachers on c.Id_Teacher equals t.Id
                        join s in _context.Subjects on c.Id_Subject equals s.Id_Subject
                        select new { c, t, s };
            //filter
                query = query.Where(x => x.c.Name.Contains(keyword));

            //Paging
            int totalRow = await query.CountAsync();
            var data = await query
                .Select(x => new CourseVm()
                {
                    Id_Course = x.c.Id_Course,
                    Name = x.c.Name,
                    NameTeacher = x.t.FullName,
                    NameSubject = x.s.Name,
                    DateBegin = x.c.DateBegin,
                    DateEnd = x.c.DateEnd,
                    Semester = x.c.Semester,
                    SchoolYear = x.c.SchoolYear,
                    DateCreate = x.c.DateCreate,
                    DateUpdate = x.c.DateUpdate,
                }).ToListAsync();

            var pagedResult = new PagedResult<CourseVm>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = data
            };

            return new ApiSuccessResult<PagedResult<CourseVm>>(pagedResult);
        }

        public async Task<ApiResult<PagedResult<CourseVm>>> GetAllBySubject(GetPagingBySubjectRequest request)
        {
            var query = from c in _context.Courses
                        join t in _context.Teachers on c.Id_Teacher equals t.Id
                        join s in _context.Subjects on c.Id_Subject equals s.Id_Subject
                        where s.Id_Subject == request.Id_Subject
                        select new { c, t, s };
            //filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.c.Name.Contains(request.Keyword) || x.t.FullName.Contains(request.Keyword) || x.s.Name.Contains(request.Keyword));
            }

            //Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CourseVm()
                {
                    Id_Course = x.c.Id_Course,
                    Name = x.c.Name,
                    NameTeacher = x.t.FullName,
                    NameSubject = x.s.Name,
                    DateBegin = x.c.DateBegin,
                    DateEnd = x.c.DateEnd,
                    Semester = x.c.Semester,
                    SchoolYear = x.c.SchoolYear,
                    DateCreate = x.c.DateCreate,
                    DateUpdate = x.c.DateUpdate,
                }).ToListAsync();

            var pagedResult = new PagedResult<CourseVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };

            return new ApiSuccessResult<PagedResult<CourseVm>>(pagedResult);
        }

        public async Task<ApiResult<PagedResult<CourseVm>>> GetAllPaging(GetCoursePagingRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.Id_User.ToString());
            var query = from c in _context.Courses
                        join t   in _context.Teachers on c.Id_Teacher equals t.Id
                        join s in _context.Subjects on c.Id_Subject equals s.Id_Subject
                        where c.SchoolYear==request.Year
                        select new { c,t,s };
            if (user.Type == "teacher")
            {
                query = query.Where(x => x.t.Id == request.Id_User);
            }
            //filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.c.Name.Contains(request.Keyword) || x.t.FullName.Contains(request.Keyword)||x.s.Name.Contains(request.Keyword));
            }

            //Paging
            int totalRow = await query.CountAsync();
            var data = await query
                .Take(request.PageSize*request.PageIndex)
                .Select(x => new CourseVm()
                {
                    Id_Course=x.c.Id_Course,
                    Name = x.c.Name,
                    NameTeacher =x.t.FullName,
                    NameSubject=x.s.Name,
                    DateBegin=x.c.DateBegin,
                    DateEnd=x.c.DateEnd,
                    Semester=x.c.Semester,
                    SchoolYear=x.c.SchoolYear,
                    DateCreate = x.c.DateCreate,
                    DateUpdate = x.c.DateUpdate,
                }).ToListAsync();

            var pagedResult = new PagedResult<CourseVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };

            return new ApiSuccessResult<PagedResult<CourseVm>>(pagedResult);
        }

        public async Task<ApiResult<PagedResult<CourseVm>>> GetAllPagingByDay(GetCourseByDayRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.Id_User.ToString());
            
            var query = from c in _context.Courses
                        join t in _context.Teachers on c.Id_Teacher equals t.Id
                        join s in _context.Subjects on c.Id_Subject equals s.Id_Subject
                        where c.SchoolYear==request.SchoolYear && c.Semester==request.Semester
                        select new { c, t, s };
            if (user.Type == "teacher")
            {
                query = query.Where(x => x.t.Id == request.Id_User);
            }
                //filter
                if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.c.Name.Contains(request.Keyword));
            }

            //Paging
            int totalRow = await query.CountAsync();
            var data = await query
                .Select(x => new CourseVm()
                {
                    Id_Course = x.c.Id_Course,
                    Name = x.c.Name,
                    NameTeacher = x.t.FullName,
                    NameSubject = x.s.Name,
                    DateBegin = x.c.DateBegin,
                    DateEnd = x.c.DateEnd,
                    Semester = x.c.Semester,
                    SchoolYear = x.c.SchoolYear,
                    DateCreate = x.c.DateCreate,
                    DateUpdate = x.c.DateUpdate,
                }).ToListAsync();

            var pagedResult = new PagedResult<CourseVm>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = data
            };

            return new ApiSuccessResult<PagedResult<CourseVm>>(pagedResult);
        }

        public async Task<ApiResult<CourseVm>> GetCourseById(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return new ApiErrorResult<CourseVm>("Không tìm thấy ");
            }
            var teacher = await _context.Teachers.FindAsync(course.Id_Teacher);
            var subject = await _context.Subjects.FindAsync(course.Id_Subject);
            var data = new CourseVm()
            {
                Id_Course=course.Id_Course,
                Name = course.Name,
                NameTeacher=teacher.FullName,
                NameSubject=subject.Name,
                DateBegin=course.DateBegin,
                DateEnd=course.DateEnd,
                SchoolYear=course.SchoolYear,
                Semester=course.Semester,
                DateCreate = course.DateCreate,
                DateUpdate = course.DateUpdate,
            };
            return new ApiSuccessResult<CourseVm>(data);
        }

        public async Task<ApiResult<bool>> UpdateCourse(UpdateCourseRequest request)
        {
            var course = await _context.Courses.FindAsync(request.Id_Course);
            if (course == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy ");
            }
            if(await _context.Teachers.FindAsync(request.Id_Teacher)==null||
             await _context.Subjects.FindAsync(request.Id_Subject)==null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy Giáo viên hoạc Môn học phù hợp ");
            }
            var subject = await _context.Subjects.FindAsync(request.Id_Subject);
            course.Name = request.Name;
            course.Id_Subject = request.Id_Subject;
            course.Id_Teacher = request.Id_Teacher;
            course.DateBegin = request.DateBegin;
            course.Semester = request.Semester;
            course.SchoolYear = request.SchoolYear;
            course.DateEnd = request.DateBegin.AddDays(subject.Lesson * 7).Date;
            course.DateUpdate = DateTime.UtcNow.AddHours(7);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Cập nhật thành công");
        }
    }
}
