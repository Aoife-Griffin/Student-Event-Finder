using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Event_Finder.Models
{
    public class Booking
    {

        public int BookingId { get; set; }

        public string StudentName { get; set; }

        public string StudentEmail { get; set; }

        public DateTime BookingDate { get; set; }

        // Foreign Key
        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; }
    }
}