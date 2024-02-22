using Trabea.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabea.DataAccess.Models;

namespace Trabea.Business.Interfaces {
    public interface IAccountRepository : ICrudRepository<Account, string> {
        public Account GetRole(string email);
        public List<Role> GetRoleList(string email);
    }
}
