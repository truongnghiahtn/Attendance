using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.Data.Entities
{
    public class HistoryAttendance
    {
        public int Id_HistoryAttendace { get; set; }
        public int Id_Course { get; set; }
        public int Id_Schedule { get; set; }
        public string Id_EquipmentTeacher { get; set; }
        public DateTime DateAttendance { get; set; }
        public Course Course { get; set; }
        public List<DetailHA> DetailHAs { get; set; }
    }
}
