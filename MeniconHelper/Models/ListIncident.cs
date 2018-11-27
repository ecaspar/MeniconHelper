using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeniconHelper.Models
{
    public class ListIncident
    {
        public string Reference { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Supervisor { get; set; }
        public DateTime Date { get; set; }
        public string Declarant { get; set; }
        public string Type { get; set; }
        public string Area { get; set; }
        public string Engine { get; set; }
    }
}