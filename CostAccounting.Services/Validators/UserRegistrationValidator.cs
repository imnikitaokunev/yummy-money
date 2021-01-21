using CostAccounting.Core.Entities.Membership;
using CostAccounting.Services.Models.User;
using FluentValidation;

namespace CostAccounting.Services.Validators
{
    public class UserRegistrationValidator : AbstractValidator<UserRegistrationModel>
    {
        public UserRegistrationValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().MaximumLength(User.EmailLength);
            RuleFor(x => x.Username).NotEmpty().MinimumLength(User.UsernameMinLength)
                .MaximumLength(User.UsernameMaxLength);
            RuleFor(x => x.Password).NotEmpty().MaximumLength(128);
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(User.FirstNameLength);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(User.LastNameLength);
        }
    }
}
