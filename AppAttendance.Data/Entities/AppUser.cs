using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.Data.Entities
{
    public  class AppUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public string UrlImg { get; set; }
        public string Type { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
