using AppAttendance.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.System.Classes
{
    public class GetClassPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
