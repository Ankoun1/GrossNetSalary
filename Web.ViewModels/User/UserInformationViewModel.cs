using System;
using System.Collections.Generic;
using Web.ViewModels.Salary;

namespace Web.ViewModels.User
{
    public class UserInformationViewModel
    {
        public int Year { get; init; }

        public decimal TotalSalary { get; init; }

        public List<SalaryListingViewModel> Salaryes { get; set; } = new List<SalaryListingViewModel>();
    }
}
