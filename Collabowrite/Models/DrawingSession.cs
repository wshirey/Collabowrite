using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Collabowrite.Models
{
    public class DrawingSession
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public virtual List<DrawingObject> DrawingObjects { get; set; }
        public DateTime CreateDateTimeUtc { get; set; }
    }
}