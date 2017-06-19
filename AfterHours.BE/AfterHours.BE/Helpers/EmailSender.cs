using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace AfterHours.BE.Helpers
{
    public static class EmailSender
    {
        public static void SendInvitationEmail(string eventName, IEnumerable<string> to_mails, DateTime start, DateTime end)
        {
            Process.Start("python", $"\"D:\\Git\\AfterHours\\AfterHours.Mail\\afterhour_invitation_sender.py\" \"{string.Join(", ", to_mails)};{eventName};{FormatDate(start.ToUniversalTime())};{FormatDate(end.ToUniversalTime())}\"");
        }

        public static string FormatDate(DateTime datetime)
        {
            return $"{datetime.Year.ToString("D4")}{datetime.Month.ToString("D2")}{datetime.Day.ToString("D2")}T{datetime.Hour.ToString("D2")}{datetime.Minute.ToString("D2")}{datetime.Second.ToString("D2")}Z";
        }
    }
}