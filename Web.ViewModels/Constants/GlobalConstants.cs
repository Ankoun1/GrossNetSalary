namespace Web.ViewModels.Constants
{
    public static class GlobalConstants
    {
        public class UserConstants
        {
            public const string FullNameRegex = @" *([A-za-z]{2,}) +([A-za-z]{2,}) *";
            public const string FullNameError = "FullName is invalid. Must contain at least two separate names with letters only.";

            public const byte MaxLengthName = 25;
            public const byte MinLengthName = 6;

            public const byte PasswordMaxLength = 10;
            public const byte PasswordMinLength = 6;           

            public const string RoleAdmin = "Admin";
            public const string AdminName = "Administrator Admin";           
            public const string AdminEmail = "Admin@gmail.com";
            public const string AdminPassword = "123456";

            public const string ErrorMessageIncorectEmail = "Invalid email!";
            public const string ErrorMessageExistEmail = "Invalid registration,existing email!";
            public const string ErrorMessageIncorectPassword = "Invalid login password.";

        }
        public class SalaryConstants
        {
            public const string ErrorMessageIncorectYear = "Parameters for this year are entered!";
            public const string ErrorMessageExistMonth = "Salary is now available this month!";
        }
    }
}
