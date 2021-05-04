using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.System.Courses
{
    public class CourseVm
    {
        public int Id_Course { get; set; }

        public string Name { get; set; }

        public string NameTeacher { get; set; }

        public string NameSubject { get; set; }
        public string SchoolYear { get; set; }

        public int Semester { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime DateUpdate { get; set; }
    }
}
