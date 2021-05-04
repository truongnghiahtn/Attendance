using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Students
{
    public class HistoryRcStudentVm
    {
        public int Id_Register { get; set; }
        public int Id_Course { get; set; }
        public string NameCourse { get; set; }
        public string NameTeacher { get; set; }
        public string NameSubject { get; set; }
        public bool Status { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }

        public string SchoolYear { get; set; }

        public int Semester { get; set; }
    }
}
