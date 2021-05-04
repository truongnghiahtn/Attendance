using AppAttendance.Application.System.Roles;
using AppAttendance.Application.Teachers;
using AppAttendance.ViewModel.System.Roles;
using AppAttendance.ViewModel.Teachers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppAttendance.BE.Controllers.System
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet("EquipmentOfsStudent")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetStudentOfEquipment(int idCourse)
        {
            var data = await _teacherService.GetEquipmentOfStudent(idCourse);
            if (data.IsSuccessed == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }
        [HttpGet("AllPaging")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetAllPaging([FromQuery]GetTeacherPagingRequest request)
        {
            var data = await _teacherService.GetAllPaging(request);
            if (data.IsSuccessed == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }
        [HttpGet("All")]
        //[AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var data = await _teacherService.GetAll();
            if (data.IsSuccessed == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }


        [HttpPost("CreateRepo")]
        public async Task<IActionResult> Create(CreateRepoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _teacherService.CreateRepo(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        //[HttpPut("Update")]

        //public async Task<IActionResult> Update(UpdateRoleRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var result = await _roleService.UpdateRole(request);
        //    if (result.IsSuccessed == false)
        //    {
        //        return BadRequest(result);
        //    }
        //    return Ok(result);
        //}

        //[HttpDelete("{id}")]

        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var result = await _roleService.DeleteRole(id);
        //    if (result.IsSuccessed == false)
        //    {
        //        return BadRequest(result);
        //    }
        //    return Ok(result);
        //}
    }
}
