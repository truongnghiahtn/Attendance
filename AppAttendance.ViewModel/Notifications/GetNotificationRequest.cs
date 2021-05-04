using AppAttendance.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Notifications
{
    public class GetNotificationRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public bool Status { get; set; }
    }
}
