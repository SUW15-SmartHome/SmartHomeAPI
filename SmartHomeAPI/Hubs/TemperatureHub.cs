using Microsoft.AspNet.SignalR;
using SmartHomeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHomeAPI.Hubs
{
    public class TemperatureHub : Hub
    {
        public void BroadcastMessage(Temperatures temp)
        {
            Clients.All.recieveMessage(temp);
        }
    }
}