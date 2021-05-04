using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Students
{
    public class StudentSchedule
    {
        public Guid Id_Student { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Boolean Status { get; set; }
    }
}
