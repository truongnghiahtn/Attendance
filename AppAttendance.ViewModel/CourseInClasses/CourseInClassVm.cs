using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.CourseInClasses
{
    public class CourseInClassVm
    {
        public int Id_CourseInClass { get; set; }
        public string NameCourse { get; set; }
        public string NameClass { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
