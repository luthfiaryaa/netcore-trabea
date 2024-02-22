using Trabea.Business.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabea.Business.Interfaces;
using Trabea.DataAccess.Models;

namespace Trabea.Business.Implementations {
    public class AccountRepository : CrudRepository<Account, string>, IAccountRepository{
        private readonly TrabeaContext _dbContext;

        public AccountRepository(TrabeaContext dbContext) : base(dbContext) {
            _dbContext = dbContext;
        }

        public override Account GetById(string name) {
            return _dbContext.Accounts.Find(name);
        }

        public Account GetRole(string email) {
           return _dbContext.Accounts.Include(a => a.Roles).SingleOrDefault(a => a.Email.Equals(email)) ?? 
                throw new KeyNotFoundException(EntityNotFoundMessage);
        }
        public List<Role> GetRoleList(string email) {
           return _dbContext.Accounts.Include(a => a.Roles).Where(a => a.Email.Equals(email)).SelectMany(a => a.Roles).ToList() ?? 
                throw new KeyNotFoundException(EntityNotFoundMessage);
        }

    }
}
