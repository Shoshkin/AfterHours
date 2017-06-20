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
using AfterHours.BE.Auth;
using AfterHours.BE.Helpers;

namespace AfterHours.BE.Controllers
{
    public class AttendancesController : ApiController
    {
        private EventsContext db = new EventsContext();

        [ResponseType(typeof(void))]
        [HttpPost]
        public async Task<IHttpActionResult> AddAttendingUser(int eventId)
        {
            AuthResult res = Auth.UserAuth.IsUserAuth(db, Request);
            if (res.Result != UserAuthResult.OK)
                return Unauthorized();

            bool isAlreadyAttended = db.Attendances.Any(x => x.EventId == eventId && x.UserId == res.User.UserId);
            if (!isAlreadyAttended)
            {
                Event eventAttended = db.Events.Find(eventId);
                if(eventAttended.MaxLimit.HasValue && db.Attendances.Count(x=>x.EventId == eventId) >= eventAttended.MaxLimit)
                {
                    return BadRequest("no more space left for this event");
                }

                Attendance attendance = new Attendance { UserId = res.User.UserId, EventId = eventId, IsGoing = true };
                db.Attendances.Add(attendance);

                await db.SaveChangesAsync();

                List<User> currentPeopleInEvent = db.Attendances.Where(x => x.EventId == eventId)
                    .Select(x=>x.User).ToList();

                if(!eventAttended.MinLimit.HasValue || currentPeopleInEvent.Count > eventAttended.MinLimit)
                {
                    EmailSender.SendInvitationEmail(eventAttended.EventName, 
                        new string[] { res.User.Email }, eventAttended.StartTime, eventAttended.EndTime);
                }
                else if(currentPeopleInEvent.Count == eventAttended.MinLimit)
                {
                    EmailSender.SendInvitationEmail(eventAttended.EventName, 
                        currentPeopleInEvent.Select(x=>x.Email), eventAttended.StartTime, eventAttended.EndTime);
                }

                return Ok();
            }
            else
            {
                return BadRequest("user already attending event");
            }
        }

        [ResponseType(typeof(void))]
        [HttpDelete]
        public async Task<IHttpActionResult> RemoveAttendingUser(int eventId)
        {
            AuthResult res = Auth.UserAuth.IsUserAuth(db, Request);
            if (res.Result != UserAuthResult.OK)
                return Unauthorized();

            Attendance attendance = db.Attendances.SingleOrDefault(x => x.UserId == res.User.UserId && x.EventId == eventId);
            if (attendance == null)
            {
                return NotFound();
            }

            attendance.IsGoing = false;
            db.Entry(attendance).State = EntityState.Modified;

            await db.SaveChangesAsync();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}