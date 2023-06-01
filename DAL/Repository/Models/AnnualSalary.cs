using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Models
{
    public class AnnualSalary
    {
        public int Id { get; init; }

        public int Year { get; init; } = DateTime.UtcNow.Year;

        public decimal Net { get; set; }

        public string UserId { get; init; }

        public virtual AplicationUser User { get; init; }
        
    }
}
