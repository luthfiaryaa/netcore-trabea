using Microsoft.AspNetCore.Mvc;
using Trabea.Presentasion.Web.Services.Interfaces;

namespace Trabea.Presentasion.Web.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountService _accountService;
        public HomeController(ILogger<HomeController> logger, IAccountService accountService) {
            _logger = logger;
            _accountService = accountService;
        }

        public IActionResult Index() {
            return RedirectToAction("Index", "Schedule");
        }
    }
}
