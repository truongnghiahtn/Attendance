using AppAttendance.Application.Common;
using AppAttendance.Data.EF;
using AppAttendance.Data.Entities;
using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.Students;
using AppAttendance.ViewModel.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace AppAttendance.Application.Students
{
    public class StudentService :IStudentService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly AppAttendanceDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        public StudentService(IStorageService storageService, AppAttendanceDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _context = context;
            _storageService = storageService;
        }
        public async Task<ApiResult<ResultStudentLogin>> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(request.UserName);
                if (user == null)
                { return new ApiErrorResult<ResultStudentLogin>("Tài khoản không tồn tại"); }

            }
            var roles = await _userManager.GetRolesAsync(user);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role == "student")
                {
                    checkRole = true;
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded || checkRole == false)
            {
                return new ApiErrorResult<ResultStudentLogin>("Đăng nhập không đúng");
            }
            var claims = new[]
    {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FullName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            var newStudent = await _context.Students.FindAsync(user.Id);

            var data = new ResultStudentLogin()
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                UrlImg = user.UrlImg,
                Email = user.Email,
                StatusEquipment= newStudent.StatusEquipment,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            };


            return new ApiSuccessResult<ResultStudentLogin>(data);
        }

        public async Task<ApiResult<bool>> Register(RegisterStudentRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");

            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }
            var role = await _roleManager.FindByNameAsync("student");
            user = new AppUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                FullName=request.FullName,
                Type = "student",
                //UrlImg= await this.SaveFile(request.ThumbnailImage),
                DateCreate = DateTime.UtcNow.AddHours(7),
                DateUpdate = DateTime.UtcNow.AddHours(7)

            };
            var resultCreateUser = await _userManager.CreateAsync(user, request.Password);
            var resultRole = await _userManager.AddToRoleAsync(user, role.Name);
            if (resultCreateUser.Succeeded && resultRole.Succeeded)
            {
                var newUser = await _userManager.FindByNameAsync(request.UserName);
                if (newUser != null)
                {
                    var student = new Student()
                    {
                        Id = newUser.Id,
                        FullName = request.FullName,
                        Email = request.Email,
                        //UrlImg = newUser.UrlImg,
                        DateCreate = DateTime.UtcNow.AddHours(7),
                        DateUpdate = DateTime.UtcNow.AddHours(7)
                    };
                    _context.Students.Add(student);
                    await _context.SaveChangesAsync();
                }
                return new ApiSuccessResult<bool>("Đăng ký tài khoản thành công");
            }
            else
            {
                return new ApiErrorResult<bool>("Đăng ký tài khoản không thành công");
            }


        }
        public async Task<ApiResult<StudentVm>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<StudentVm>("Tài khoản không tồn tại");
            }
            var newStudent = await _context.Students.FindAsync(user.Id);
            var queryEquipment = from e in _context.Equipment
                                 where e.Id_Student == user.Id
                                 select new { e };
            var dataEquipment = await queryEquipment.Select(x => new StudentEquipment
            {
                Id_Equipment = x.e.Id_Equipment,
                Name = x.e.Name,
                Description = x.e.Description,
                Id_BLE=x.e.Id_BLE,
                Status=x.e.Status
            }).ToListAsync();
            var queryCourse = from Rc in _context.RegisterCourses
                              join c in _context.Courses on Rc.Id_Course equals c.Id_Course
                              where Rc.Id_Student == id
                              select new { c, Rc };
            var dataCourse = await queryCourse.Select(x => new StudentRC()
            {
                Id_Course = x.c.Id_Course,
                NameCourse = x.c.Name,
                DateBegin = x.c.DateBegin,
                DateEnd = x.c.DateEnd
            }).ToListAsync();
            var userVm = new StudentVm()
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                UrlImg=user.UrlImg,
                StatusEquipment=newStudent.StatusEquipment,
                StudentEquipment=dataEquipment,
                StudentRCs=dataCourse
            };
            return new ApiSuccessResult<StudentVm>(userVm);
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<ApiResult<bool>> RegisterEquipment(AddEquipmentRequest request)
        {
            var user = await _context.Students.FindAsync(request.Id_Student);
            if (user == null)
            {
                return new ApiErrorResult<bool>("tài khoản không tồn tại");

            }
            var equipment = await _context.Equipment.FirstOrDefaultAsync(x => x.Id_Equipment == request.Id_Equipment);
            var userequipment= await _context.Equipment.FirstOrDefaultAsync(x => x.Id_Student == request.Id_Student);
            if (equipment!=null)
            {
                return new ApiErrorResult<bool>("Thiết bị đã tồn tại");
            }
            if(userequipment!=null)
            {
                return new ApiErrorResult<bool>("người dùng đã đăng ký thiết bị");
            }
            user.StatusEquipment = true;
            var newequipment = new Equipment()
            {
                Id_BLE=request.Id_BLE,
                Id_Equipment=request.Id_Equipment,
                Id_Student = user.Id,
                Name = request.Name,
                Status=true,
                Description = request.Description,
                DateCreate = DateTime.UtcNow.AddHours(7),
                DateUpdate = DateTime.UtcNow.AddHours(7),
            };
             _context.Equipment.Add(newequipment);
             await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("thêm thiết bị thành công");
        }

        public async Task<ApiResult<bool>> DeleteStudent(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("tài khoản không tồn tại");

            }
            var Student = await _context.Students.FindAsync(user.Id);

            var result= await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                if (Student.UrlImg != null)
                {
                    string cutImage = user.UrlImg.Substring(14);
                    await _storageService.DeleteFileAsync(cutImage);
                }
                _context.Students.Remove(Student);
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>("Xóa thành công");
            }
            return new ApiErrorResult<bool>("Xóa không thành công");
           
        }

        public async Task<ApiResult<bool>> RegisterCourse(RegisterCourseRequest request)
        {
            if( await _context.Students.FindAsync(request.Id_Student)==null||
                await _context.Courses.FindAsync(request.Id_Course) == null)
            {
                return new ApiErrorResult<bool>("Không kiếm thấy khóa học hoặc sinh viên");
            }
            var register = await _context.RegisterCourses.FirstOrDefaultAsync(x => x.Id_Student == request.Id_Student && x.Id_Course == request.Id_Course);
            if(register!=null)
            {
                return new ApiErrorResult<bool>("đã đăng ký");
            }
            var data = new RegisterCourse()
            {
                Id_Student = request.Id_Student,
                Id_Course = request.Id_Course,
                Status=true,
                DateCreate = DateTime.UtcNow.AddHours(7),
                DateUpdate = DateTime.UtcNow.AddHours(7)
            };
            _context.RegisterCourses.Add(data);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Tạo thành công");
        }

        public async Task<ApiResult<bool>> DeleteRegisterCourse(int id)
        {
            var data = await _context.RegisterCourses.FindAsync(id);
            if (data == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy ");
            }
            _context.RegisterCourses.Remove(data);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Xóa thành công");
        }

        public async Task<ApiResult<PagedResult<StudentVm>>> GetAllPaging(GetStudentPagingRequest request)
        {
            var queryst = from st in _context.Students
                        select new { st};
            var queryEquipment = from e in _context.Equipment
                                 select new { e };
            var queryCourse = from Rc in _context.RegisterCourses
                              join c in _context.Courses on Rc.Id_Course equals c.Id_Course
                              select new { c, Rc };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                queryst = queryst.Where(x => x.st.FullName.Contains(request.Keyword) || x.st.Email.Contains(request.Keyword));
            }

            int totalRow = await queryst.CountAsync();
            var data = await queryst.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new StudentVm()
                {
                    Id=x.st.Id,
                    FullName=x.st.FullName,
                    Email=x.st.Email,
                    UrlImg=x.st.UrlImg,
                    StatusEquipment=x.st.StatusEquipment,
                    StudentEquipment= queryEquipment.Where(y=>x.st.Id==y.e.Id_Student).Select(y=> new StudentEquipment()
                    {
                        Id_BLE=y.e.Id_BLE,
                        Id_Equipment= y.e.Id_Equipment,
                        Name=y.e.Name,
                        Description=y.e.Description,
                        Status=y.e.Status,
                    }).ToList(),
                    StudentRCs=queryCourse.Where(y=>x.st.Id==y.Rc.Id_Student).Select(y=> new StudentRC() { 
                        Id_Course=y.c.Id_Course,
                        NameCourse=y.c.Name,
                        DateBegin=y.c.DateBegin,
                        DateEnd=y.c.DateEnd
                    }).ToList()
                }).ToListAsync();
            var pagedResult = new PagedResult<StudentVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };

            return new ApiSuccessResult<PagedResult<StudentVm>>(pagedResult);



        }

        public async Task<ApiResult<EquipmentOfTeacher>> GetEquipmentTeacher(int id)
        {
            var data = await _context.HistoryAttendances.FindAsync(id);
            if (data == null)
            {
                return new ApiErrorResult<EquipmentOfTeacher>("Không tìm thấy được thiết bị");
            }

            var equipment = new EquipmentOfTeacher()
            {
                Id_HistoryAttendace = data.Id_HistoryAttendace,
                Id_EquipmentTeacher = data.Id_EquipmentTeacher
            };
            return new ApiSuccessResult<EquipmentOfTeacher>(equipment);
        }

        public async Task<ApiResult<PagedResult<StudentSchedule>>> GetPagingBySchedule(int id_Schedule)
        {
            var queryStudentSchedule = from s in _context.Schedules
                                       join c in _context.Courses on s.Id_Course equals c.Id_Course
                                       join rc in _context.RegisterCourses on c.Id_Course equals rc.Id_Course
                                       join st in _context.Students on rc.Id_Student equals st.Id
                                       where s.Id_Schedule== id_Schedule
                                       select new { s, st };
            var dataStudentSchedule = await queryStudentSchedule.Select(x => new StudentSchedule()
            {
                Id_Student = x.st.Id,
                Name = x.st.FullName,
                Date = x.s.Date,
                Status = false
            }).ToListAsync();

            var queryStudentAttdendace = from s in _context.Schedules
                                       join ha in _context.HistoryAttendances on s.Id_Schedule equals ha.Id_Schedule
                                       join d in _context.DetailHAs on ha.Id_HistoryAttendace equals d.Id_HistoryAttendance
                                       join st in _context.Students on d.Id_Student equals st.Id
                                       where s.Id_Schedule==id_Schedule
                                       select new { s, d,st };
            
            var dataStudentAttdendace = await queryStudentAttdendace.Select(x => x.st.Id).ToListAsync();

            var data = new List<StudentSchedule>();
            foreach(var item in dataStudentSchedule)
            {
                int check = dataStudentAttdendace.IndexOf(item.Id_Student);
                if (check == -1)
                {
                    data.Add(item);
                }
                else
                {
                    var user = new StudentSchedule()
                    {
                        Id_Student = item.Id_Student,
                        Name = item.Name,
                        Date = item.Date,
                        Status = true
                    };
                    data.Add(user);
                }
            }
            var totalRow = dataStudentSchedule.Count();
            var pagedResult = new PagedResult<StudentSchedule>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<StudentSchedule>>(pagedResult);

        }

        public async Task<ApiResult<StudentSchedule>> GetBySchedule(GetByScheduleRequest request)
        {
            var student = await _context.Students.FindAsync(request.Id_Student);
            var schedule = await _context.Schedules.FindAsync(request.Id_Schedule);
            if(student==null||schedule==null)
            {
                return new ApiErrorResult<StudentSchedule>("Không tìm thấy dữ liệu");
            }    

            var queryStudentAttdendace = from s in _context.Schedules
                                         join ha in _context.HistoryAttendances on s.Id_Schedule equals ha.Id_Schedule
                                         join d in _context.DetailHAs on ha.Id_HistoryAttendace equals d.Id_HistoryAttendance
                                         join st in _context.Students on d.Id_Student equals st.Id
                                         where s.Id_Schedule == request.Id_Schedule
                                         select new { s, d, st };

            var dataStudentAttdendace = await queryStudentAttdendace.Select(x => x.st.Id).ToListAsync();
            int check = dataStudentAttdendace.IndexOf(request.Id_Student);
            var user = new StudentSchedule();

            if (check > -1)
            {
                 user = new StudentSchedule()
                {
                    Id_Student = student.Id,
                    Name = student.FullName,
                    Date = schedule.Date,
                    Status = true
                };
            }
            else
            {
                 user = new StudentSchedule()
                {
                    Id_Student = student.Id,
                    Name = student.FullName,
                    Date = schedule.Date,
                    Status = false
                };
            }

            return new ApiSuccessResult<StudentSchedule>(user);
        }

        public async Task<ApiResult<PagedResult<StudentAttendanceVm>>> GetPagingByCourse(GetPagingByCourseRequest request)
        {
            var queryStudentAttdendace = from s in _context.Schedules
                                         join ha in _context.HistoryAttendances on s.Id_Schedule equals ha.Id_Schedule
                                         join d in _context.DetailHAs on ha.Id_HistoryAttendace equals d.Id_HistoryAttendance
                                         join st in _context.Students on d.Id_Student equals st.Id
                                         where s.Id_Course == request.Id_Course
                                         select new { s, d, st };
            var queryStudent = from rc in _context.RegisterCourses
                               join s in _context.Students on rc.Id_Student equals s.Id
                               join c in _context.Courses on rc.Id_Course equals c.Id_Course
                               where rc.Id_Course == request.Id_Course
                               select new { rc, s ,c};
            var queryAttendace = from ha in _context.Schedules
                                 where ha.Id_Course == request.Id_Course
                                 select new { ha };
            int countDay = await queryAttendace.CountAsync();

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                queryStudent = queryStudent.Where(x => x.s.FullName.Contains(request.Keyword));
            }
            int totalRow = await queryStudent.CountAsync();
            var data = await queryStudent.Skip((request.PageIndex - 1) * request.PageSize)
               .Take(request.PageSize)
               .Select(x => new StudentAttendanceVm()
               {
                   Id_Student = x.s.Id,
                   Name = x.s.FullName,
                   Email=x.s.Email,
                   NameCource=x.c.Name,
                   NumberDay=countDay,
                   dayAttendances = queryStudentAttdendace.Where(y => y.st.Id == x.s.Id).Select(y => new DayAttendance()
                   {
                       Date = y.s.Date,
                       id_Schedule = y.s.Id_Schedule
                   }).ToList()
               }).ToListAsync();
            var pagedResult = new PagedResult<StudentAttendanceVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<StudentAttendanceVm>>(pagedResult);
        }

        public async Task<ApiResult<bool>> RegisterCourseByUser(RegisterCourseRequest request)
        {
            if (await _context.Students.FindAsync(request.Id_Student) == null ||
              await _context.Courses.FindAsync(request.Id_Course) == null)
            {
                return new ApiErrorResult<bool>("Không kiếm thấy khóa học hoặc sinh viên");
            }
            var register = await _context.RegisterCourses.FirstOrDefaultAsync(x => x.Id_Student == request.Id_Student && x.Id_Course == request.Id_Course);
            if (register != null)
            {
                return new ApiErrorResult<bool>("đã đăng ký");
            }
            var data = new RegisterCourse()
            {
                Id_Student = request.Id_Student,
                Id_Course = request.Id_Course,
                Status = false,
                DateCreate = DateTime.UtcNow.AddHours(7),
                DateUpdate = DateTime.UtcNow.AddHours(7)
            };
            _context.RegisterCourses.Add(data);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Tạo thành công");
        }

        public async Task<ApiResult<bool>> Confirm(ConfirmRcRequest request)
        {
            var registerCourse = await _context.RegisterCourses.FindAsync(request.Id);
            if(registerCourse==null)
            {
                return new ApiErrorResult<bool>("không tìm thấy");
            }
            if(request.Status==true)
            {
                registerCourse.Status = true;
                registerCourse.DateUpdate = DateTime.UtcNow.AddHours(7);
            }
            else
            {
                _context.RegisterCourses.Remove(registerCourse);
            }
            
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Cập nhật thành công");
        }


        public async Task<ApiResult<PagedResult<StudentVm>>> GetByKey(string Keyword)
        {
            var queryst = from st in _context.Students
                          select new { st };
                queryst = queryst.Where(x => x.st.FullName.Contains(Keyword) || x.st.Email.Contains(Keyword));
            int totalRow = await queryst.CountAsync();
            var data = await queryst
                .Select(x => new StudentVm()
                {
                    Id = x.st.Id,
                    FullName = x.st.FullName,
                    Email = x.st.Email,
                    StatusEquipment = x.st.StatusEquipment,
                }).ToListAsync();
            var pagedResult = new PagedResult<StudentVm>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = data
            };

            return new ApiSuccessResult<PagedResult<StudentVm>>(pagedResult);
        }

        public async Task<ApiResult<PagedResult<StudentCourseVm>>> GetPagingStudentCourse(GetStudentCource request)
        {
            var queryStudentCourse = from Rc in _context.RegisterCourses
                              join c in _context.Courses on Rc.Id_Course equals c.Id_Course
                              join st in _context.Students on Rc.Id_Student equals st.Id
                              where Rc.Status == false 
                              select new { c, Rc,st};

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                queryStudentCourse = queryStudentCourse.Where(x => x.st.FullName.Contains(request.Keyword));
            }

            int totalRow = await queryStudentCourse.CountAsync();
            var data = await queryStudentCourse.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new StudentCourseVm()
                {

                    Id=x.Rc.Id_RegisterCourse,
                    Id_Cource=x.c.Id_Course,
                    NameUser=x.st.FullName,
                    NameCourse=x.c.Name,
                    Status=x.Rc.Status,
                    DateBegin=x.Rc.DateCreate,
                    DateUpdate=x.Rc.DateUpdate
                }).ToListAsync();
            var pagedResult = new PagedResult<StudentCourseVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };

            return new ApiSuccessResult<PagedResult<StudentCourseVm>>(pagedResult);
        }

        public async Task<ApiResult<PagedResult<HistoryRcStudentVm>>> GetHistoryRcStudent(GetHistoryRcStudent request)
        {
            var queryStudentCourse = from Rc in _context.RegisterCourses
                                     join c in _context.Courses on Rc.Id_Course equals c.Id_Course
                                     join st in _context.Students on Rc.Id_Student equals st.Id
                                     join t in _context.Teachers on c.Id_Teacher equals t.Id
                                     join s in _context.Subjects on c.Id_Subject equals s.Id_Subject
                                     where Rc.Id_Student == request.Id_User
                                     select new { c, Rc, st,t,s };


            int totalRow = await queryStudentCourse.CountAsync();
            var data = await queryStudentCourse
                .Take(request.PageSize*request.PageIndex)
                .Select(x => new HistoryRcStudentVm()
                {

                    Id_Register=x.Rc.Id_RegisterCourse,
                    Id_Course=x.c.Id_Course,
                    NameCourse=x.c.Name,
                    NameTeacher=x.t.FullName,
                    NameSubject=x.s.Name,
                    DateBegin=x.c.DateBegin,
                    DateEnd=x.c.DateEnd,
                    SchoolYear=x.c.SchoolYear,
                    Semester=x.c.Semester,
                    Status=x.Rc.Status
                }).ToListAsync();
            var pagedResult = new PagedResult<HistoryRcStudentVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };

            return new ApiSuccessResult<PagedResult<HistoryRcStudentVm>>(pagedResult);
        }

        public async Task<ApiResult<bool>> Update(UpdateStudentRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.Id_User.ToString());
            if(user==null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy người dùng");
            }
            if(await _userManager.Users.AnyAsync(x => x.Email == request.email && x.Id != request.Id_User))
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }
            var student = await _context.Students.FindAsync(user.Id);

            if (request.ThumbnailImage != null)
            {
                if (user.UrlImg != null)
                {
                    string cutImage = user.UrlImg.Substring(14);
                    await _storageService.DeleteFileAsync(cutImage);
                }
                user.UrlImg = await this.SaveFile(request.ThumbnailImage);
            }
            user.FullName = request.fullName;
            user.Email = request.email;
            user.DateUpdate = DateTime.UtcNow.AddHours(7);
            student.FullName = request.fullName;
            student.Email = request.email;
            student.DateUpdate = DateTime.UtcNow.AddHours(7);
            student.UrlImg = user.UrlImg;

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>("Cập nhật thành công");
            




        }

        public async Task<ApiResult<HistoryAttendanceVm>> GetHistoryAttendanceVm(historyAttendanceRequest request)
        {
            var queryStudentSchedule = from s in _context.Schedules
                                       join c in _context.Courses on s.Id_Course equals c.Id_Course
                                       join rc in _context.RegisterCourses on c.Id_Course equals rc.Id_Course
                                       join st in _context.Students on rc.Id_Student equals st.Id
                                       join cl in _context.Classes on s.Id_Class equals cl.Id_Class
                                       where s.Id_Course == request.Id_Cource && st.Id == request.Id_User
                                       select new { s, st,c,cl };
            var dataStudentSchedule = await queryStudentSchedule.Select(x => new DayAttendance()
            {
                Date = x.s.Date,
                id_Schedule=x.s.Id_Schedule,
                Status=false
            }).ToListAsync();
            var queryStudentAttdendace = from s in _context.Schedules
                                         join ha in _context.HistoryAttendances on s.Id_Schedule equals ha.Id_Schedule
                                         join d in _context.DetailHAs on ha.Id_HistoryAttendace equals d.Id_HistoryAttendance
                                         join st in _context.Students on d.Id_Student equals st.Id
                                         where s.Id_Course == request.Id_Cource && st.Id == request.Id_User
                                         select new { ha };
            var dataStudentAttdendace = await queryStudentAttdendace.Select(x => x.ha.Id_Schedule).ToListAsync();

            var data = new List<DayAttendance>();
            foreach (var item in dataStudentSchedule)
            {
                int check = dataStudentAttdendace.IndexOf(item.id_Schedule);
                if (check == -1)
                {
                    data.Add(item);
                }
                else
                {
                    var user = new DayAttendance()
                    {
                        Date = item.Date,
                        id_Schedule =item.id_Schedule,
                        Status = true
                    };
                    data.Add(user);
                }
            }

            var users = await _context.Students.FindAsync(request.Id_User);
            var course = await _context.Courses.FindAsync(request.Id_Cource);


            var result = new HistoryAttendanceVm()
            {
                NameUser = users.FullName,
                NameCourse = course.Name,
                DayAttendances = data
            };
            return new ApiSuccessResult<HistoryAttendanceVm>(result);

        }
    }


}
