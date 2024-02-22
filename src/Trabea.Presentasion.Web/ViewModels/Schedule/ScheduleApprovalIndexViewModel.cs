using Microsoft.AspNetCore.Mvc.Rendering;
using Trabea.DataAccess.Models;

namespace Trabea.Presentasion.Web.ViewModels.Schedule {
    public class ScheduleApprovalIndexViewModel {
        public  long Id { get; set; }
        public string Email { get; set; } = null!;
        public string Shift { get; set; } = null!;
        public string PartTime { get; set; } = null!;
        public  bool? IsApprove { get; set; }
    }
}
