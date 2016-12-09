using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
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
            //app.MapSignalR("/signalr", new HubConfiguration());
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
