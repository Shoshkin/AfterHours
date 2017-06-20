using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfterHours.BE
{
    public class Program
    {
        static void Main()
        {
            string baseAddress = "http://localhost:55049/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Started");
                Console.ReadLine();
            }
        }
    }
}