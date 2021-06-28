namespace CarShop.Data
{
    public class Constants
    {
        public const int UserNameMinLength = 4;
        public const int UserNameMaxLength = 20;
        public const int UserPasswordMinLength = 5;
        public const int UserPasswordMaxLength = 20;
        public const string UserEmailRegex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        public const string UserTypeMechanic = "Mechanic";
        public const string UserTypeClient = "Client";

        public const int CarModelNameMinLength = 5;
        public const int CarModelNameMaxLength = 20;
        public const string CarPlateNumRegex = @"[A-Z]{2}[0-9]{4}[A-Z]{2}";

        public const int IssueDescriptionMinLength = 5;
    }
}
