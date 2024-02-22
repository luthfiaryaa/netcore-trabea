using Trabea.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabea.DataAccess.Models;

namespace Trabea.Business.Interfaces {
    public interface IRoleRepository : ICrudRepository<Role, long> {
        public Role GetRoleId(long id);
        public Role GetRoleByEmail(string email);
    }
}
