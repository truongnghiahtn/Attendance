using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.Data.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public string Id_BLE { get; set; }
        public Guid Id_User { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        public bool Status { get; set; }
        public Student Student { get; set; }
    }
}
