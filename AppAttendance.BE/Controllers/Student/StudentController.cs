using AppAttendance.Application.Students;
using AppAttendance.ViewModel.Students;
using AppAttendance.ViewModel.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppAttendance.BE.Controllers.Student
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _studentService.GetById(id);
            if (user.IsSuccessed == false)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }
        [HttpGet("AllPaging")]
        public async Task<IActionResult> GetAll([FromQuery] GetStudentPagingRequest request)
        {
            var user = await _studentService.GetAllPaging(request);
            return Ok(user);
        }
        [HttpGet("EquipmentTeacher")]
        public async Task<IActionResult> GetEquipment([FromQuery] int id_Schedule)
        {
            var data = await _studentService.GetEquipmentTeacher(id_Schedule);
            if (data.IsSuccessed == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }
        [HttpGet("HistoryOfStudent")]
        public async Task<IActionResult> GetHistoryOfStudent([FromQuery] GetHistoryRcStudent request)
        {
            var data = await _studentService.GetHistoryRcStudent(request);
            if (data.IsSuccessed == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpGet("PagingBySchedule")]
        public async Task<IActionResult> GetPagingBySchedule([FromQuery] int id_Schedule)
        {
            var data = await _studentService.GetPagingBySchedule(id_Schedule);
            if (data.IsSuccessed == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpGet("GetAttendanceByCourse")]
        public async Task<IActionResult> GetAttendanceByCourse([FromQuery] historyAttendanceRequest request)
        {
            var data = await _studentService.GetHistoryAttendanceVm(request);
            if (data.IsSuccessed == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpGet("BySchedule")]
        public async Task<IActionResult> BySchedule([FromQuery] GetByScheduleRequest request)
        {
            var data = await _studentService.GetBySchedule(request);
            if (data.IsSuccessed == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpGet("PagingbyCourse")]
        public async Task<IActionResult> PagingbyCourse([FromQuery] GetPagingByCourseRequest request)
        {
            var data = await _studentService.GetPagingByCourse(request);
            if (data.IsSuccessed == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpGet("PagingByKeyword")]
        public async Task<IActionResult> PagingbyKeyword([FromQuery] string Keyword)
        {
            var data = await _studentService.GetByKey(Keyword);
            if (data.IsSuccessed == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpGet("PagingStudentCourse")]
        public async Task<IActionResult> PagingStudentCourse([FromQuery] GetStudentCource request)
        {
            var data = await _studentService.GetPagingStudentCourse(request);
            if (data.IsSuccessed == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _studentService.Authencate(request);

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("Register/Account")]
        //[Consumes("multipart/form-data")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterStudentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _studentService.Register(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }


        [HttpPost("Register/Equipment")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterEquipment([FromBody]AddEquipmentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _studentService.RegisterEquipment(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }


        [HttpPost("Register/Course")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCourse([FromBody] RegisterCourseRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _studentService.RegisterCourse(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }

        [HttpPost("Register/CourseByUSer")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCourseByUser([FromBody] RegisterCourseRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _studentService.RegisterCourseByUser(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }


        [HttpPut("Update/Confirm")]
        [AllowAnonymous]
        public async Task<IActionResult> Confirm(ConfirmRcRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _studentService.Confirm(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }

        [HttpPut("Update")]
        [Consumes("multipart/form-data")]
        [AllowAnonymous]
        public async Task<IActionResult> Update([FromForm] UpdateStudentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _studentService.Update(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _studentService.DeleteStudent(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }


        [HttpDelete("RegisterCourse/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteRegisterCourse(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _studentService.DeleteRegisterCourse(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }
    }
}
