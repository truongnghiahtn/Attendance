using AppAttendance.Application.System.Classes;
using AppAttendance.ViewModel.System.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppAttendance.BE.Controllers.System
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet("{id}")]
       
        public async Task<IActionResult> GetById(int id)
        {
            var classes = await _classService.GetClassById(id);
            if (classes.IsSuccessed == false)
            {
                return BadRequest(classes);
            }
            return Ok(classes);
        }

        [HttpGet("AllPaging")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] GetClassPagingRequest request)
        {
            var classes = await _classService.GetAllPaging(request);
            return Ok(classes);
        }
        [HttpGet("All")]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var classes = await _classService.GetAll();
            return Ok(classes);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateClassRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _classService.CreateClass(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("Update")]

        public async Task<IActionResult> Update(UpdateClassRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _classService.UpdateClass(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _classService.DeleteClass(id);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
