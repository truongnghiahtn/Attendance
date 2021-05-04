using AppAttendance.ViewModel.Students;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.System.Users
{
    public class RegisterUserRequest :RegisterStudentRequest
    {
        public string Type { get; set; }
    }
}
