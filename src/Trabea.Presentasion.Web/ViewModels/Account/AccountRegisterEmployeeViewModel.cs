using System.ComponentModel.DataAnnotations;

namespace Trabea.Presentasion.Web.ViewModels.Account {
    public class AccountRegisterEmployeeViewModel {
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
        [Compare("Password", ErrorMessage = "Password didn't match. Try Again")]
        public string RetypePassword { get; set; } = null!;
        //public long? RoleId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
    }
}
