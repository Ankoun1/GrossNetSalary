using System.Security.Claims;
using static Web.ViewModels.Constants.GlobalConstants.UserConstants;

namespace GrossNetSalary.Web.Infrastructure
{ 
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal user)
           => user.IsInRole(RoleAdmin);
    }
}
