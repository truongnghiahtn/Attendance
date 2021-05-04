using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Students
{
    public class StudentAttendanceVm
    {
        public Guid Id_Student { get; set; }
        public string Name { get; set; }
        
        public string Email { get; set; }
        public string NameCource { get; set; }
        public int NumberDay { get; set; }
        public List<DayAttendance> dayAttendances { get; set; }
    }
}
