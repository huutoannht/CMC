using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LamNghiep.DTO
{
    public class Event
    {
        public string id { get; set; }

        public string  description { get; set; }
        public string title { get; set; }
        public string date { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string url { get; set; }

        public string color { get; set; }
        public string borderColor { get; set; }
        public string textColor { get; set; }

        public bool allDay { get; set; }

        public string name { get; set; }

        public string location { get; set; }

        public string[] attend { get; set; }
    }
}