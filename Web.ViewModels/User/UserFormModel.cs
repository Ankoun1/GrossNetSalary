using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static Web.ViewModels.Constants.GlobalConstants.UserConstants;

namespace Web.ViewModels.User
{
    public class UserFormModel
    {
        [Required]
        [StringLength(MaxLengthName, MinimumLength = MinLengthName)]
        [RegularExpression(FullNameRegex, ErrorMessage = FullNameError)]
        public string FullName { get; init; }

        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string Password { get; init; }
    }
}
