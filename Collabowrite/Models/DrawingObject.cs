using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Collabowrite.Models
{
    public class DrawingObject
    {
        public int Id { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
        public virtual DrawingSession DrawingSession { get; set; }
        public string Json { get; set; }
    }
}