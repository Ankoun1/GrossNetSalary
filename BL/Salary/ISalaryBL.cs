using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.ViewModels.Salary;

namespace BL.Salary
{
    public interface ISalaryBL
    {
        void UseParameters(SalaryParametersFormModel parameters);        

        void SalarySummation(string email, decimal grossSalary, int month);

        decimal CalculatorOnlain(OnlainPaymentFormModel salary);

        void GenerateMonths();

        bool InvalidYear();

        bool ExistingSalary(string userId, int month);       
    }
}
