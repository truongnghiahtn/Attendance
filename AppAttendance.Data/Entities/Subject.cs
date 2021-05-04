using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.Data.Entities
{
    public class Subject
    {
        public int Id_Subject { get; set; }
        public string Name { get; set; }
        public int NumberOfCredits { get; set; }
        public int Lesson { get; set; }
        public string Description { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }

        public List<Course> Courses { get; set; }



    }
}
