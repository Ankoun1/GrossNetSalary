using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DAL.Repository.Models
{
    public class SalaryParameters
    {
        public SalaryParameters()
        {
            SalarysUsers = new List<UserSalary>();
        }

        public int Id { get; init; }
       
        public int Year { get; init; } = DateTime.UtcNow.Year;       

        public int IncomeTax { get; init; }

        public int HealthAndSocialInsurance { get; init; }

        public int MinInsuranceThreshold { get; init; }

        public int MaxInsuranceThreshold { get; init; }          

        public virtual ICollection<UserSalary> SalarysUsers { get; set; }
    }
}
