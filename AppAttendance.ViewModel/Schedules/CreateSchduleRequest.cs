using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Schedules
{
    public class CreateSchduleRequest
    {
        public int Id_Course { get; set; }
        public int Id_Class { get; set; }
        public int TimeBegin { get; set; }

        public int TimeEnd { get; set; }

        public DateTime DateBegin { get; set; }
    }
}
