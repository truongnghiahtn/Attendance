using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.System.Courses
{
    public class GetCourseByStudentRequest: GetCoursePagingRequest
    {
        public Guid Id { get; set; }
    }
}
