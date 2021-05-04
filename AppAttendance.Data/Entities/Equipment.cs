using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.Data.Entities
{
    public class Equipment
    {
        public string Id_BLE { get; set; }
        public Guid Id_Student { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Id_Equipment { get; set; }
        public bool Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }

        public Student Student { get; set; }

    }
}
