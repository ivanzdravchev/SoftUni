using BattleCards.ViewModels.Card;
using BattleCards.ViewModels.User;
using System.Collections.Generic;

namespace BattleCards.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUserRegistration(RegisterUserViewModel model);

        ICollection<string> ValidateCardCreation(CreateCardViewModel model);
    }
}
