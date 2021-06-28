namespace SharedTrip.Data
{
    public class DataConstants
    {
        public const int UserNameMinLength = 5;
        public const int UserNameMaxLength = 20;
        public const int UserPasswordMinLength = 6;
        public const int UserPasswordMaxLength = 20;
        public const string UserEmailRegex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const int TripMinSeats = 2;
        public const int TripMaxSeats = 6;
        public const int TripMaxDescription = 80;

        public const int CommitDescriptionMinLength = 5;
    }
}
