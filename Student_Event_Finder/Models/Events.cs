using System.ComponentModel.DataAnnotations;

namespace Student_Event_Finder.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        public string Location { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string Organizer { get; set; }
    }
}