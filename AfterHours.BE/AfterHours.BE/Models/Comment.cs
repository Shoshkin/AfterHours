using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AfterHours.BE.Models
{
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime CommentTime { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}