using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Teachers
{
    public class TeacherVm
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UrlImg { get; set; }
        public List<TeacherCourse> TeacherCourses { get; set; }
    }
}
