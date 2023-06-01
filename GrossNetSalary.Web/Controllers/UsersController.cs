using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL.Salary;
using BL.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.User;
using static Web.ViewModels.Constants.GlobalConstants.UserConstants;

namespace GrossNetSalary.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserBL userService;
        private readonly ISalaryBL salaryService;     

        public UsersController(IUserBL userService, ISalaryBL salaryService )
        {
            this.userService = userService;
            this.salaryService = salaryService;            
        }

        [Authorize(Roles = RoleAdmin)]
        public IActionResult Create()
        {            
            return View();
        }

        [Authorize(Roles = RoleAdmin)]
        [HttpPost]
        public async Task<IActionResult> Create(UserFormModel user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (userService.EmailVerification(user.Email))
            {
                ModelState.AddModelError("CustomErrorEmail", ErrorMessageExistEmail);
                return View();
            }           
            await userService.CreateUser(user);          
            
            return RedirectToAction(nameof(Create));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserFormModel user)
        {
            if (!ModelState.IsValid)
            {               
                return View();
            }

            if (!userService.EmailVerification(user.Email))
            {
                ModelState.AddModelError("CustomErrorEmail", ErrorMessageIncorectEmail);
                return View();
            }

            if (!await userService.CheckPassword(user.Email,user.Password))
            {
                ModelState.AddModelError("CustomErrorPassword", ErrorMessageIncorectPassword);
                return View();
            }

            if (userService.UserIsDeleted(user.Email))
            {
                return BadRequest();
            }
            if (userService.VerificationAdmin(user))
            {
                salaryService.GenerateMonths();
                await userService.CreateUser(user);
                await userService.CreateRoleAdmin();
            }          

            var result = await userService.SignIn(user.FullName, user.Password);

            if (result.Succeeded && user.FullName != AdminName)
            {
                string id = userService.GetUserId(user.Email);

                return RedirectToAction("Details", "Users", new { @id = id});
            }

            string returnUrl = null;
            returnUrl ??= Url.Content("~/");
            return LocalRedirect(returnUrl);
        }
        
        public IActionResult Logout()
        {
            userService.SignOut();

            string returnUrl = null;
            returnUrl ??= Url.Content("~/");
            return LocalRedirect(returnUrl);
        }

        public IActionResult Find()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Find(FindUserFormModel userModel)
        {
            var user = userService.ExistingUser(userModel.Email);
            if (user == null)
            {
                ModelState.AddModelError("CustomErrorEmail", ErrorMessageIncorectEmail);
                return View();
            }                    

            return RedirectToAction("Update", "Users", new {@id = user.Id, @email = user.Email });
        }

        public IActionResult Update(string email)
        {
            if (userService.UserIsDeleted(email))
            {
                return BadRequest();
            }
            var user = new UserUpdateFormModel { NewEmail = email };
            if (email == AdminEmail)
            {
                user.IsAdmin = true;
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, UserUpdateFormModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            await userService.UpdateUser(id, user);

            return RedirectToAction(nameof(Login));
        }

        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInfo = userService.UserInfo(id);
            if (userInfo == null)
            {
                return BadRequest();
            }

            return View((IEnumerable<UserInformationViewModel>)userInfo);
        }

        public IActionResult Delete(string id)
        {
            if (id == null || !userService.UserExist(id))
            {
                return NotFound();
            }
            userService.DeleteUser(id);

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = RoleAdmin)]
        public IActionResult All()
        {
            var scep = User.Identity.Name;
            var users = userService.GetUsers();
            return View(users);
        }
    }
}
