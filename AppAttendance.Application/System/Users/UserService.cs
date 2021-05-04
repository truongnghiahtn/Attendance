using AppAttendance.Application.Common;
using AppAttendance.Data.EF;
using AppAttendance.Data.Entities;
using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.Students;
using AppAttendance.ViewModel.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppAttendance.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly AppAttendanceDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public UserService(IStorageService storageService, AppAttendanceDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _context = context;
            _storageService = storageService;
        }
        public async Task<ApiResult<ResultLogin>> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(request.UserName);
                if(user==null)
                { return new ApiErrorResult<ResultLogin>("Tài khoản không tồn tại"); }
            }
            var roles = await _userManager.GetRolesAsync(user);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role != "student")
                {
                    checkRole = true;
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded || checkRole == false)
            {
                return new ApiErrorResult<ResultLogin>("Đăng nhập không đúng");
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
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var data = new ResultLogin()
            {
                Id = user.Id,
                UserName=user.UserName,
                FullName=user.FullName,
                UrlImg=user.UrlImg,
                Email = user.Email,
                Type = user.Type,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            };


            return new ApiSuccessResult<ResultLogin>(data);
        }

        public async Task<ApiResult<bool>> Register(RegisterUserRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tài");

            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Email đã tồn tài");
            }
            var role = await _roleManager.FindByIdAsync(request.Type);
            if (role == null||role.Name=="student")
            {
                return new ApiErrorResult<bool>("Loại người dùng không tồn tại");
            }
            user = new AppUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                FullName = request.FullName,
                Type = role.Name,
                //UrlImg = await this.SaveFile(request.ThumbnailImage),
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
                    if (role.Name == "teacher")
                    {
                        var teacher = new Teacher()
                        {
                            Id = newUser.Id,
                            FullName = request.FullName,
                            Email = request.Email,
                            //UrlImg = newUser.UrlImg,
                            DateCreate = DateTime.UtcNow.AddHours(7),
                            DateUpdate = DateTime.UtcNow.AddHours(7)
                        };
                        _context.Teachers.Add(teacher);
                        await _context.SaveChangesAsync();
                    }
                }
                return new ApiSuccessResult<bool>("Đăng ký tài khoản thành công");
            }
            else
            {
                return new ApiErrorResult<bool>("Đăng ký tài khoản không thành công");
            }


        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<ApiResult<PagedResult<UserVm>>> GetUsersPaging(GetUserPagingRequest request)
        {
            // get data
            var query = _userManager.Users;
            //filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword) || x.Email.Contains(request.Keyword) || x.FullName.Contains(request.Keyword));
            }
            var ndata = await query.Where(x => x.Type != "student").ToListAsync();

            //

            // data
            var data = await query.Where(x=>x.Type!="student").Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserVm()
            {
                Id = x.Id,
                UserName = x.UserName,
                FullName = x.FullName,
                Email = x.Email,
                Type = x.Type,
                UrlImg = x.UrlImg
            }).ToListAsync();

            var pagedResult = new PagedResult<UserVm>()
            {
                Items = data,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalRecords = ndata.Count(),
            };

            return new ApiSuccessResult<PagedResult<UserVm>>(pagedResult);

        }

        public async Task<ApiResult<UserVm>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserVm>("Tài khoản không tồn tại");
            }
            var userVm = new UserVm()
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                UrlImg = user.UrlImg,
                Type=user.Type,
                
            };
            return new ApiSuccessResult<UserVm>(userVm);
        }

        public async Task<ApiResult<bool>> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("tài khoản không tồn tại");

            }
            var student = await _context.Students.FindAsync(user.Id);
            var teacher = await _context.Teachers.FindAsync(user.Id);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                if (student != null)
                {
                    _context.Students.Remove(student);
                }
                else
                {
                    if (teacher != null)
                    {
                        _context.Teachers.Remove(teacher);
                    }
                }
                
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>("Xóa thành công");
            }
            return new ApiErrorResult<bool>("Xóa không thành công");
        }
    }
}
