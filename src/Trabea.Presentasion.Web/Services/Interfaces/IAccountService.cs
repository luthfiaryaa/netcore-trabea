using Microsoft.AspNetCore.Authentication;
using Trabea.Presentasion.Web.ViewModels.Account;

namespace Trabea.Presentasion.Web.Services.Interfaces {
    public interface IAccountService {
        public AuthenticationTicket Login(AccountLoginViewModel vm);
        string RegisterEmployee(AccountRegisterEmployeeViewModel vm);
    }
}
