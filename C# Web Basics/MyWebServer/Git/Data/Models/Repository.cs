using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Git.Data.Models
{
    public class Repository
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<Commit> Commits { get; init; } = new HashSet<Commit>();
    }
}
