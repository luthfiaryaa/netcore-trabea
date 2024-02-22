using Trabea.Presentation.Web.ViewModels;

namespace Trabea.Presentasion.Web.ViewModels.PartTime {
    public class PartTimeIndexViewModel {
        public List<PartTimeItemViewModel> PartTimes { get; set; } = null!;
        public PaginationInfoViewModel PaginationInfo { get; set; } = null!;
    }
}
