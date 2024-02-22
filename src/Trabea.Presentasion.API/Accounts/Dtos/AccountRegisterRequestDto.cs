namespace Trabea.Presentasion.API.Accounts.Dtos {
    public class AccountRegisterRequestDto {
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
        public string RetypePassword { get; set; } = null!;
        public long RoleId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
    }
}
