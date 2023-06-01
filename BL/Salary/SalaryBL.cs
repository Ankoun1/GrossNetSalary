using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;
using DAL.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Web.ViewModels.Salary;

namespace BL.Salary
{
    public class SalaryBL : ISalaryBL
    {
        private ApplicationDbContext dbContext;
        private readonly ICalcualtorNetSalary calculatorNet;       

        public SalaryBL(ApplicationDbContext dbContext, ICalcualtorNetSalary calculatorNet)
        {
            this.dbContext = dbContext;
            this.calculatorNet = calculatorNet;
        }      

        public void UseParameters(SalaryParametersFormModel parameters)
        {
            var salaryParameters = new SalaryParameters
            {
                IncomeTax = parameters.IncomeTax,
                HealthAndSocialInsurance = parameters.HealthAndSocialInsurance,
                MinInsuranceThreshold = parameters.MinInsuranceThreshold,
                MaxInsuranceThreshold = parameters.MaxInsuranceThreshold
            };

            dbContext.SalaryesParameters.Add(salaryParameters);
            dbContext.SaveChanges();           
        }

        public void SalarySummation(string email, decimal grossSalary, int month)
        {
            var userId = dbContext.Users.Where(x => x.Email == email).Select(x => x.Id).FirstOrDefault();

            var salaryParameters = GetSalaryParameters();

            var netSalary = calculatorNet.Calculate(grossSalary, salaryParameters.IncomeTax, salaryParameters.HealthAndSocialInsurance, salaryParameters.MinInsuranceThreshold, salaryParameters.MaxInsuranceThreshold);

            dbContext.UsersSalaryes.Add(new UserSalary { UserId = userId, SalaryParametersId = salaryParameters.Id, MonthId = month, Gross = grossSalary, Net = netSalary });
            dbContext.SaveChanges();
        }

        public decimal CalculatorOnlain(OnlainPaymentFormModel salary)
        {
            var salaryParameters = GetSalaryParameters();

            var netSalary = calculatorNet.Calculate(salary.GrossSalary, salaryParameters.IncomeTax, salaryParameters.HealthAndSocialInsurance, salaryParameters.MinInsuranceThreshold, salaryParameters.MaxInsuranceThreshold);
            return netSalary;
        }

        public void GenerateMonths()
        {
            for (int i = 1; i <= 12; i++)
            {
                dbContext.Months.Add(new Month());
            }
            dbContext.SaveChanges();
        }

        public bool InvalidYear()
            => dbContext.SalaryesParameters.Any(x => x.Year == DateTime.UtcNow.Year);        

        private SalaryParameters GetSalaryParameters()
        {
            return dbContext.SalaryesParameters.Where(x => x.Year == DateTime.UtcNow.Year).FirstOrDefault();
        }

        public bool ExistingSalary(string userId, int month)
         => dbContext.UsersSalaryes.Any(x => x.UserId == userId && x.MonthId == month);        
    }
}