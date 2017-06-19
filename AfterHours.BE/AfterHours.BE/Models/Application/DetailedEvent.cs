using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfterHours.BE.Models.Application
{
    public class DetailedEvent : PreviewEvent
    {
        public string Description { get; set; }
        public List<AttendedUser> Users { get; set; }
        public List<EventComment> Comments { get; set; }
    }
}