using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;
using DAL.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Web.ViewModels.Salary;
using Web.ViewModels.User;
using static Web.ViewModels.Constants.GlobalConstants.UserConstants;

namespace BL.Users
{
    public class UserBL : IUserBL
    {
        private readonly UserManager<AplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<AplicationUser> signInManager;
        private readonly ApplicationDbContext dbContext;

        public UserBL
        (
            UserManager<AplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            SignInManager<AplicationUser> signInManager, 
            ApplicationDbContext dbContext
        )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.dbContext = dbContext;
        }

        public async Task<IdentityResult> CreateUser(UserFormModel userModel)
        {
            var user1 = new AplicationUser
            {
                UserName = userModel.FullName,
                Email = userModel.Email
            };
            var result = await userManager.CreateAsync(user1, userModel.Password);
            return result;
        }

        public async Task<IdentityResult> CreateRoleAdmin()
        {
            var result2 = await roleManager.CreateAsync(new IdentityRole { Name = RoleAdmin });
            var user = dbContext.Users.FirstOrDefault();
            return await userManager.AddToRoleAsync(user, RoleAdmin);
        }

        public List<UserInformationViewModel> UserInfo(string userId)
        {
            List<UserInformationViewModel> userInformationModel = new List<UserInformationViewModel>();

            var userSalaryes = dbContext.UsersSalaryes.Where(x => x.UserId == userId).AsEnumerable().GroupBy(x => x.SalaryParametersId).ToList();

            foreach (var userSalary in userSalaryes)
            {
                List<SalaryListingViewModel> salaryModel = new List<SalaryListingViewModel>();

                foreach (var item in userSalary)
                {
                    var salary = new SalaryListingViewModel { Month = item.MonthId, Gross = item.Gross, Net = item.Net };
                    salaryModel.Add(salary);
                }
                var year = dbContext.SalaryesParameters.Where(x => x.Id == userSalary.Select(x => x.SalaryParametersId).FirstOrDefault()).Select(x => x.Year).FirstOrDefault();
                decimal annyalSalary = dbContext.AnnualSalaryes.Where(x => x.UserId == userId && x.Year == year).Select(x => x.Net).FirstOrDefault();

                bool validateAnnyalSalary = year == DateTime.UtcNow.Year && annyalSalary == 0 && userSalary.Any(x => x.MonthId == 12 && x.Net > 0);

                if (validateAnnyalSalary)
                {
                    annyalSalary = userSalary.Sum(x => x.Net);
                }

                userInformationModel.Add(new UserInformationViewModel { Year = year, TotalSalary = annyalSalary, Salaryes = salaryModel });

                if (annyalSalary != 0)
                {
                    dbContext.AnnualSalaryes.Add(new AnnualSalary { Year = year, Net = annyalSalary, UserId = userId });
                    dbContext.SaveChanges();
                }
            }
            return userInformationModel;
        }

        public async Task<IdentityResult> UpdateUser(string id, UserUpdateFormModel user)
        {
            var userDb = await userManager.FindByIdAsync(id);
            var resultRemoved = await userManager.RemovePasswordAsync(userDb);

            if (resultRemoved.Succeeded)
            {
                var result = await userManager.AddPasswordAsync(userDb, user.NewPassword);

                if (result.Succeeded && user.NewEmail != null && userDb.Email != AdminEmail)
                {
                    userDb.Email = user.NewEmail;
                }
            }

            return await userManager.UpdateAsync(userDb);
        }

        public AplicationUser ExistingUser(string email)
        {
            return userManager.FindByEmailAsync(email).Result;
        }

        public string GetUserId(string email)
        {
            var userId = dbContext.Users.Where(x => x.Email == email).Select(x => x.Id).FirstOrDefault();

            return userId;
        }

        public void DeleteUser(string userId)
        {
            var userDb = dbContext.Users.Find(userId);
            userDb.IsDeleted = true;
            dbContext.SaveChanges();
        }

        public List<UserListingViewModel> GetUsers()
        {
            var users = dbContext.Users.Where(x => x.Email != AdminEmail).Select(x => new { Id = x.Id, Username = x.UserName, Email = x.Email,IsDeleted = x.IsDeleted }).OrderBy(x => x.Username).ToList();

            var usersModel = new List<UserListingViewModel>();

            foreach (var user in users)
            {
                var userModel = new UserListingViewModel { Id = user.Id, FullName = user.Username, Email = user.Email,IsDeleted = user.IsDeleted };

                usersModel.Add(userModel);
            }
            return usersModel;
        }

        public bool EmailVerification(string email)
        {
            return dbContext.Users.Any(x => x.Email == email);
        }

        public bool VerificationAdmin(UserFormModel userModel)
            => userModel.FullName == AdminName 
            && userModel.Password == AdminPassword 
            && userModel.Email == AdminEmail 
            && !EmailVerification(userModel.Email);

        public async Task<bool> CheckPassword(string email, string password)
        {
            var user = ExistingUser(email);
            return await userManager.CheckPasswordAsync(user, password);
        }

        public bool UserExist(string userId)
         => dbContext.Users.Any(x => x.Id == userId);

        public bool UserIsDeleted(string email)
       => dbContext.Users.Any(x => x.Email == email && x.Email != AdminEmail && x.IsDeleted);

        public async Task<SignInResult> SignIn(string email, string password)
        {
            var result = await signInManager.PasswordSignInAsync(email, password, false, false);
           
            return result;
        }

        public async void SignOut()
        {
           await signInManager.SignOutAsync();
        }
    }
}
