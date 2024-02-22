using Microsoft.AspNetCore.Mvc.Rendering;
using Trabea.Presentasion.Web.ViewModels.Schedule;

namespace Trabea.Presentasion.Web.Services.Interfaces {
    public interface IScheduleService {

        public List<ScheduleApprovalIndexViewModel> GetAllApprove();
        public ScheduleIndexViewModel GetAll();
        public void Insert(ScheduleInsertViewModel vm);
        public List<SelectListItem> GetShiftDropdown();
        public List<SelectListItem> GetPartimeDropdown();
        public List<SelectListItem> GetEmployeeDropdown();
    }
}
