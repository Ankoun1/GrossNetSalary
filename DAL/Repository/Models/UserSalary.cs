using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Models
{
    public class UserSalary
    {
        public string UserId { get; init; }

        public virtual AplicationUser User { get; init; }

        public int SalaryParametersId { get; init; }

        public virtual SalaryParameters SalaryParameters { get; init; }

        public int MonthId { get; init; }

        public virtual Month Month  { get; init; }

        public decimal Gross { get; set; }

        public decimal Net { get; set; }
    }
}
