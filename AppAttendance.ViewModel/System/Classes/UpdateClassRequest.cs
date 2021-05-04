using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.ViewModel.System.Classes
{
    public class UpdateClassRequest
    {
        public int Id_Class { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
