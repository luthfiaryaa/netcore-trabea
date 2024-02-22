using Trabea.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabea.DataAccess.Models;

namespace Trabea.Business.Interfaces {
    public interface IEmployeeRepository : ICrudRepository<Employee, long> {
        public List<Employee> GetAllManager();
        public Employee GetByEmail(string email);
    }
}
