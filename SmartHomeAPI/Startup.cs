using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;


[assembly: OwinStartup(typeof(SmartHomeAPI.Startup))]
[assembly: OwinStartupAttribute(typeof(SmartHomeAPI.Startup))]

namespace SmartHomeAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    EnableJSONP = true
                };

                map.RunSignalR(hubConfiguration);
            });
            //app.MapSignalR("/signalr", new HubConfiguration());
            //app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
