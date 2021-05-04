using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.System.Courses
{
    public class GetCourseByDayRequest
    {
        public string Keyword { get; set; }
        public Guid Id_User { get; set; }
        public string SchoolYear { get; set; }

        public int Semester { get; set; }
    }
}
