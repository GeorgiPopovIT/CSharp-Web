using SharedTrip.ViewModels.Trips;
using SharedTrip.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using static SharedTrip.Data.DataConstants;

namespace SharedTrip.Services
{
    public class Validator : IValidator
    {
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
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password)
                || string.IsNullOrWhiteSpace(model.ConfirmPassword)
                || string.IsNullOrWhiteSpace(model.Email))
            {
                errors.Add("There is an empty field in the form.");
            }

            return errors;
        }
        public ICollection<string> ValidateTrip(AddTripModel model)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(model.EndPoint)
                || string.IsNullOrWhiteSpace(model.StartPoint)
                || string.IsNullOrWhiteSpace(model.DepartureTime.ToString())
                || string.IsNullOrWhiteSpace(model.Description))
            {
                errors.Add("There is an empty field in the form.");
            }
            if (!DateTime.TryParseExact(model.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture,DateTimeStyles.None, out _))
            {
                errors.Add("DepartureTime is in invalid format.");
            }
            if (string.IsNullOrWhiteSpace(model.ImagePath) || !Uri.IsWellFormedUriString(model.ImagePath, UriKind.Absolute))
            {
                errors.Add($"Image '{model.ImagePath}' is not valid. It must be a valid URL.");
            }
            if (model.Description.Length > TripMaxDescription)
            {
                errors.Add("Description is too long.");
            }

            return errors;
        }
    }
}
