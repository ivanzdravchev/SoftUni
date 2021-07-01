using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattleCards.Data.Models
{
    public class Card
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Keyword { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Attack { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Health { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public ICollection<UserCard> UserCards { get; init; } = new HashSet<UserCard>();
    }
}
