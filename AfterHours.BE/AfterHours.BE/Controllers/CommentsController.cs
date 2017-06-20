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
using AfterHours.BE.Models.Application;

namespace AfterHours.BE.Controllers
{
    public class CommentsController : ApiController
    {
        private EventsContext db = new EventsContext();

        // POST: api/Comments
        [ResponseType(typeof(Comment))]
        [HttpPost]
        public async Task<IHttpActionResult> PostAddComment(int eventId, EventComment comment)
        {
            AuthResult res = Auth.UserAuth.IsUserAuth(db, Request);
            if (res.Result != UserAuthResult.OK)
                return Unauthorized();

            Comment dbComment = new Comment { CommentTime = DateTime.Now, EventId = eventId, UserId = res.User.UserId, Content = comment.Content};

            db.Comments.Add(dbComment);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = comment.CommentId }, comment);
        }

        // DELETE: api/Comments/5
        [ResponseType(typeof(Comment))]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteComment(int id)
        {
            AuthResult res = Auth.UserAuth.IsUserAuth(db, Request);
            if (res.Result != UserAuthResult.OK)
                return Unauthorized();
            
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
                return NotFound();

            if (comment.UserId != res.User.UserId)
                return Unauthorized();

            db.Comments.Remove(comment);
            await db.SaveChangesAsync();

            return Ok(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int id)
        {
            return db.Comments.Count(e => e.CommentId == id) > 0;
        }
    }
}