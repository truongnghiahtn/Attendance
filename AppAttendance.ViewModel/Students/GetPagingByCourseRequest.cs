using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Students
{
    public class GetPagingByCourseRequest : GetStudentPagingRequest
    {
        public int Id_Course { get; set; }
    }
}
