using System.ComponentModel.DataAnnotations;

namespace CostAccounting.Services.Models.User
{
    public class UserRegistrationModel
    {
        // TODO: Add validation.  

        [EmailAddress]
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
