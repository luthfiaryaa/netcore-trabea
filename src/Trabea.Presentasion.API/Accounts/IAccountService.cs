using Trabea.Presentasion.API.Accounts.Dtos;

namespace Trabea.Presentasion.API.Accounts {
    public interface IAccountService {
        string RegisterEmployee(AccountRegisterRequestDto vm);
        void AddNewRole(string existingAccountId, long newRoleId);
        public void Delete(long id);
    }
}
