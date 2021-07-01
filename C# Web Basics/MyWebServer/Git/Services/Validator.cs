using Git.ViewModels.Repository;
using Git.ViewModels.User;
using System.Collections.Generic;

namespace Git.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateUserRegistration(RegisterUserViewModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < 5 || model.Username.Length > 20)
            {
                errors.Add($"Username '{model.Username}' must be between 5 and 20 characters long.");
            }

            if (string.IsNullOrWhiteSpace(model.Email) || !model.Email.Contains("@") || model.Email.Length < 5)
            {
                errors.Add($"Email is not valid.");
            }

            if (model.Password.Length < 6 || model.Password.Length > 20)
            {
                errors.Add($"Password must be between 6 and 20 characters long.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Password does not match.");
            }

            return errors;
        }

        public ICollection<string> ValidateRepositoryCreation(CreateRepositoryViewModel model)
        {
            var errors = new List<string>();

            if (model.Name.Length < 3 || model.Name.Length > 10)
            {
                errors.Add($"Repository name '{model.Name}' must be between 3 and 10 characters long.");
            }

            if (model.RepositoryType != "Public" && model.RepositoryType != "Private")
            {
                errors.Add($"Repository must be either public or private. '{model.RepositoryType}' is invalid.");
            }

            return errors;
        }
    }
}
