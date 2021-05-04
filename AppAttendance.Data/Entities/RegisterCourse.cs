using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.Data.Entities
{
    public class RegisterCourse
    {
        public int Id_RegisterCourse { get; set; }

        public int Id_Course { get; set; }

        public Guid Id_Student { get; set; }

        public bool Status { get; set; }
        public DateTime DateCreate { get; set; }

        public DateTime DateUpdate { get; set; }
        public Course Course { get; set; }

        public Student Student { get; set; }
    }
}

