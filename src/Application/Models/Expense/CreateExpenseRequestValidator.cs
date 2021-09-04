using FluentValidation;

namespace Application.Models.Expense
{
    public class CreateExpenseRequestValidator : AbstractValidator<CreateExpenseRequest>
    {
        public CreateExpenseRequestValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0).LessThan(decimal.MaxValue);
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Description).MaximumLength(128);
        }
    }
}
