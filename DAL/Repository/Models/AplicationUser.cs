using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DAL.Repository.Models
{
    public class AplicationUser : IdentityUser
    {
        public AplicationUser()
        {
            SalarysUsers = new List<UserSalary>();

            NetSalaryes = new List<AnnualSalary>();
        }

        public bool IsDeleted { get; set; }

        public virtual ICollection<UserSalary> SalarysUsers { get; set; }

        public virtual ICollection<AnnualSalary> NetSalaryes { get; set; }
    }
}
