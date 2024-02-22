using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Trabea.Business.Implementations;
using Trabea.Business.Interfaces;
using Trabea.DataAccess.Models;
using Trabea.Presentasion.Web.Services.Interfaces;
using Trabea.Presentasion.Web.ViewModels.PartTime;
using Trabea.Presentation.Web.ViewModels;

namespace Trabea.Presentasion.Web.Services.Implementations {
    public class PartTimeService : Helper, IPartTimeService {
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPartTimeRepository _partTimeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TrabeaContext _dbContext;

        public PartTimeService(IAccountRepository accountRepository, IRoleRepository roleRepository, IPartTimeRepository partTimeRepository, IHttpContextAccessor httpContextAccessor, TrabeaContext dbContext) {
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
            _partTimeRepository = partTimeRepository;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        protected string GetUsername()
        {
            var user = _httpContextAccessor.HttpContext.User;

            if (user.Identity!.IsAuthenticated)
            {
                foreach (var claim in user.Claims)
                {
                    if (claim.Type == ClaimTypes.NameIdentifier)
                    {
                        return claim.Value;
                    }
                }
            }
            return "DefaultUsername";
        }

        public void ActivePartime(long id) {
            var partTime = _partTimeRepository.GetById(id);
            var transaction = _dbContext.Database.BeginTransaction();
            try {
                var email = GenerateEmail($"{partTime.FirstName}{partTime.LastName}", _accountRepository);
                var account = new Account {
                    Email = email,
                    Password = BCrypt.Net.BCrypt.HashPassword("Trabea123")
                };
                var role = _roleRepository.GetRoleId(3);
                account.Roles.Add(role);
                _accountRepository.Save(account);

                partTime.StartDate = DateTime.Today;
                partTime.ResignDate = null;
                partTime.OfficeEmail = email;
                partTime.IsActive = true;
               
                _partTimeRepository.Save(partTime);
                transaction.Commit();
            } catch (DbUpdateException ex) {
                transaction.Rollback();
                Console.WriteLine(ex);
            }
        }

        
        public PartTimeIndexViewModel GetAll(int pageNumber, int pageSize) {
            return new PartTimeIndexViewModel {
                PartTimes = _partTimeRepository.GetAll(pageNumber, pageSize).Select(p =>
                    new PartTimeItemViewModel {
                        Id = p.Id,
                        FullName = p.FirstName + " " + p.LastName,
                        Address = p.Address,
                        PersonalEmail = p.PersonalEmail,
                        OfficelEmail = p.OfficeEmail,
                        Phone = p.Phone,
                        IsActive = p.IsActive
                    }).ToList(),
                PaginationInfo = new PaginationInfoViewModel {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalItem = (int)_partTimeRepository.Count()
                }
            };
        }

        public void Insert(PartTimeUpsertViewModel vm) {
            var transaction = _dbContext.Database.BeginTransaction();
            try {
                var email = GenerateEmail($"{vm.FirstName}{vm.LastName}", _accountRepository);
                var account = new Account {
                    Email = email,
                    Password = BCrypt.Net.BCrypt.HashPassword("Trabea123")
                };
                var role = _roleRepository.GetRoleId(3);
                account.Roles.Add(role);
                _accountRepository.Save(account);

                var partTime = new PartTimeEmployee {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    PersonalEmail = vm.PersonalEmail,
                    Phone = vm.Phone,
                    OfficeEmail = email,
                    Address = vm.Address,
                    CurrentEducation = vm.CurrentEducation,
                    LastEducation = vm.LastEducation,
                    StartDate = vm.StartDate ?? DateTime.Today,
                    ResignDate = vm.RegisnDate,
                    BirthDate = vm.BirthDate,
                    IsActive = true,
                };
                _partTimeRepository.Save(partTime);
                transaction.Commit();
            } catch (DbUpdateException ex) {
                transaction.Rollback();
                Console.WriteLine(ex);
            }
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

        public PartTimeUpsertViewModel GetById(long Id) {
            var partTime = _partTimeRepository.GetById(Id);
            return new PartTimeUpsertViewModel {
                Id = partTime.Id,
                FirstName = partTime.FirstName,
                LastName = partTime.LastName,
                PersonalEmail = partTime.PersonalEmail,
                Email = partTime.OfficeEmail,
                LastEducation = partTime.LastEducation,
                Address = partTime.Address,
                BirthDate = partTime.BirthDate,
                CurrentEducation = partTime.CurrentEducation,
                IsActive = partTime.IsActive,
                RegisnDate = partTime.ResignDate,
                Phone = partTime.Phone,
                StartDate = partTime.StartDate
            };
        }

        public void Update(long id, PartTimeUpsertViewModel vm) {
            var entity = _partTimeRepository.GetById(id);
            entity.FirstName = vm.FirstName;
            entity.LastName = vm.LastName;
            entity.PersonalEmail = vm.PersonalEmail;
            entity.BirthDate = vm.BirthDate;
            entity.CurrentEducation = vm.CurrentEducation;
            entity.Phone = vm.Phone;
            entity.IsActive = vm.IsActive;
            _partTimeRepository.Save(entity);
        }
    }
}
