using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trabea.Presentasion.Web.Services.Interfaces;
using Trabea.Presentasion.Web.ViewModels.Account;
using Trabea.Presentasion.Web.ViewModels.PartTime;

namespace Trabea.Presentasion.Web.Controllers {
    [Route("parttime")]
    [Authorize(Roles = "Administrator,, Manager")]
    public class PartTimeController : Controller {
        private readonly IPartTimeService _service;

        public PartTimeController(IPartTimeService service) {
            _service = service;
        }

        [HttpGet("")]
        public IActionResult Index(int pageNumber = 1, int pageSize = 10) {
            var vm = _service.GetAll(pageNumber, pageSize);
            return View(vm);
        }

        [HttpGet("insert")]
        public IActionResult Insert() {
            return View("Upsert", new PartTimeUpsertViewModel());
        }

        [HttpPost("insert")]
        public IActionResult Insert(PartTimeUpsertViewModel vm) {
            if (!ModelState.IsValid) {
                return View("Upsert", vm);
            }
            _service.Insert(vm);
            return RedirectToAction("Index");
        }

        [HttpGet("update/{id}")]
        public IActionResult UpdateForm(long id) {
            var vm = _service.GetById(id);
            return View("Upsert", vm);
        }

        [HttpPost("update/{id}")]
        public IActionResult Update(long id, PartTimeUpsertViewModel vm) {
            _service.Update(id, vm);
            return RedirectToAction("Index");
        }

        [AcceptVerbs("GET", "POST")]
        [Route("active/{id}")]
        public IActionResult Active(long id) {
            _service.ActivePartime(id);
            return RedirectToAction("Index");
        }

        [AcceptVerbs("GET", "POST")]
        [Route("Delete")]
        public IActionResult Delete(long id) {
            _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
