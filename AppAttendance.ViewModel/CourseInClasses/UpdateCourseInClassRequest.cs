using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.CourseInClasses
{
    public class UpdateCourseInClassRequest
    {
        public int Id_CourseInClass { get; set; }
        public int Id_Course { get; set; }
        public int Id_Class { get; set; }
    }
}
