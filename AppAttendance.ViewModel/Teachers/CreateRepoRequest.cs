using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Teachers
{
    public class CreateRepoRequest
    {
        public int Id_Schedule { get; set; }
        public int Id_Course { get; set; }
        public string Id_EquipmentTeacher { get; set; }
    }
}
