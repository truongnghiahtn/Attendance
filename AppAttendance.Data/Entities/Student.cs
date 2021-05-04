using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.Data.Entities
{
    public class Student 
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UrlImg { get; set; }

        public string Email { get; set; }

        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }

        public bool StatusEquipment { get; set; }

        public List<DetailHA> DetailHAs { get; set; }

        public List<Equipment> Equipment { get; set; }

        public List<RegisterCourse> RegisterCourses { get; set; }

        public List<Notification> Notifications { get; set; }

    }
}
