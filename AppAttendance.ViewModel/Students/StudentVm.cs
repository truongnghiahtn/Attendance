using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.Students
{
    public class StudentVm
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UrlImg { get; set; }
        public bool StatusEquipment { get; set; }
        public List<StudentRC> StudentRCs { get; set; }
        public List<StudentEquipment> StudentEquipment { get; set; }
    }
}
