namespace BL.Salary
{
    public interface ICalcualtorNetSalary
    {
        decimal Calculate(decimal grossSalary, int incomeTax, int healthAndSocialInsurance, int minInsuranceThreshold, int maxInsuranceThreshold);
    }
}
