using SharedTrip.ViewModels.Trips;
using SharedTrip.ViewModels.Users;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static SharedTrip.Data.Constants;

namespace Git.Service
{
    public class ValidatorClass : IValidator
    {
        public ICollection<string> ValidateTrip(AddTripModel model)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(model.EndPoint)
                || string.IsNullOrWhiteSpace(model.StartPoint)
                || string.IsNullOrWhiteSpace(model.DepartureTime.ToString())
                || string.IsNullOrWhiteSpace(model.ImagePath)
                || string.IsNullOrWhiteSpace(model.Description))
            {
                errors.Add("Some field in form is empty.");
            }
            if (model.DepartureTime.ToString() == model.DepartureTime.ToString("dd.MM.yyyy HH: mm"))
            {
                errors.Add("DepartureTime is in invalid format.");
            }

            return errors;
        }

        public ICollection<string> ValidateUser(RegisterUserFormModel model)
        {
            var errors = new List<string>();
            if (model.Username.Length < UserNameMinLength || model.Username.Length > UserNameMaxLength)
            {
                errors.Add($"Username have to be between {UserNameMinLength} and {UserNameMaxLength} charachters long.");
            }
            if (!Regex.IsMatch(model.Email, UserEmailRegex))
            {
                errors.Add("Invalid email.");
            }
            if (model.Password.Length < UserPasswordMinLength || model.Password.Length > UserPasswordMaxLength)
            {
                errors.Add($"Password have to be between {UserPasswordMinLength} and {UserPasswordMaxLength} charachters long.");
            }
            if (model.Password != model.ConfirmPassword)
            {
                errors.Add("Password and Confirm password are diffrent.");
            }
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
            {
                errors.Add("Username or password is empty.");
            }

            return errors;
        }
    }
}
