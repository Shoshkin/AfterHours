using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AfterHours.BE.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public bool IsGoing { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }
    }
}