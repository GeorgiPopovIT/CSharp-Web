using System;

namespace Git.ViewModels.Commits
{
    public class ListingCommitViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
