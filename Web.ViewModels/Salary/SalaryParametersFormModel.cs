using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Salary
{
    public class SalaryParametersFormModel
    {
        [Range(0,100)]
        public int IncomeTax { get; init; }

        [Range(0,100)]
        public int HealthAndSocialInsurance { get; init; }

        [Range(0,int.MaxValue)]
        public int MinInsuranceThreshold { get; init; }

        [Range(0, int.MaxValue)]
        public int MaxInsuranceThreshold { get; init; }
    }
}
