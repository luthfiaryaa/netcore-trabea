using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trabea.Business.Implementations;
using Trabea.Business.Interfaces;
using Trabea.DataAccess.Models;
using Trabea.Presentasion.API.Accounts.Dtos;

namespace Trabea.Presentasion.API.Accounts {
    public class AccountService : IAccountService {
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





        //public void RegisterEmployee(AccountRegisterItemViewModel vm) {
        //    var acc = new Account {
        //        Email = vm.Email,
        //        Password = vm.Password
        //        };
        //    var role = _roleRepository.GetRoleId(vm.RoleId);
        //    acc.Roles.Add(role);
        //    _accountRepository.Save(acc);
        //}

        public List<SelectListItem> GetRoleDropdown() {
            return _roleRepository.GetAll().Select(r => new SelectListItem {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();
        }

        public string GenerateEmail(string name) {
            string generatedEmail = $"{name}@trabea.com";
            var acc = _accountRepository.GetById(generatedEmail);

            int suffix = 1;
            while (acc != null) {
                generatedEmail = $"{name}{suffix}@trabea.com";
                acc = _accountRepository.GetById(generatedEmail);
                suffix++;
            }

            return generatedEmail;
        }



        public string RegisterEmployee(AccountRegisterRequestDto vm) {
            var transaction = _accountRepository.BeginTransaction();
            try {
                var email = GenerateEmail($"{vm.FirstName}{vm.LastName}");
                var account = new Account {
                    Email = email,
                    Password = vm.Password
                };
                var role = _roleRepository.GetRoleId(vm.RoleId);
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

        public void AddNewRole(string existingAccountId, long newRoleId) {
            var existingAccount = _accountRepository.GetById(existingAccountId);
            var newRole = _roleRepository.GetRoleId(newRoleId);
            existingAccount.Roles.Add(newRole);
            _accountRepository.Save(existingAccount);

        }

        public void Delete(long id) {
            var partTime = _dbContext.PartTimeEmployees.SingleOrDefault(p => p.Id == id);
            var transaction = _dbContext.Database.BeginTransaction();
            try {
                var acc = _dbContext.Accounts.Include(a => a.Roles).SingleOrDefault(r => r.Email.Equals(partTime.OfficeEmail));
                partTime.OfficeEmail = null;
                partTime.ResignDate = DateTime.Now;
                partTime.IsActive = false;
                _dbContext.SaveChanges();
                foreach (var item in acc.Roles.ToList()) {
                    acc.Roles.Remove(item);
                }
                _dbContext.SaveChanges();

                _dbContext.Accounts.Remove(acc);
                _dbContext.SaveChanges();  

                transaction.Commit();
            } catch (DbUpdateException ex) {
                transaction.Rollback();
                Console.WriteLine(ex);
            }
        }

    }
}
