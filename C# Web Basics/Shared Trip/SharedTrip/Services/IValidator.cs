using SharedTrip.ViewModels.Trip;
using SharedTrip.ViewModels.User;
using System.Collections.Generic;

namespace SharedTrip.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUserRegistration(RegisterUserViewModel model);

        ICollection<string> ValidateTripCreation(AddTripViewModel model);
    }
}
