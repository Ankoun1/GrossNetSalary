namespace BL.Salary
{
    public class CalcualtorNetSalary : ICalcualtorNetSalary
    {
        public decimal Calculate(decimal grossSalary, int incomeTax, int healthAndSocialInsurance, int minInsuranceThreshold, int maxInsuranceThreshold)
        {
            var IncomeTaxPercent = 0.01 * (100 - incomeTax);
            var HealthAndSocialInsurancePercent = 0.01 * (100 - healthAndSocialInsurance);

            decimal deductionIncomeTax = 0;
            decimal deductionHealthAndSocialInsurance = 0;

            if (grossSalary > minInsuranceThreshold)
            {
                var grossSalaryAfterMinThreshold = (grossSalary - minInsuranceThreshold);

                deductionIncomeTax = grossSalaryAfterMinThreshold - (grossSalaryAfterMinThreshold * (decimal)IncomeTaxPercent);

                if (grossSalary <= maxInsuranceThreshold)
                {
                    grossSalaryAfterMinThreshold = (grossSalary - minInsuranceThreshold);
                }
                else
                {
                    grossSalaryAfterMinThreshold = (maxInsuranceThreshold - minInsuranceThreshold);
                }

                deductionHealthAndSocialInsurance = grossSalaryAfterMinThreshold - (grossSalaryAfterMinThreshold * (decimal)HealthAndSocialInsurancePercent);
            }

            var netSalary = grossSalary - deductionIncomeTax - deductionHealthAndSocialInsurance;

            return netSalary;
        }
    }
}
