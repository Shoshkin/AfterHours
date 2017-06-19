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

namespace AfterHours.BE.Controllers
{
    public class OrganizersController : ApiController
    {
        private EventsContext db = new EventsContext();

        // GET: api/Organizers
        public IQueryable<Organizer> GetOrganizers()
        {
            return db.Organizers;
        }

        // GET: api/Organizers/5
        [ResponseType(typeof(Organizer))]
        public async Task<IHttpActionResult> GetOrganizer(int id)
        {
            Organizer organizer = await db.Organizers.FindAsync(id);
            if (organizer == null)
            {
                return NotFound();
            }

            return Ok(organizer);
        }

        // PUT: api/Organizers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrganizer(int id, Organizer organizer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != organizer.OrganizerId)
            {
                return BadRequest();
            }

            db.Entry(organizer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Organizers
        [ResponseType(typeof(Organizer))]
        public async Task<IHttpActionResult> PostOrganizer(Organizer organizer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Organizers.Add(organizer);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = organizer.OrganizerId }, organizer);
        }

        // DELETE: api/Organizers/5
        [ResponseType(typeof(Organizer))]
        public async Task<IHttpActionResult> DeleteOrganizer(int id)
        {
            Organizer organizer = await db.Organizers.FindAsync(id);
            if (organizer == null)
            {
                return NotFound();
            }

            db.Organizers.Remove(organizer);
            await db.SaveChangesAsync();

            return Ok(organizer);
        }

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