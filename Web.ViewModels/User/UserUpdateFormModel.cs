using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Web.ViewModels.Constants.GlobalConstants.UserConstants;

namespace Web.ViewModels.User
{
    public class UserUpdateFormModel
    {
        [Required]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string NewPassword { get; init; }        
        
        [EmailAddress]
        public string NewEmail { get; init; }


        public bool IsAdmin { get; set; }
    }
}
