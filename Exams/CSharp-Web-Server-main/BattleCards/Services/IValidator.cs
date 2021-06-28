using BattleCards.ViewModels.Cards;
using BattleCards.ViewModels.Users;
using System.Collections.Generic;

namespace BattleCards.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserModel model);
        ICollection<string> ValidateCard(CreateCardViewModel model);

    }
}
