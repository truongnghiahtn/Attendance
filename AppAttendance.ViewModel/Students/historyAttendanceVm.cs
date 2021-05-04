using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Students
{
    public class HistoryAttendanceVm
    {
        public string NameUser { get; set; }
        public string NameCourse { get; set; }
        public List<DayAttendance> DayAttendances { get; set; }
    }
}
