using CarShop.Models.Car;
using CarShop.Models.Issues;
using CarShop.Models.Users;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CarShop.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateUserRegistration(RegisterUserFormModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < 4 || model.Username.Length > 20)
            {
                errors.Add($"Username '{model.Username}' must be between 4 and 20 characters long.");
            }

            if (model.Password.Length < 5 || model.Password.Length > 20)
            {
                errors.Add($"Password must be between 5 and 20 characters long.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Password does not match.");
            }

            if (model.UserType != "Mechanic" && model.UserType != "Client")
            {
                errors.Add("User must be either a Client or a Mechanic.");
            }

            return errors;
        }

        public ICollection<string> ValidateCarCreation(AddCarFormModel model)
        {
            var errors = new List<string>();

            if (model.Model.Length < 5 || model.Model.Length > 20)
            {
                errors.Add($"Model '{model.Model}' must be between 5 and 20 characters long.");
            }

            if (model.Year < 1900 || model.Year > 2100)
            {
                errors.Add($"Year '{model.Year}' must be between 1900 and 2100.");
            }

            if (!Uri.IsWellFormedUriString(model.Image, UriKind.Absolute))
            {
                errors.Add($"Image '{model.Image}' is not a valid Url.");
            }

            if (!Regex.IsMatch(model.PlateNumber, "[A-Z]{2}[0-9]{4}[A-Z]{2}"))
            {
                errors.Add($"Plate number {model.PlateNumber} is not valid. It should be in format 'AA0000AA'");
            }

            return errors;
        }

        public ICollection<string> ValidateIssueCreation(AddIssueViewModel model)
        {
            var errors = new List<string>();

            if (model.Description.Length < 5)
            {
                errors.Add($"Description must be longer than 5 characters long.");
            }

            return errors;
        }
    }
}
