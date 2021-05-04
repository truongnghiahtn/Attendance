using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Students
{
    public class DayAttendance
    {
        public int id_Schedule { get; set; }
        public DateTime Date { get; set; }
        
        public bool Status { get; set; }
    }
}
