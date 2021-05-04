using AppAttendance.Application.AttendanceStudent;
using AppAttendance.ViewModel.AttendanceStudents;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppAttendance.BE.Controllers.System
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceStudentController : ControllerBase
    {
        private readonly IAttendanceStudentService _attendanceService;

        public AttendanceStudentController(IAttendanceStudentService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost("CreatebyTeacher")]
        public async Task<IActionResult> Create(CreateAttendancebyTeacher request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _attendanceService.CreatebyTeacher(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("CreatebyStudent")]
        public async Task<IActionResult> CreatebyStudent(CreateAttendancebyStudent request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _attendanceService.CreatebyStudent(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
