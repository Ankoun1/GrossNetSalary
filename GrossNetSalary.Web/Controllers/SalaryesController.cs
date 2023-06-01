using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL.Salary;
using BL.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Salary;
using static Web.ViewModels.Constants.GlobalConstants.UserConstants;
using static Web.ViewModels.Constants.GlobalConstants.SalaryConstants;

namespace GrossNetSalary.Web.Controllers
{
    public class SalaryesController : Controller
    {
        private readonly ISalaryBL salaryService;
        private readonly IUserBL userService;
        
        public SalaryesController(ISalaryBL salaryService,IUserBL userService)
        {
            this.salaryService = salaryService;           
            this.userService = userService;           
        }

        [Authorize(Roles = RoleAdmin)]
        public IActionResult CreateParameters()
        {
            return View();
        }

        [Authorize(Roles = RoleAdmin)]
        [HttpPost]
        public IActionResult CreateParameters(SalaryParametersFormModel parameters)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (salaryService.InvalidYear())
            {
                ModelState.AddModelError("CustomErrorYear", ErrorMessageIncorectYear);
                return View();
            }
            salaryService.UseParameters(parameters);           

            return RedirectToAction(nameof(CreateParameters));
        }

        [Authorize(Roles = RoleAdmin)]
        public IActionResult Payment()
        {           
            return View();
        }

        [Authorize(Roles = RoleAdmin)]
        [HttpPost]
        public IActionResult Payment(SalaryPaymentFormModel salaryPayment)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var userId = userService.GetUserId(salaryPayment.Email);
            if(userId == null)
            {
                ModelState.AddModelError("CustomErrorEmail", ErrorMessageIncorectEmail);
                return View();
            }

            if (salaryService.ExistingSalary(userId, salaryPayment.Month))
            {
                ModelState.AddModelError("CustomErrorMonth", ErrorMessageExistMonth);
                return View();
            }
            salaryService.SalarySummation(salaryPayment.Email, salaryPayment.GrossSalary,salaryPayment.Month);

            return RedirectToAction(nameof(Payment));
        }

        public IActionResult InputCalculate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InputCalculate(OnlainPaymentFormModel salary)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var netSalary = salaryService.CalculatorOnlain(salary);            

            return RedirectToAction("OutputCalculate", "Salaryes", new { @gross = salary.GrossSalary, @net = netSalary });
        }

        public IActionResult OutputCalculate(decimal gross, decimal net)
        {
            return View(new OnlainPaymentViewModel {Gross = gross,Net = net }); 
        }
    }
}
