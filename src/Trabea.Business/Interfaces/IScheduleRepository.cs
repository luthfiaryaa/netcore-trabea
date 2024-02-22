using Trabea.Business.Repositories;
using Trabea.DataAccess.Models;

namespace Trabea.Business.Interfaces {
    public interface IScheduleRepository : ICrudRepository<Schedule, long> {
        public List<Schedule> GetAll(int pageNumber, int pageSize, string? name);
        public List<Schedule> GetAllApproval();
        public long Count(string? name);
        public List<Schedule> WeekSch(DateTime startDate, DateTime endate);
    }
}
