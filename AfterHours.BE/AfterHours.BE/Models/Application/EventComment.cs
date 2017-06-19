using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfterHours.BE.Models.Application
{
    public class EventComment
    {
        public int? CommentId { get; set; }
        public string Username { get; set; }
        public DateTime? Time { get; set; }
        public string Content { get; set; }
    }
}