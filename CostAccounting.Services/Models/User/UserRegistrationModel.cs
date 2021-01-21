namespace CostAccounting.Services.Models.User
{
    public class UserRegistrationModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
    }
}
