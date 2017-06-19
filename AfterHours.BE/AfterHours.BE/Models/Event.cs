using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AfterHours.BE.Models
{
    public class Event
    {
        public string EventName { get; set; }
        public string Category { get; set; }
        public int? MinLimit { get; set; }
        public int? MaxLimit { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }
        [Key]
        public int EventId { get; set; }
        public bool IsOpen { get; set; }
        public string Place { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}