using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Students
{
    public class AddEquipmentRequest
    {
        public string Id_BLE { get; set; }
        public string Id_Equipment { get; set; }
        public Guid Id_Student { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
