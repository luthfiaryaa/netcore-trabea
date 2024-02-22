namespace Trabea.Presentasion.Web.ViewModels.Schedule {
    public class ScheduleApproveViewModel {
        public long Id { get; set; }
        public string Email { get; set; } = null!;
        public long ApproveBy { get; set; }
        public bool IsApproved { get; set; }
    }
}
