using CostAccounting.Services.Membership;
using CostAccounting.Services.Models.User;
using FluentValidation;

namespace CostAccounting.Services.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLoginModel>
    {
        public const string UsernameEmptyErrorMessage = "Username must be not empty.";
        public const string UserDoesNotExistsErrorMessage = "User with this username doesn't exixsts.";
        public const string PasswordEmptyErrorMessage = "Password must be not empty.";

        private readonly IUserService _userService;

        public UserLoginValidator(IUserService userService)
        {
            _userService = userService;

            RuleFor(x => x.Username).NotEmpty().WithMessage(UsernameEmptyErrorMessage);//.Must(ExistsInUserRepository)
               // .WithMessage(UserDoesNotExistsErrorMessage);
            RuleFor(x => x.Password).NotEmpty().WithMessage(PasswordEmptyErrorMessage);
        }

        private bool ExistsInUserRepository(string username) => _userService.GetByUsername(username) != null;
    }
}