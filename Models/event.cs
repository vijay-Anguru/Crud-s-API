using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class @event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }

        public string location { get; set; }
        public string Organizer { get; set; }

        public string Description { get; set; }
    }
}