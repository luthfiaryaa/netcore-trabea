using Microsoft.EntityFrameworkCore;
using Trabea.Business.Interfaces;
using Trabea.Business.Repositories;
using Trabea.DataAccess.Models;

namespace Trabea.Business.Implementations {
    public class ScheduleRepository : CrudRepository<Schedule, long>, IScheduleRepository{
        private readonly TrabeaContext _dbContext;

        public ScheduleRepository(TrabeaContext dbContext) : base(dbContext) {
            _dbContext = dbContext;
        }

        public long Count(string? name) {
            return _dbContext.Schedules.Count();
        }

        public List<Schedule> GetAll(int pageNumber, int pageSize, string? name) {
            var query = from schedule in _dbContext.Schedules
                        select schedule;
            return query.Include(s => s.Partime).Include(s => s.Shift)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();
        }

        public List<Schedule> GetAllApproval() {
            return _dbContext.Schedules.Include(s => s.Shift).Include(s => s.Partime).ToList();
        }

        public List<Schedule> WeekSch(DateTime startDate, DateTime endate) {
            return _dbContext.Schedules.Include(s => s.Partime)
                .OrderBy(s => s.ScheduleDate)
                .Where(s => s.ScheduleDate >= startDate && s.ScheduleDate <= endate 
            && s.IsApproved == true).ToList();
        }
    }
}
