using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trabea.Presentasion.Web.Services.Interfaces;
using Trabea.Presentasion.Web.ViewModels.Account;

namespace Trabea.Presentasion.Web.Controllers {
    [Route("")]
    public class AccountController : Controller {
        private readonly IAccountService _service;

        public AccountController(IAccountService service) {
            _service = service;
        }

        [HttpGet("login")]
        public IActionResult Login() {
            return View();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(AccountLoginViewModel vm) {
            if (!ModelState.IsValid) {
                return View(vm);
            }
            try {
                var authTicket = _service.Login(vm);
                await HttpContext.SignInAsync(authTicket.AuthenticationScheme, authTicket.Principal, authTicket.Properties);
                return RedirectToAction("Index", "Home");

            } catch (Exception e) {
                ViewBag.ErrorMessage = e.Message;
                return View(vm);
            }
        }

        [HttpGet("register")]
        public IActionResult RegisterAdmin() {
            return View(new AccountRegisterEmployeeViewModel());
        }

        [HttpPost("register")]
        public IActionResult RegisterAdmin(AccountRegisterEmployeeViewModel vm) {
            if(!ModelState.IsValid) {
                return View(vm);
            }
            var result = _service.RegisterEmployee(vm);
            if (result == "berhasil") {
                ViewData["SuccessMessage"] = "Registrasi berhasil!";
            } else {
                ViewData["ErrorMessage"] = "Registrasi gagal. Silakan coba lagi.";
            }
            return View(vm);
        }

        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout() {
            if (User.Identity!.IsAuthenticated) {
                await HttpContext.SignOutAsync();
            }
            return RedirectToAction("Login");
        }

        [HttpGet("forbidden")]
        [Authorize]
        public IActionResult Forbidden() {
            return View();
        }
    }
}
