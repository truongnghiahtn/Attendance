using AppAttendance.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.System.Courses
{
    public class GetCoursePagingRequest : PagingRequestBase
    {
        public Guid Id_User { get; set; }
        public string Year { get; set; }
        public string Keyword { get; set; }
    }
}
