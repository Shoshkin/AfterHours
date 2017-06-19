using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfterHours.BE.Models.Application
{
    public class PreviewEvent
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public string Category { get; set; }
        public string Tags { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? MinAttandence { get; set; }
        public int? MaxAttandence { get; set; }
        public int CurrentAttandance { get; set; }
        public bool IsOpen { get; set; }
    }
}