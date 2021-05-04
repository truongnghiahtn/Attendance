using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Students
{
    public class StudentCourseVm
    {
        public int Id { get; set; }
        public int Id_Cource { get; set; }
        public string NameUser { get; set; }
        public string NameCourse { get; set; }
        public bool Status { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
