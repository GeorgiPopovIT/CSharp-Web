using CarShop.ViewModels.Cars;
using CarShop.ViewModels.Users;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static CarShop.Data.Constants;

namespace CarShop.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateCar(CreateCarModel model)
        {
            var errors = new List<string>();
            if (model.Model.Length < CarModelNameMinLength || model.Model.Length > CarModelNameMaxLength)
            {
                errors.Add($"Model have to be between {CarModelNameMinLength} and {CarModelNameMaxLength} charachters long.");
            }
            if (!Regex.IsMatch(model.Model, CarPlateNumRegex))
            {
                errors.Add("Invalid plate number.");
            }
            if (string.IsNullOrWhiteSpace(model.Model) 
                || string.IsNullOrWhiteSpace(model.PlateNumber)
                || string.IsNullOrWhiteSpace(model.Year.ToString()))
            {
                errors.Add("Some field is empty.");
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
            if (model.UserType != UserTypeMechanic && model.UserType != UserTypeClient)
            {
                errors.Add("User type will be Mechanic or client.");
            }

            return errors;
        }
    }
}
