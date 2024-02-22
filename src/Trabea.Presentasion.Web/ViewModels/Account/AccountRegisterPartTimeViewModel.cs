using System.ComponentModel.DataAnnotations;

namespace Trabea.Presentasion.Web.ViewModels.Account {
    public class AccountRegisterPartTimeViewModel {
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string? Email { get; set; }

        [Phone]
        public string Phone { get; set; } = null!;

        [EmailAddress]
        public string PersonalEmail { get; set; } = null!;

        public string? Address { get; set; }
        public string? CurrentEducation { get; set; }
        public string? LastEducation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? RegisnDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
