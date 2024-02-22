using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabea.Business.Interfaces;
using Trabea.Business.Repositories;
using Trabea.DataAccess.Models;

namespace Trabea.Business.Implementations {
    public class PartTimeRepository : CrudRepository<PartTimeEmployee, long>, IPartTimeRepository {
        private readonly TrabeaContext _dbContext;

        public PartTimeRepository(TrabeaContext dbContext) : base(dbContext) {
            _dbContext = dbContext;
        }

        public List<PartTimeEmployee> GetAll(int pageNumber, int pageSize) {
            var query = from parttime in _dbContext.PartTimeEmployees select parttime;
            return query
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();
        }

        public long Count() {
            return _dbContext.PartTimeEmployees.Count();
        }

        public override List<PartTimeEmployee> GetAll() {
            return base.GetAll().Where(p => p.IsActive != false).ToList();
        }

        public List<PartTimeEmployee> GetPartTimeEmployeesWithScheduleForThisWeek() {
            DateTime today = DateTime.Today;
            DateTime startOfWeek = today.AddDays((int)DayOfWeek.Sunday - (int)today.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(6);

            var partTimeEmployees = _dbContext.PartTimeEmployees
                      .Include(p => p.Schedules)
                      .Where(p => p.Schedules.Any(s => s.ScheduleDate >= startOfWeek && s.ScheduleDate <= endOfWeek))
                      .ToList();

            return partTimeEmployees.ToList();
        }

        public PartTimeEmployee GetByEmail(string email) {
            return _dbContext.PartTimeEmployees.FirstOrDefault(p => p.OfficeEmail.Equals(email))
                ?? throw new KeyNotFoundException(EntityNotFoundMessage); 
        }

    }
}
