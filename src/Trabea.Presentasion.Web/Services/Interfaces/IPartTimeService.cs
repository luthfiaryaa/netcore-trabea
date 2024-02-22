using Trabea.Presentasion.Web.ViewModels.Account;
using Trabea.Presentasion.Web.ViewModels.PartTime;

namespace Trabea.Presentasion.Web.Services.Interfaces {
    public interface IPartTimeService {
        public PartTimeIndexViewModel GetAll(int pageNumber, int pageSize);
        public PartTimeUpsertViewModel GetById(long Id);
        public void Insert(PartTimeUpsertViewModel vm);
        public void Update(long id, PartTimeUpsertViewModel vm);
        public void ActivePartime(long id);
        public void Delete(long id);
    }
}
