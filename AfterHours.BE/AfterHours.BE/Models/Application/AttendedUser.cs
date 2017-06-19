using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfterHours.BE.Models.Application
{
    public class AttendedUser
    {
        public string Username { get; set; }
        public bool IsOrganizer { get; set; }
    }
}