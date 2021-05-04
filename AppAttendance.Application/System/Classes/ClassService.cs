using AppAttendance.Data.EF;
using AppAttendance.Data.Entities;
using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.System.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAttendance.Application.System.Classes
{
    public class ClassService : IClassService
    {
        private readonly AppAttendanceDbContext _context;

        public ClassService(AppAttendanceDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<bool>> CreateClass(CreateClassRequest request)
        {
            var classes = new Class()
            {
                Name = request.Name,
                Description = request.Description,
                DateCreate = DateTime.UtcNow.AddHours(7),
                DateUpdate = DateTime.UtcNow.AddHours(7),
            };
            _context.Classes.Add(classes);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>("Tạo thành Lớp học thành công");
        }

        public async Task<ApiResult<bool>> DeleteClass(int id)
        {
            var classes = await _context.Classes.FindAsync(id);
            if (classes == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy ");
            }
            _context.Classes.Remove(classes);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Xóa thành công");
        }

        public async Task<ApiResult<PagedResult<ClassVm>>> GetAll()
        {
            var query = from c in _context.Classes
                        select new { c };
            //filter
            //Paging
            int totalRow = await query.CountAsync();
            var data = await query
                .Select(x => new ClassVm()
                {
                    Id_Class = x.c.Id_Class,
                    Name = x.c.Name,
                    Description = x.c.Description,
                    DateCreate = x.c.DateCreate,
                    DateUpdate = x.c.DateUpdate,
                }).ToListAsync();

            var pagedResult = new PagedResult<ClassVm>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = data
            };

            return new ApiSuccessResult<PagedResult<ClassVm>>(pagedResult);
        }

        public async Task<ApiResult<PagedResult<ClassVm>>> GetAllPaging(GetClassPagingRequest request)
        {
            //lọc dữ liệu
            var query = from c in _context.Classes
                        select new { c };
            //filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.c.Name.Contains(request.Keyword) || x.c.Description.Contains(request.Keyword));
            }

            //Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ClassVm()
                {
                    Id_Class = x.c.Id_Class,
                    Name = x.c.Name,
                    Description = x.c.Description,
                    DateCreate = x.c.DateCreate,
                    DateUpdate = x.c.DateUpdate,
                }).ToListAsync();

            var pagedResult = new PagedResult<ClassVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };

            return new ApiSuccessResult<PagedResult<ClassVm>>(pagedResult);

        }

        public async Task<ApiResult<ClassVm>> GetClassById(int id)
        {
            var classes = await _context.Classes.FindAsync(id);
            if (classes == null)
            {
                return new ApiErrorResult<ClassVm>("Không tìm thấy ");
            }
            var data = new ClassVm()
            {
                Id_Class = classes.Id_Class,
                Name = classes.Name,
                Description = classes.Description,
                DateCreate = classes.DateCreate,
                DateUpdate = classes.DateUpdate,
            };
            return new ApiSuccessResult<ClassVm>(data);
        }

        public async Task<ApiResult<bool>> UpdateClass(UpdateClassRequest request)
        {
            var classes = await _context.Classes.FindAsync(request.Id_Class);
            if (classes == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy ");
            }
            classes.Name = request.Name;
            classes.Description = request.Description;
            classes.DateUpdate = DateTime.UtcNow.AddHours(7);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Cập nhật thành công");
        }
    }
}
