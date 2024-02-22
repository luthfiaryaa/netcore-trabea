

namespace Trabea.Presentasion.Web.ViewModels.Schedule {
    public class ScheduleItemViewModel {
        public DateTime ScheduleDate { get; set; }
        public List<PartTimeViewModel>? Shift1 { get; set; } 
        public List<PartTimeViewModel>? Shift2 { get; set; } 
        public List<PartTimeViewModel>? Shift3 { get; set; } 
        public List<PartTimeViewModel>? Shift4 { get; set; } 
    }
}
