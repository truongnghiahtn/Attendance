using AppAttendance.Application.System.Subjects;
using AppAttendance.ViewModel.System.Subjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppAttendance.BE.Controllers.System
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }



        [HttpGet("{id}")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var subject = await _subjectService.GetSubjectById(id);
            return Ok(subject);
        }

        [HttpGet("AllPaging")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] GetSubjectPagingRequest request)
        {
            var subject = await _subjectService.GetAllPaging(request);
            return Ok(subject);
        }


        [HttpGet("All")]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var subject = await _subjectService.GetAll();
            return Ok(subject);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateSubjectRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _subjectService.CreateSubject(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("Update")]

        public async Task<IActionResult> Update(UpdateSubjectRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _subjectService.UpdateSubject(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _subjectService.DeleteSubject(id);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
