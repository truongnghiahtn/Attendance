using AppAttendance.Application.Schedules;
using AppAttendance.ViewModel.Schedules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppAttendance.BE.Controllers.System
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }


        [HttpGet("PagingByDate")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagingByDate([FromQuery] DateTime date, Guid id_Student)
        {
            var schedule = await _scheduleService.GetByDate(date,id_Student);
            return Ok(schedule);
        }
        [HttpGet("AllPaging")]
        public async Task<IActionResult> GetAll([FromQuery] GetScheduleByTeacherRequest request)
        {
            var schedule = await _scheduleService.GetAllByTeacher(request);
            return Ok(schedule);
        }


        [HttpPost("Create")]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<IActionResult> Create( CreateSchduleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _scheduleService.CreateSchedule(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpPost("CreateAuto")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateAuto(CreateSchduleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _scheduleService.CreateScheduleAuto(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("Update")]

        public async Task<IActionResult> Update(UpdateSchduleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _scheduleService.UpdateSchdule(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _scheduleService.DeleteSchdule(id);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
