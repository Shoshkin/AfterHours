using Microsoft.Owin.Hosting;
using System;
using System.Web.Http;
using Topshelf;
using Topshelf.ServiceConfigurators;
using TopShelf.Owin;

namespace AfterHours.BE
{
    public class AfterHoursBE
    {
        public void Start()
        {
        }
        public void Stop()
        {
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(c =>
            {
                c.Service<AfterHoursBE>(service =>
                {
                    ServiceConfigurator<AfterHoursBE> s = service;
                    s.ConstructUsing(() => new AfterHoursBE());
                    s.WhenStarted(a => a.Start());
                    s.WhenStopped(a => a.Stop());

                    s.OwinEndpoint(app =>
                    {
                        app.Domain = "localhost";
                        app.Port = 8080;
                        app.ConfigureHttp(httpConfig =>
                        {
                            httpConfig.MapHttpAttributeRoutes();
                        });
                    });
                });
            });
        }

    }
}