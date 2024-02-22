using System;
using System.Collections.Generic;

namespace Trabea.DataAccess.Models
{
    public partial class Schedule
    {
        public long Id { get; set; }
        public long? PartimeId { get; set; }
        public long? ShiftId { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public bool? IsApproved { get; set; }
        public long? ApprovedBy { get; set; }

        public virtual Employee? ApprovedByNavigation { get; set; }
        public virtual PartTimeEmployee? Partime { get; set; }
        public virtual Shift? Shift { get; set; }
    }
}
