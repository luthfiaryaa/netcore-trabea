using System;
using System.Collections.Generic;

namespace Trabea.DataAccess.Models
{
    public partial class Shift
    {
        public Shift()
        {
            Schedules = new HashSet<Schedule>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
