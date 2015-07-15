using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Web.Helpers;
using System.Text;
using System.Threading.Tasks;

namespace Collabowrite.Hubs
{
    public class DrawHub : Hub
    {
        public static Dictionary<string, List<string>> objectCache = new Dictionary<string, List<string>>();
        public void ObjectAdded(string sessionId, string json)
        {
            Clients.OthersInGroup(sessionId).drawObject(json);
            if (objectCache.ContainsKey(sessionId))
            {
                objectCache[sessionId].Add(json);
            }
            else
            {
                objectCache.Add(sessionId, new List<string>() { json });
            }
        }

        public async Task LoadObjects(string sessionId)
        {
            await Groups.Add(Context.ConnectionId, sessionId);
            Clients.Caller.drawObject(Json.Encode(objectCache[sessionId]));
        }
    }
}