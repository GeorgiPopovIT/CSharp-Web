using Git.ViewModels.Repositories;
using Git.ViewModels.Users;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static Git.Common.Constants;

namespace Git.Service
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateRepository(CreateRepositoryViewModel model)
        {
            var errors = new List<string>();

            if (model.Name.Length < RepoNameMinLength || model.Name.Length > RepoNameMaxLength)
            {
                errors.Add("Repository name is very short ot vey long.");
            }
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                errors.Add("The name is empty;");
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
            if (!Regex.IsMatch(model.Email,UserEmailRegex))
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
