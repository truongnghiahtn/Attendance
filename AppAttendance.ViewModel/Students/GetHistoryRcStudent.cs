using AppAttendance.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Students
{
    public class GetHistoryRcStudent: PagingRequestBase
    {
        public Guid Id_User { get; set; }
    }
}
