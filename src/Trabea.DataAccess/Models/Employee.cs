using System;
using System.Collections.Generic;

namespace Trabea.DataAccess.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Schedules = new HashSet<Schedule>();
        }

        public long Id { get; set; }
        public string OfficeEmail { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }

        public virtual Account OfficeEmailNavigation { get; set; } = null!;
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
