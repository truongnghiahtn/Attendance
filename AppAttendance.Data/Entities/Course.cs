using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.Data.Entities
{
     public class Course
    {
        public int Id_Course { get; set; }

        public string Name { get; set; }

        public Guid Id_Teacher { get; set; }

        public int Id_Subject { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }

        public string SchoolYear { get; set; }

        public int Semester { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime DateUpdate { get; set; }

        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
 
        public List<HistoryAttendance> HistoryAttendances { get; set; }

        public List<RegisterCourse> RegisterCourses { get; set; }

        public List<Schedule> Schedules { get; set; }




    }
}
