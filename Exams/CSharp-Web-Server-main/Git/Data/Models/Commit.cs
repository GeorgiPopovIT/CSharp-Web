using System;
using System.ComponentModel.DataAnnotations;
using static Git.Common.Constants;


namespace Git.Data.Models
{
    public class Commit
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [Required]
        public string Description { get; init; }
        [Required]
        public DateTime CreatedOn { get; init; }

        public string CreatorId { get; init; }
        public User Creator { get; init; }

        public string RepositoryId { get; init; }
        public Repository Repository { get; init; }

    }
}
