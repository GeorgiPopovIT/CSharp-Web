using Git.ViewModels.Repositories;
using Git.ViewModels.Users;
using System.Collections.Generic;


namespace Git.Service
{
    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel model);
        ICollection<string> ValidateRepository(CreateRepositoryViewModel model);
    }
}
