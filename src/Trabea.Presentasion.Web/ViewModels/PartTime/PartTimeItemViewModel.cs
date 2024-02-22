namespace Trabea.Presentasion.Web.ViewModels.PartTime {
    public class PartTimeItemViewModel {
        public long Id;
        public string FullName { get; set; } = null!;
        public string PersonalEmail { get; set; } = null!;
        public string? OfficelEmail { get; set; }
        public string Phone { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public bool? IsActive { get; set; }
    }
}
