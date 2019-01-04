using System;
using System.Collections.Generic;

namespace MeniconHelper.Models
{
    public class ListIncident
    {
        public string Reference { get; set; }
        public List<document> Image { get; set; }
        public string Description { get; set; }
        public List<person> Supervisor { get; set; }
        public DateTime Date { get; set; }
        public string Declarant { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Area { get; set; }
        public string Engine { get; set; }

    }
}