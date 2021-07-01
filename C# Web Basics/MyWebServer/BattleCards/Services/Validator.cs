using BattleCards.ViewModels.Card;
using BattleCards.ViewModels.User;
using System.Collections.Generic;

namespace BattleCards.Services
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

        public ICollection<string> ValidateCardCreation(CreateCardViewModel model)
        {
            var errors = new List<string>();

            if (model.Name.Length < 5 || model.Name.Length > 15)
            {
                errors.Add($"Card name '{model.Name}' must be between 5 and 15 characters long.");
            }

            if (string.IsNullOrWhiteSpace(model.Image))
            {
                errors.Add($"Invalid card image url.");
            }

            if (string.IsNullOrWhiteSpace(model.Keyword))
            {
                errors.Add($"Invalid keyword.");
            }

            if (model.Attack < 0)
            {
                errors.Add($"Attack cannot be negative.");
            }

            if (model.Health < 0)
            {
                errors.Add($"Health cannot be negative.");
            }

            return errors;
        }
    }
}
