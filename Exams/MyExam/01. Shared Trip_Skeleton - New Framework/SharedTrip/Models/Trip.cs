using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static SharedTrip.Data.DataConstants;
namespace SharedTrip.Models
{
    public class Trip
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string StartPoint { get; set; }
        [Required]
        public string EndPoint { get; set; }
        [Required]
        public DateTime DepartureTime { get; set; }
        [Required]
        [MaxLength(TripMaxSeats)]
        public int Seats { get; set; }
        [Required]
        [MaxLength(TripMaxDescription)]
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
