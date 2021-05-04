using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.Data.Entities
{
    public class Teacher 
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UrlImg { get; set; }
        public string Email { get; set; }

        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        public List<Course> Courses { get; set; }
        //public List<HistoryAttendance> HistoryAttendances { get; set; }

    }
}
