using AppAttendance.Application.Notifications;
using AppAttendance.Application.System.Classes;
using AppAttendance.ViewModel.Notifications;
using AppAttendance.ViewModel.System.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppAttendance.BE.Controllers.System
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "admin")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("AllPaging")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] GetNotificationRequest request)
        {
            var classes = await _notificationService.GetAllPaging(request);
            return Ok(classes);
        }

        [HttpPost("Create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create(CreateNotificationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _notificationService.Create(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("Update")]

        public async Task<IActionResult> Update(ProsessRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _notificationService.Prosess(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _notificationService.Delete(id);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
