using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Salary
{
    public class SalaryPaymentFormModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Range(0, 15000)]
        public decimal GrossSalary { get; init; }

        [Range(1, 12)]
        public int Month { get; init; }       
        
    }
}
