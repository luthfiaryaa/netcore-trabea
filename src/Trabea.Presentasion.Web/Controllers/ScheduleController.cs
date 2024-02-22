using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Trabea.Presentasion.Web.Services.Interfaces;
using Trabea.Presentasion.Web.ViewModels.PartTime;
using Trabea.Presentasion.Web.ViewModels.Schedule;

namespace Trabea.Presentasion.Web.Controllers {
    [Route("schedules")]
    public class ScheduleController : Controller {
        private readonly IScheduleService _service;

        public ScheduleController(IScheduleService service) {
            _service = service;
        }

        [HttpGet("")]
        public IActionResult Index() {
            var vm = _service.GetAll();
            return View(vm);
        }

        [HttpGet("approve")]
        [Authorize(Roles = "Manager")]
        public IActionResult Approve() {
            var vm = _service.GetAllApprove();
            return View("ApproveIndex", vm);
        }

        [HttpGet("insert")]
        [Authorize(Roles = "PartTime")]
        public IActionResult Insert() {
            return View("Upsert", new ScheduleInsertViewModel {
                Shift = _service.GetShiftDropdown()
            });
        }

        [HttpPost("insert")]
        [Authorize(Roles = "PartTime")]
        public IActionResult Insert(ScheduleInsertViewModel vm) {
            if (!ModelState.IsValid) {
                vm.Shift = _service.GetShiftDropdown();
                return View("Upsert", vm);
            }
            _service.Insert(vm);
            return RedirectToAction("Index");
        }
    }
}
