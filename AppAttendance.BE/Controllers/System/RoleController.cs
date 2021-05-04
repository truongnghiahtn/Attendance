using AppAttendance.Application.System.Roles;
using AppAttendance.ViewModel.System.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppAttendance.BE.Controllers.System
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAll();
            return Ok(roles);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateRoleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _roleService.CreateRole(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("Update")]

        public async Task<IActionResult> Update(UpdateRoleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _roleService.UpdateRole(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _roleService.DeleteRole(id);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
