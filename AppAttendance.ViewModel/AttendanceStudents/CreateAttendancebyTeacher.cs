using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.AttendanceStudents
{
    public class CreateAttendancebyTeacher
    {
        public int Id_Schedule { get; set; }
        public List<DetailHAVm> Content { get; set; }
    }
}
