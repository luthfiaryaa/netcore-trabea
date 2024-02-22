using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trabea.Presentasion.Web.Services.Interfaces;
using Trabea.Presentasion.Web.ViewModels.Schedule;

namespace Trabea.Presentasion.Web.Controllers {
    [Authorize(Roles = "Administrator, Manager")]
    [Route("employees")]
    public class EmployeeController : Controller {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service) {
            _service = service;
        }

        [HttpGet("")]
        public IActionResult Index() {
            return View();
        }

        [HttpGet("approval")]
        [Authorize(Roles = "Manager")]
        public IActionResult Approval() {
            return View("InsertApprove", new ScheduleApproveViewModel());
        }

        [HttpPost("approval")]
        [Authorize(Roles = "Manager")]
        public IActionResult Approval(ScheduleApproveViewModel vm) {
            if (!ModelState.IsValid) {
                return View("InsertApprove", vm);
            }
            _service.Approval(vm);
            return RedirectToAction("Index", "Schedule");
        }
    }
}
