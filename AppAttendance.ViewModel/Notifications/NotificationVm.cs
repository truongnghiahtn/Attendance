using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Notifications
{
    public class NotificationVm
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public string NameEquipment { get; set; }
        public string NameUser { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
