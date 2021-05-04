using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Schedules
{
    public class UpdateSchduleRequest: CreateSchduleRequest
    {
        public int Id_Schedule { get; set; }
    }
}
