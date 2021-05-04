using AppAttendance.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.CourseInClasses
{
    public class GetCourseInClassPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
