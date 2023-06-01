using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Models
{
    public class Month
    {
        public Month()
        {
            SalarysUsers = new List<UserSalary>();
        }

        [Range(1,12)]
        public int Id { get; init; }      

        public virtual ICollection<UserSalary> SalarysUsers { get; set; }
    }
}
