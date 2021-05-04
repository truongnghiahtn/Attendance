using AppAttendance.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.System.Courses
{
    public class GetPagingBySubjectRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public int Id_Subject { get; set; }
    }
}
