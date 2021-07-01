using System;

namespace Git.ViewModels.Repository
{
    public class AllRepositoriesViewModel
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string Owner { get; init; }

        public string CreatedOn { get; init; }

        public int Commits { get; init; }
    }
}
