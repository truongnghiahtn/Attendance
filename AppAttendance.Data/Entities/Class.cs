using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.Data.Entities
{
    public  class Class
    {
        public int Id_Class { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        public List<Schedule> Schedules { get; set; }

    }
}
