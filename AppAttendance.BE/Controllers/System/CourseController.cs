using AppAttendance.Application.System.Courses;
using AppAttendance.ViewModel.System.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppAttendance.BE.Controllers.System
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICoursesService _courseService;

        public CourseController(ICoursesService courseService)
        {
            _courseService = courseService;
        }



        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _courseService.GetCourseById(id);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("AllPaging")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] GetCoursePagingRequest request)
        {
            var courses = await _courseService.GetAllPaging(request);
            return Ok(courses);
        }


        [HttpGet("AllPagingByKey")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllByKey([FromQuery] string keyword)
        {
            var courses = await _courseService.GetAllByKey(keyword);
            return Ok(courses);
        }

        [HttpGet("AllPagingBySubject")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllBySubject([FromQuery] GetPagingBySubjectRequest request)
        {
            var courses = await _courseService.GetAllBySubject(request);
            return Ok(courses);
        }

        [HttpGet("PagingBySemeter")]
        [AllowAnonymous]
        public async Task<IActionResult> PagingBySemeter([FromQuery] GetCourseByDayRequest request)
        {
            var courses = await _courseService.GetAllPagingByDay(request);
            return Ok(courses);
        }

        [HttpGet("PagingByTeacher")]
        //[Authorize(Roles = "teacher")]
        public async Task<IActionResult> PagingByTeacher([FromQuery] GetCourseByStudentRequest request)
        {
            var courses = await _courseService.GetAllByIdTeacher(request);
            return Ok(courses);
        }

        [HttpGet("PagingByStudent")]
        public async Task<IActionResult> PagingByStudent([FromQuery] GetCourseByStudentRequest request)
        {
            var student = await _courseService.GetAllByIdStudent(request);
            if (student.IsSuccessed == false)
            {
                return BadRequest(student);
            }
            return Ok(student);
        }

        [HttpPost("Create")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(CreateCourseRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _courseService.CreateCourse(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("Update")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Update(UpdateCourseRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _courseService.UpdateCourse(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _courseService.DeleteCourse(id);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
