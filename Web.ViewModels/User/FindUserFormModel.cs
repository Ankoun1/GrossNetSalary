using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.User
{
    public class FindUserFormModel
    {        
        [Required]
        [EmailAddress]
        public string Email { get; init; }
    }
}
