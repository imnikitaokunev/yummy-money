using Application.Models.Expense;
using FluentValidation;

namespace Application.Models.Income
{
    public class CreateIncomeRequestValidator : AbstractValidator<CreateExpenseRequest>
    {
        public CreateIncomeRequestValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0).LessThan(decimal.MaxValue);;
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Description).MaximumLength(128);
        }
    }
}
