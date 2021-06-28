using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Git.Common.Constants;

namespace Git.Data.Models
{
    public class User
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [Required]
        public string UserName { get; init; }
        [Required]
        public string Email { get; init; }
        [Required]
        public string Password { get; init; }

        public ICollection<Repository> Repositories { get; init; } = new HashSet<Repository>();
        public ICollection<Commit> Commits { get; init; } = new HashSet<Commit>();

    }
}
