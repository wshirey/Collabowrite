using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Web.Helpers;
using System.Text;
using System.Threading.Tasks;
using Collabowrite.Models;

namespace Collabowrite.Hubs
{
    public class DrawHub : Hub
    {
        public static Dictionary<string, List<string>> objectCache = new Dictionary<string, List<string>>();
        private CollabowriteDbContext db = new CollabowriteDbContext();

        public void ObjectAdded(string sessionId, string json)
        {
            Guid guid;
            if (Guid.TryParse(sessionId, out guid) == false) return;
            DateTime utcNow = DateTime.UtcNow;
            
            var session = db.DrawingSessions.Where(x => x.Guid.Equals(guid)).FirstOrDefault();
            if (session == null)
            {
                session = new DrawingSession()
                {
                    CreateDateTimeUtc = utcNow,
                    Guid = guid
                };
                db.DrawingSessions.Add(session);
            }

            db.DrawingObjects.Add(new DrawingObject()
            {
                CreatedDateTimeUtc = utcNow,
                DrawingSession = session,
                Json = json
            });
            db.SaveChangesAsync();

            Clients.OthersInGroup(sessionId).drawObject(json);
        }

        public async Task LoadObjects(string sessionId)
        {
            await Groups.Add(Context.ConnectionId, sessionId);
            //Clients.Caller.drawObject(Json.Encode(objectCache[sessionId]));
            Guid guid;
            if (Guid.TryParse(sessionId, out guid))
            {
                var session = db.DrawingSessions.Where(x => x.Guid.Equals(guid));
                if (session.Any())
                {
                    var json = session.First().DrawingObjects.Select(x => x.Json);
                    Clients.Caller.drawObject(Json.Encode(json));
                }
            }
        }
    }
}