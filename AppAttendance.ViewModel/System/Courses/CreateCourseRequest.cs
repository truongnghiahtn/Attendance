using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.System.Courses
{
    public class CreateCourseRequest
    {
        public string Name { get; set; }

        public Guid Id_Teacher { get; set; }

        public int Id_Subject { get; set; }

        public DateTime DateBegin { get; set; }
        public string SchoolYear { get; set; }

        public int Semester { get; set; }

    }
}
