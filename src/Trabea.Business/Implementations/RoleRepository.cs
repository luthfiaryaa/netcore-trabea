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
    public class RoleRepository : CrudRepository<Role, long>, IRoleRepository {
        private readonly TrabeaContext _dbContext;

        public RoleRepository(TrabeaContext dbContext) : base(dbContext) {
            _dbContext = dbContext;
        }

        public Role GetRoleId(long id) {
            return _dbContext.Roles.FirstOrDefault(r => r.Id == id)
                ?? throw new KeyNotFoundException(EntityNotFoundMessage);
        }

        public Role GetRoleByEmail(string email) {
            return _dbContext.Roles
       .Include(r => r.AccountEmails)
       .FirstOrDefault(r => r.AccountEmails.Any(ae => ae.Email == email)) ?? throw new KeyNotFoundException(EntityNotFoundMessage);
        }
    }
}
