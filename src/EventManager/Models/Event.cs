using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Location { get; set; }
        public string Genre { get; set; }
    }
}
