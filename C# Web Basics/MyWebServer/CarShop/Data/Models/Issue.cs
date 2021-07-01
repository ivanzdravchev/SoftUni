using System;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Data.Models
{
    public class Issue
    {
        [Key]
        [Required]
        [MaxLength(40)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MinLength(5)]
        public string Description { get; set; }

        public bool IsFixed { get; set; }

        [Required]
        public string CarId { get; set; }

        public Car Car { get; set; }
    }
}
