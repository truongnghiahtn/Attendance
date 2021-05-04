using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Schedules
{
    public class ScheduleVm
    {
        public int Id_Schedule { get; set; }
        public string NameCourse { get; set; }
        public string NameClass { get; set; }

        public DateTime Date { get; set; }

        public int TimeBegin { get; set; }

        public int TimeEnd { get; set; }
    }
}
