using Trabea.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabea.DataAccess.Models;

namespace Trabea.Business.Interfaces {
    public interface IPartTimeRepository : ICrudRepository<PartTimeEmployee, long> {
        public List<PartTimeEmployee> GetAll(int pageNumber, int pageSize);
        public long Count();
        public PartTimeEmployee GetByEmail(string email);
        public List<PartTimeEmployee> GetPartTimeEmployeesWithScheduleForThisWeek();
    }
}
