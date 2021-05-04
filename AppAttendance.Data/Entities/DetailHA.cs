using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.Data.Entities
{
    public class DetailHA
    {
        public int Id_DetailHA { get; set; }
        public Guid Id_Student { get; set; }
        public int Id_HistoryAttendance { get; set; }
        public DateTime DateCreate { get; set; }
        public HistoryAttendance HistoryAttendance { get; set; }
        public Student Student { get; set; }
    }
}
