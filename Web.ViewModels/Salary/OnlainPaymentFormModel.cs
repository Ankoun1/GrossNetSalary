using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Web.ViewModels.Salary
{
    public class OnlainPaymentFormModel
    {
        [Range(0, 15000)]
        public decimal GrossSalary { get; init; }
    }
}
