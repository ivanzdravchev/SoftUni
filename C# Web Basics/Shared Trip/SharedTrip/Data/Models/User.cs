using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedTrip.Data.Models
{
    public class User
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<UserTrip> UserTrips { get; init; } = new HashSet<UserTrip>();
    }
}
