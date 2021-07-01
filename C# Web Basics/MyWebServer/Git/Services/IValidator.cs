using Git.ViewModels.Repository;
using Git.ViewModels.User;
using System.Collections.Generic;

namespace Git.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUserRegistration(RegisterUserViewModel model);

        ICollection<string> ValidateRepositoryCreation(CreateRepositoryViewModel model);
    }
}
