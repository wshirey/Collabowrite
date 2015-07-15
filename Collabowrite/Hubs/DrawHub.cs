using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Collabowrite.Hubs
{
    public class DrawHub : Hub
    {
        public void Draw(string json)
        {
            Clients.All.draw(json);
        }
    }
}