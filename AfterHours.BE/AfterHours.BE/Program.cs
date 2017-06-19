using Microsoft.Owin.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Topshelf;
using Topshelf.ServiceConfigurators;
using TopShelf.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin;

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

                    s.OwinEndpoint(appConfigurator =>
                    {
                        appConfigurator.Domain = AfterHoursConfig.HostName;
                        appConfigurator.Port = AfterHoursConfig.Port;
                        appConfigurator.ConfigureHttp(httpConfig =>
                        {
                            httpConfig.MapHttpAttributeRoutes();

                            //var jsonFormatter = httpConfig.Formatters.OfType<JsonMediaTypeFormatter>().First();
                            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        });

                        appConfigurator.ConfigureAppBuilder(app =>
                        {
                            ConfigureOAuth(app);
                        });
                    });
                });
            });
        }

        private static void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
        }
    }
}