using AfterHours.BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace AfterHours.BE.Auth
{
    public enum UserAuthResult
    {
        UserNotRegistered,
        PasswordIncorrect,
        OK
    }

    public static class UserAuth
    {
        public static (string username, string password) ParseAuthorizationHeader(HttpRequestMessage request)
        {
            var tmp = request.Headers.Authorization.Parameter.Split(',');
            return (tmp[0], tmp[1]);
        }

        public static UserAuthResult IsUserAuth(EventsContext context, string username, string password)
        {
            User user = context.Users.First(x => x.Username == username);
            if (user == null)
                return UserAuthResult.UserNotRegistered;

            return user.Password == password ? UserAuthResult.OK : UserAuthResult.PasswordIncorrect;

        }

        public static bool IsUserOrganizerOfEvent(EventsContext context, string username, int eventid)
        {
            var current_userid = context.Users.First(x => x.Username == username).UserId;
            return context.Organizers.Any(x => x.EventId == eventid && x.UserId == current_userid);
        }
    }
}