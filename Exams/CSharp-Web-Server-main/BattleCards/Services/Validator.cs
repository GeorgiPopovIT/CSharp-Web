using BattleCards.ViewModels.Cards;
using BattleCards.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static BattleCards.Common.Constants;

namespace BattleCards.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateCard(CreateCardViewModel model)
        {
            var errors = new List<string>();
            if (model.Name.Length < MinLengthCardName || model.Name.Length > MaxLengthCardName)
            {
                errors.Add($"Card name have to be between {MinLengthCardName} and {MaxLengthCardName} characters.");
            }
            if (model.Description.Length > MaxLengthCardDescription)
            {
                errors.Add($"Card name cannot be longer than {MaxLengthCardDescription} characters.");
            }
            if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Description)
                || string.IsNullOrWhiteSpace(model.Attack.ToString()) || string.IsNullOrWhiteSpace(model.Health.ToString())
               )
            {
                errors.Add("Some field from form is empty;");
            }
            //if (!Uri.IsWellFormedUriString(model.ImageUrl, UriKind.Absolute))
            //{
            //    errors.Add($"Image {model.ImageUrl} is not a valid URL.");
            //}
            return errors;
        }

        public ICollection<string> ValidateUser(RegisterUserModel model)
        {
            var errors = new List<string>();
            if (model.Username.Length < MinLengthUsername || model.Username.Length > MaxLengthUsername)
            {
                errors.Add($"Username have to be between {MinLengthUsername} and {MaxLengthUsername} characters.");
            }
            if (!Regex.IsMatch(model.Email,UserEmailRegex))
            {
                errors.Add("Invalid email.");
            }
            if (model.Password.Length < MinLengthPassword || model.Password.Length > MaxLengthPassword)
            {
                errors.Add($"Password have to be between {MinLengthPassword} and {MaxLengthPassword} characters.");
            }
            if (model.Password != model.ConfirmPassword)
            {
                errors.Add("Password and confirm password are diffrent.");
            }
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Email)||
                string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.ConfirmPassword))
            {
                errors.Add("Some field is empty.");
            }

            return errors;
        }
    }
}
