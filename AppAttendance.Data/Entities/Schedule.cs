using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.Data.Entities
{
    public  class Schedule
    {
        public int  Id_Schedule { get; set; }
        public int Id_Course { get; set; }
        public int Id_Class { get; set; }

        public DateTime Date { get; set; }

        public int  TimeBegin { get; set; }

        public int TimeEnd { get; set; }
        
        public DateTime DateCreate { get; set; }

        public DateTime DateUpdate { get; set; }

        public Course Course { get; set; }

        public Class Class { get; set; }
        
    }
}
