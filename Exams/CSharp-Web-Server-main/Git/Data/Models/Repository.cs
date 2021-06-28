using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Git.Common.Constants;

namespace Git.Data.Models
{
    public class Repository
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [Required]
        [MaxLength(RepoNameMaxLength)]
        public string Name { get; init; }
        [Required]
        public DateTime CreatedOn { get; init; }
        [Required]
        public bool IsPublic { get; init; }

        public string OwnerId { get; init; }
        public User Owner { get; init; }

        public ICollection<Commit> Commits { get; init; } = new HashSet<Commit>();
    }
}
