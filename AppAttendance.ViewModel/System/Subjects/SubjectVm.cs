using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.System.Subjects
{
    public class SubjectVm
    {
        public int Id_Subject { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public int NumberOfCredits { get; set; }
        public int Lesson { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
