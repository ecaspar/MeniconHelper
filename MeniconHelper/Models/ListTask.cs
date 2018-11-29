using System;

namespace MeniconHelper.Models
{
    public class ListTask
    {
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime CloseDate { get; set; }
        public person PersonComment { get; set; }

    }
}