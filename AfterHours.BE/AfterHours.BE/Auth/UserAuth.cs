using AfterHours.BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace AfterHours.BE.Auth
{
    public class AuthResult
    {
        public UserAuthResult Result { get; set; }
        public User User { get; set; }
    }

    public enum UserAuthResult
    {
        UserNotRegistered,
        PasswordIncorrect,
        OK
    }

    public static class UserAuth
    {
        private static User ParseAuthorizationHeader(HttpRequestMessage request)
        {
            var tmp = request.Headers.Authorization.Parameter.Split(',');
            return new User { Username= tmp[0], Password = tmp[1] };
        }

        public static AuthResult IsUserAuth(EventsContext context, HttpRequestMessage request)
        {
            AuthResult authResult = new AuthResult();
            User currUser = ParseAuthorizationHeader(request);

            User user = context.Users.FirstOrDefault(entry => entry.Username == currUser.Username);
            if (user == null)
            {
                authResult.Result = UserAuthResult.UserNotRegistered;
                return authResult;
            }

            
            if (user.Password == currUser.Username)
            {
                authResult.User = user;
                authResult.Result = UserAuthResult.OK;
            }
            else
            {
                authResult.Result = UserAuthResult.PasswordIncorrect;
            }

            return authResult;
        }

        public static bool IsUserOrganizerOfEvent(EventsContext context, string username, int eventid)
        {
            var current_userid = context.Users.First(x => x.Username == username).UserId;
            return context.Organizers.Any(x => x.EventId == eventid && x.UserId == current_userid);
        }
    }
}