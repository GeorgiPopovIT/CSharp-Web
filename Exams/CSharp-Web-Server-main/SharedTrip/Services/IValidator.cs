using SharedTrip.ViewModels.Trips;
using SharedTrip.ViewModels.Users;
using System.Collections.Generic;


namespace Git.Service
{
    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel model);
        ICollection<string> ValidateTrip(AddTripModel model);
    }
}
