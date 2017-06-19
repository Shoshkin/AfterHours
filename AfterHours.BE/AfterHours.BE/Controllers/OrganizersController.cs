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
    public class OrganizersController : ApiController
    {
        private EventsContext db = new EventsContext();

        // POST: api/Organizers
        [ResponseType(typeof(Organizer))]
        [HttpPost]
        public async Task<IHttpActionResult> AddOrganizer(Organizer organizer)
        {
            var authResult = UserAuth.IsUserAuth(db, Request);
            if (authResult.Result != UserAuthResult.OK)
            {
                return Unauthorized();
            }
            if(!db.Organizers.Any(o => o.EventId == organizer.EventId && o.UserId == authResult.User.UserId))
            {
                // requester is not an organizer of the event.
                return Unauthorized();
            }
            if(db.Organizers.Any(o => o.EventId == organizer.EventId && o.UserId == organizer.UserId))
            {
                // user already organizer
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Organizers.Add(organizer);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = organizer.OrganizerId }, organizer);
        }

        //// DELETE: api/Organizers/5
        //[ResponseType(typeof(Organizer))]
        //public async Task<IHttpActionResult> DeleteOrganizer(int id)
        //{
        //    Organizer organizer = await db.Organizers.FindAsync(id);
        //    if (organizer == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Organizers.Remove(organizer);
        //    await db.SaveChangesAsync();

        //    return Ok(organizer);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganizerExists(int id)
        {
            return db.Organizers.Count(e => e.OrganizerId == id) > 0;
        }
    }
}