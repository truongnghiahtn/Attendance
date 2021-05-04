using AppAttendance.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Students
{
    public class GetStudentPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
