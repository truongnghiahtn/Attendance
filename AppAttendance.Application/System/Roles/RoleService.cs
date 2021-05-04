using AppAttendance.Data.Entities;
using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.System.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAttendance.Application.System.Roles
{
    public class RoleService : IRoleService
    {

        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<ApiResult<bool>> CreateRole(CreateRoleRequest request)
        {
            var role = await _roleManager.FindByNameAsync(request.Name);
            if (role != null)
            {
                return new ApiErrorResult<bool>("Quyền đã tồn tại");
            }
            role = new AppRole()
            {
                Name = request.Name,
                Description = request.Description,
                DateCreate = DateTime.UtcNow.AddHours(7),
                DateUpdate = DateTime.UtcNow.AddHours(7)
            };
            var result= await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>("Tạo thành công");
            }
            else
            {
                return new ApiErrorResult<bool>("Tạo không thành công");
            }
            
        }

        public async Task<ApiResult<bool>> DeleteRole(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return new ApiErrorResult<bool>("Quyền không tồn tại");
            }
            var result= await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>("Xóa thành công"); 
            }
            else
            {
                return new ApiErrorResult<bool>("Xóa không thành công");
            }
        }

        public async Task<ApiResult<PagedResult<RoleVm>>> GetAll()
        {
            //get dữ liệu
            var query = _roleManager.Roles;
            int totalRow = await query.CountAsync();
            var role = await query
                .Select(x => new RoleVm()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    DateCreate = x.DateCreate,
                    DateUpdate = x.DateUpdate
                }).ToListAsync();
            var pageRole = new PagedResult<RoleVm>()
            {
                PageIndex = 1,
                PageSize = totalRow,
                Items = role,
                TotalRecords = totalRow,
            };
            return new ApiSuccessResult<PagedResult<RoleVm>>(pageRole);

        }

        public async Task<ApiResult<bool>> UpdateRole(UpdateRoleRequest request)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null)
            {
                return new ApiErrorResult<bool>("Quyền không tồn tại");
            }
            role.Name = request.Name;
            role.Description = request.Description;
            role.DateUpdate = DateTime.UtcNow.AddHours(7);

            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>("Cập nhật thành công");
            }
            else
            {
                return new ApiErrorResult<bool>("Cập nhật không thành công");
            }
        }
    }
}
