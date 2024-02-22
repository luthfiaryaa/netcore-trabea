using Trabea.Business.Interfaces;
using Trabea.Presentasion.Web.Services.Interfaces;
using Trabea.Presentasion.Web.ViewModels.Schedule;

namespace Trabea.Presentasion.Web.Services.Implementations {
    public class EmployeeService : IEmployeeService {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IScheduleRepository _scheduleRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IScheduleRepository scheduleRepository) {
            _employeeRepository = employeeRepository;
            _scheduleRepository = scheduleRepository;
        }

        public void Approval(ScheduleApproveViewModel vm) {
            var schedule = _scheduleRepository.GetById(vm.Id);
            schedule.ApprovedBy = _employeeRepository.GetByEmail(vm.Email).Id;
            schedule.IsApproved = vm.IsApproved;
            _scheduleRepository.Save(schedule);
        }
    }
}
