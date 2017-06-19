using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AfterHours.BE.Models
{
    public class Organizer
    {
        [Key]
        public int OrganizerId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}