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

            Attendance attendance = new Attendance { UserId = res.User.UserId, EventId = eventId };
            db.Attendances.Add(attendance);

            await db.SaveChangesAsync();

            return Ok();
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

            db.Attendances.Remove(attendance);
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