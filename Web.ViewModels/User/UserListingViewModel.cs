using System;
using System.Collections.Generic;

namespace Web.ViewModels.User
{
    public class UserListingViewModel
    {
        public string Id { get; init; }

        public string FullName { get; init; }

        public string Email { get; init; }

        public bool IsDeleted { get; init; }        
    }
}
