using EventManager.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Models
{
    public class Event
    {
        public int EventID { get; set; }
        [Required(ErrorMessage = "Date is required!")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Time is required!")]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }
        [Required(ErrorMessage = "Venue name is required!")]
        public string Venue { get; set; }
        public Genre Genre { get; set; }
        public int GenreID { get; set; }
        public bool IsCancelled { get; set; }
        public ApplicationUser Artist { get; set; }
        public List<Attendance> Users { get; set; }
    }
}