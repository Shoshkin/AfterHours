using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AfterHours.BE;
using AfterHours.BE.Models;
using AfterHours.BE.Models.Application;
using AfterHours.BE.Auth;
using AfterHours.BE.Helpers;

namespace AfterHours.BE.Controllers
{
    public class EventsController : ApiController
    {
        private EventsContext db = new EventsContext();

        // GET: api/Events
        public IEnumerable<PreviewEvent> GetEventsPreview()
        {
            foreach (var e in db.Events.Where(e => e.IsOpen))
            {
                yield return new PreviewEvent()
                {
                    EventId = e.EventId,
                    Name = e.EventName,
                    Place = e.Place,
                    Category = e.Category,
                    IsOpen = e.IsOpen,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    Tags = e.Tags,
                    MinAttandence = e.MinLimit,
                    MaxAttandence = e.MaxLimit,
                    CurrentAttandance = GetAttendance(e)
                };
            }
        }

        // GET: api/Events/5
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> GetDetailedEvent(int id)
        {
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            var detailedEvent = new DetailedEvent()
            {
                Name = @event.EventName,
                Place = @event.Place,
                Category = @event.Place,
                Description = @event.Place,
                StartTime = @event.StartTime,
                EndTime = @event.EndTime,
                MinAttandence = @event.MinLimit,
                MaxAttandence = @event.MaxLimit,
                Tags = @event.Tags,
                IsOpen = @event.IsOpen,
                Comments = db.Comments.Where(c => c.EventId == @event.EventId)
                .Select(c => new EventComment() { Content = c.Content, Time = c.CommentTime, Username = c.User.Username })
                .ToList(),
                CurrentAttandance = GetAttendance(@event),
                Users = db.Attendances.Where(a => a.EventId == @event.EventId)
                .AsEnumerable()
                .Select(a => new AttendedUser()
                {
                    Username = a.User.Username,
                    IsOrganizer = IsUserOrganizer(a.EventId, a.UserId)
                })
                .ToList()
            };
            return Ok(detailedEvent);
        }

        private int GetAttendance(Event @event)
        {
            int eventId = @event.EventId;
            //TODO: remove the to list
            return db.Attendances.Count(a => a.EventId == eventId && a.IsGoing);
        }

        private bool IsUserOrganizer(int eventId, int userId)
        {
            return db.Organizers.Any(o => o.EventId == eventId && o.UserId == userId);
        }

        // POST: api/Events
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> PostEvent(Event @event)
        {
            var authResult = UserAuth.IsUserAuth(db, Request);
            if (authResult.Result != UserAuthResult.OK)
            {
                return Unauthorized();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Events.Add(@event);
            db.Attendances.Add(new Attendance { IsGoing = true, UserId = authResult.User.UserId, EventId = @event.EventId });
            db.Organizers.Add(new Organizer { EventId = @event.EventId, UserId = authResult.User.UserId });
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = @event.EventId }, @event);
        }

        // DELETE: api/Events/3
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> DeleteEvent(int eventId)
        {
            var authResult = UserAuth.IsUserAuth(db, Request);
            if (authResult.Result != UserAuthResult.OK)
            {
                return Unauthorized();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (db.Organizers.Any(o => o.EventId == eventId && o.UserId == authResult.User.UserId))
            {
                Event e = await db.Events.FindAsync(eventId);
                e.IsOpen = false;
                await db.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("Not an organizer, can't delete event");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventExists(int id)
        {
            return db.Events.Count(e => e.EventId == id) > 0;
        }
    }
}