using Microsoft.AspNetCore.Mvc.Rendering;
using Trabea.Business.Interfaces;
using Trabea.DataAccess.Models;
using Trabea.Presentasion.Web.Services.Interfaces;
using Trabea.Presentasion.Web.ViewModels.PartTime;
using Trabea.Presentasion.Web.ViewModels.Schedule;
using Trabea.Presentation.Web.ViewModels;

namespace Trabea.Presentasion.Web.Services.Implementations {
    public class ScheduleService : IScheduleService {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IShiftRepository _shiftRepository;
        private readonly IPartTimeRepository _partTimeRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ScheduleService(IScheduleRepository scheduleRepository, IShiftRepository shiftRepository, IPartTimeRepository partTimeRepository, IEmployeeRepository employeeRepository) {
            _scheduleRepository = scheduleRepository;
            _shiftRepository = shiftRepository;
            _partTimeRepository = partTimeRepository;
            _employeeRepository = employeeRepository;
        }

        public List<ScheduleApprovalIndexViewModel> GetAllApprove() {
            var scheduleList = _scheduleRepository.GetAllApproval();

            var schedule = scheduleList.Select(s => new ScheduleApprovalIndexViewModel {
                Id = s.Id,
                Email = s.Partime!.PersonalEmail,
                PartTime = $"{s.Partime!.FirstName} {s.Partime.LastName}" ,
                IsApprove = s.IsApproved,
                Shift = $"{s.Shift!.Name} ({s.Shift.StartTime} - {s.Shift.EndTime})"
            }).ToList();

            return schedule;
        }

        public void Insert(ScheduleInsertViewModel vm) {
            var partTime = _partTimeRepository.GetByEmail(vm.PartTimeEmail);
            _scheduleRepository.Save(new Schedule {
                PartimeId = partTime.Id,
                ShiftId = vm.ShiftId,
                ScheduleDate = vm.ScheduleDate,
                IsApproved = vm.IsApprove,
            });
        }

        public List<SelectListItem> GetShiftDropdown() {
            return _shiftRepository.GetAll().Select(s => new SelectListItem {
                Value = s.Id.ToString(),
                Text = $"{s.Name} ({s.StartTime.ToString(@"hh\:mm")} - {s.EndTime.ToString(@"hh\:mm")})",
            }).ToList();
        }

        public List<SelectListItem> GetPartimeDropdown() {
            return _partTimeRepository.GetAll().Select(p => new SelectListItem {
                Value = p.Id.ToString(),
                Text = p.FirstName + " " + p.LastName
            }).ToList();
        }

        public List<SelectListItem> GetEmployeeDropdown() {
            return _employeeRepository.GetAllManager().Select(e => new SelectListItem {
                Value = e.Id.ToString(),
                Text = e.OfficeEmail
            }).ToList();
        }

        public ScheduleIndexViewModel GetAll() {
            DateTime now = DateTime.Now;
            DateTime startDateWeek;

            if (now.DayOfWeek == DayOfWeek.Sunday) {
                startDateWeek = now.AddDays(1).Date;
            } else {
                int diff = (7 + (now.DayOfWeek - DayOfWeek.Monday)) % 7;
                startDateWeek = now.Date.AddDays(-diff);
            }
            var endDateWeek = startDateWeek.AddDays(7);
            var allData = _scheduleRepository.WeekSch(startDateWeek, endDateWeek);
            var data = allData.GroupBy(w => w.ScheduleDate)
                .Select(data => new ScheduleItemViewModel {
                    ScheduleDate = data.Key.Value,
                    Shift1 = data.Where(s => s.ShiftId ==1)
                            .Select(s => new PartTimeViewModel {
                                Id = s.Id,
                                FullName = (s.Partime.FirstName + " " + s.Partime.LastName)
                            }).ToList(),
                    Shift2 = data.Where(s => s.ShiftId == 2)
                            .Select(s => new PartTimeViewModel {
                                Id = s.Id,
                                FullName = s.Partime.FirstName + " " + s.Partime.LastName
                            }).ToList(),
                    Shift3 = data.Where(s => s.ShiftId == 3)
                            .Select(s => new PartTimeViewModel {
                                Id = s.Id,
                                FullName = s.Partime.FirstName + " " + s.Partime.LastName
                            }).ToList(),
                    Shift4 = data.Where(s => s.ShiftId == 4)
                            .Select(s => new PartTimeViewModel {
                                Id = s.Id,
                                FullName = s.Partime.FirstName + " " + s.Partime.LastName
                            }).ToList(),
                }).ToList();
            return new ScheduleIndexViewModel { Schedules = data};
        }
        
    }
}

