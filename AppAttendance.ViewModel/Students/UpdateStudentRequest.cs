using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Students
{
    public class UpdateStudentRequest
    {
        public Guid Id_User { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }

        public IFormFile ThumbnailImage { get; set; }
    }
}
