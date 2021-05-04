using AppAttendance.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Students
{
    public class GetStudentCource: PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
