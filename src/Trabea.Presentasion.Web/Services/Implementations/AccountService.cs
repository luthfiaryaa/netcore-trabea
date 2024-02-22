using Trabea.Business.Interfaces;
using Trabea.Presentasion.Web.ViewModels.Account;
using Trabea.DataAccess.Models;
using Trabea.Presentasion.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Trabea.Presentasion.Web.Services.Implementations {
    public class AccountService : Helper, IAccountService {
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPartTimeRepository _partTimeRepository;
        private readonly TrabeaContext _dbContext;

        public AccountService(IAccountRepository accountRepository, IRoleRepository roleRepository, IEmployeeRepository employeeRepository, IPartTimeRepository partTimeRepository, TrabeaContext dbContext) {
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
            _employeeRepository = employeeRepository;
            _partTimeRepository = partTimeRepository;
            _dbContext = dbContext;
        }


        public List<SelectListItem> GetRoleDropdown() {
            return _roleRepository.GetAll().Select(r => new SelectListItem {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();
        }

        public string RegisterEmployee(AccountRegisterEmployeeViewModel vm) {
            var transaction = _accountRepository.BeginTransaction();
            try {
                var email = GenerateEmail($"{vm.FirstName}{vm.LastName}", _accountRepository);
                var account = new Account {
                    Email = email,
                    Password = BCrypt.Net.BCrypt.HashPassword(vm.Password)
                };
                var role = _roleRepository.GetRoleId(1);
                account.Roles.Add(role);
                _accountRepository.Save(account);

                var employee = new Employee {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    OfficeEmail = email
                };
                _employeeRepository.Save(employee);
                transaction.Commit();
                return "berhasil";
            } catch (DbUpdateException ex) {
                transaction.Rollback();
                return "error" + ex;
            }
        }

        public AuthenticationTicket Login(AccountLoginViewModel vm) {
            var account = _accountRepository.GetById(vm.Email);
            if (!BCrypt.Net.BCrypt.Verify(vm.Password, account.Password)) {
                throw new AuthenticationException("Invalid Username or Password");
            }
            var authTicket = GetAuthenticationTicket(account);
            return authTicket;
        }

        private AuthenticationTicket GetAuthenticationTicket(Account account) {
            AuthenticationProperties properties = new AuthenticationProperties() {
                IssuedUtc = DateTime.Now,
                ExpiresUtc = DateTime.Now.AddMinutes(30),
                AllowRefresh = false
            };

            return new AuthenticationTicket(
                GetPrincipal(account),
                properties,
                CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private ClaimsPrincipal GetPrincipal(Account account) {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, account.Email),
                new Claim("email", account.Email),
            };
            foreach (var role in _accountRepository.GetRoleList(account.Email)) {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }

        public void Delete(long id) {
            var transaction = _accountRepository.BeginTransaction();  
            try {
               var part =  _partTimeRepository.GetById(id);
                var acc = _accountRepository.GetById(part.OfficeEmail);
                var role = _roleRepository.GetRoleByEmail(acc.Email);
                acc.Roles.Remove(role);
                part.IsActive = false;
                part.OfficeEmail = null;
                _partTimeRepository.Save(part);
                _accountRepository.Delete(acc.Email);
                transaction.Commit();
            } catch (DbUpdateException ex) {
                transaction.Rollback();
                Console.WriteLine(ex);
            }
        }

    }
}
