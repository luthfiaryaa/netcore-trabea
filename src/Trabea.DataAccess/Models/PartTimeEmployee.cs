using System;
using System.Collections.Generic;

namespace Trabea.DataAccess.Models
{
    public partial class PartTimeEmployee
    {
        public PartTimeEmployee()
        {
            Schedules = new HashSet<Schedule>();
        }

        public long Id { get; set; }
        public string PersonalEmail { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? OfficeEmail { get; set; }
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Address { get; set; }
        public string? LastEducation { get; set; }
        public string? CurrentEducation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ResignDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual Account? OfficeEmailNavigation { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
