using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedTrip.Data.Models
{
    public class Trip
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string StartPoint { get; set; }

        [Required]
        public string EndPoint { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        [Range(2, 6)]
        public int Seats { get; set; }

        [Required]
        [StringLength(80)]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        public ICollection<UserTrip> UserTrips { get; init; } = new HashSet<UserTrip>();
    }
}
