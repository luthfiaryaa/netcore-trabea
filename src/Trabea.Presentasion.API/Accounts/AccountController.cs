using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trabea.Presentasion.API.Accounts.Dtos;

namespace Trabea.Presentasion.API.Accounts {
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase {

        private readonly IAccountService _service;

        public AccountController(IAccountService service) {
            _service = service;
        }

        [HttpPost("regis")]
        public IActionResult Login(AccountRegisterRequestDto dto) {

            return Ok(_service.RegisterEmployee(dto));


        }

        [HttpPost("new_role")]
        public IActionResult Login(string email, long role) {

            _service.AddNewRole(email, role);
                return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            _service.Delete(id);
            return Ok();
        }
    }
}
