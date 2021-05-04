using AppAttendance.Application.System.Users;
using AppAttendance.ViewModel.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppAttendance.BE.Controllers.Student
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,teacher")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetById(id);
            return Ok(user);
        }

        [HttpGet("AllPaging")]
        [Authorize(Roles = "admin,teacher")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
        {
            var user = await _userService.GetUsersPaging(request);
            return Ok(user);
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Authencate(request);

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("Register/Account")]
        [Authorize(Roles = "admin")]
        //[Consumes("multipart/form-data")]
        //[AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Register(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        //[AllowAnonymous]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.DeleteUser(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }
    }
}
