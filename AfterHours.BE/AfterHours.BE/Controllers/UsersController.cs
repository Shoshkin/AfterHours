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
    public class UsersController : ApiController
    {
        private EventsContext db = new EventsContext();

        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostRegister(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostLogin()
        {
            AuthResult authRes = Auth.UserAuth.IsUserAuth(db, Request);
            if (authRes.Result != UserAuthResult.OK)
            {
                return BadRequest(authRes.Result.ToString());
            }

            return Ok();
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}