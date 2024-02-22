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
    public class EmployeeRepository : CrudRepository<Employee, long>, IEmployeeRepository{
        private readonly TrabeaContext _dbContext;

        public EmployeeRepository(TrabeaContext dbContext) : base(dbContext) {
            _dbContext = dbContext;
        }

        public List<Employee> GetAllManager() {
            return _dbContext.Employees.Include(e => e.OfficeEmailNavigation).ThenInclude(a => a.Roles)
                .Where(e => e.OfficeEmailNavigation.Roles.Any(role => role.Name!.Equals("Manager")))
                .ToList();
        }

        public Employee GetByEmail(string email) {
            return _dbContext.Employees.FirstOrDefault(e => e.OfficeEmail.Equals(email))
                ?? throw new KeyNotFoundException(EntityNotFoundMessage); 
        }
    }
}
