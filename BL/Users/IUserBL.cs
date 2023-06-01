using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Web.ViewModels.User;

namespace BL.Users
{
    public interface IUserBL
    {        
        Task<IdentityResult> CreateUser(UserFormModel user);

        Task<IdentityResult> CreateRoleAdmin();

        List<UserInformationViewModel> UserInfo(string userId);

        Task<IdentityResult> UpdateUser(string id,UserUpdateFormModel user);      

        AplicationUser ExistingUser(string email);

        string GetUserId(string email);

        void DeleteUser(string userId);

        List<UserListingViewModel> GetUsers();        

        bool EmailVerification(string email);

        bool VerificationAdmin(UserFormModel userModel);

        Task<bool> CheckPassword(string email, string password);

        bool UserExist(string userId);

        bool UserIsDeleted(string email);       

        Task<SignInResult> SignIn(string email, string password);

        void SignOut();
    }
}
