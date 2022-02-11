using FluentValidation;

namespace Application.Models.Transaction
{
    public class CreateTransactionRequestValidator : AbstractValidator<CreateTransactionRequest>
    {
        public CreateTransactionRequestValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0).LessThan(decimal.MaxValue);
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Description).MaximumLength(128);
        }
    }
}
