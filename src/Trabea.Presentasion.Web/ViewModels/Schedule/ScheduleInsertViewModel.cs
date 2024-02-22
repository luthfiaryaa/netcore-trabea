using Microsoft.AspNetCore.Mvc.Rendering;
using Trabea.Presentasion.Web.Validations;

namespace Trabea.Presentasion.Web.ViewModels.Schedule {
    public class ScheduleInsertViewModel {
        public long Id { get; set; }
        public string PartTimeEmail { get; set; } = null!;

        [ShiftSelection]
        public long ShiftId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public bool? IsApprove { get; set; }
        public List<SelectListItem>? Shift { get; set; }
    }
}
