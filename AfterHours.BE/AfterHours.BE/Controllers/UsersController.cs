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
        [HttpPost]
        [Route("api/register")]
        public async Task<IHttpActionResult> Register(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/login")]
        public async Task<IHttpActionResult> Login()
        {
            AuthResult authRes = Auth.UserAuth.IsUserAuth(db, Request);
            if (authRes.Result != UserAuthResult.OK)
            {
                return BadRequest(authRes.Result.ToString());
            }

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

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}