using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Notifications
{
    public class CreateNotificationRequest
    {
        public string Reason { get; set; }
        public string Id_BLE { get; set; }
        public Guid Id_User { get; set; }
    }
}
