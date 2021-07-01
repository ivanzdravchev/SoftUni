using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Data.Models
{
    public class Car
    {
        [Key]
        [Required]
        [MaxLength(40)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Model { get; set; }

        public int Year { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        [MaxLength(8)]
        public string PlateNumber { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<Issue> Issues { get; init; } = new List<Issue>();
    }
}
