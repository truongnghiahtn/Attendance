using AppAttendance.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.System.Subjects
{
    public class GetSubjectPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
