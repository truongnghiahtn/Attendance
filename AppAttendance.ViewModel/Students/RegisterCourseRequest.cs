using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Students
{
    public class RegisterCourseRequest
    {
        public int Id_Course { get; set; }

        public Guid Id_Student { get; set; }
    }
}
