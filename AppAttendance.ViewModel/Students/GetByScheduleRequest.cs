using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Students
{
    public class GetByScheduleRequest
    {
        public int Id_Schedule { get;set; }
        public Guid Id_Student { get; set; }
    }
}
