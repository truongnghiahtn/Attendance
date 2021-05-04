using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Students
{
    public class StudentRC
    {
        public int Id_Course { get; set; }
        public string NameCourse { get; set; }
        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }
    }
}
