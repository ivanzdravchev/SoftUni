using SharedTrip.ViewModels.Trip;
using SharedTrip.ViewModels.User;
using System.Collections.Generic;

namespace SharedTrip.Services
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
                errors.Add("Email is not valid.");
            }

            if (model.Password.Length < 6 || model.Password.Length > 20)
            {
                errors.Add("Password must be between 6 and 20 characters long.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add("Password does not match.");
            }

            return errors;
        }

        public ICollection<string> ValidateTripCreation(AddTripViewModel model)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(model.StartPoint))
            {
                errors.Add("Invalid start point");
            }

            if (string.IsNullOrWhiteSpace(model.EndPoint))
            {
                errors.Add("Invalid start point");
            }

            if (string.IsNullOrWhiteSpace(model.DepartureTime))
            {
                errors.Add("Invalid departure time");
            }

            if (model.Seats < 2 || model.Seats > 6)
            {
                errors.Add("Seats must be between 2 and 6.");
            }

            if (string.IsNullOrWhiteSpace(model.Description) || model.Description.Length > 80)
            {
                errors.Add("Description is required and can be up to 80 characters long.");
            }

            return errors;
        }
    }
}
