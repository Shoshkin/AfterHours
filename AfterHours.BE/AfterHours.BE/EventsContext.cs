using AfterHours.BE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AfterHours.BE
{
    public class EventsContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}