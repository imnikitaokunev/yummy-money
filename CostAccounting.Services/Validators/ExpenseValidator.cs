using CostAccounting.Core.Entities.Core;
using FluentValidation;

namespace CostAccounting.Services.Validators
{
    public class ExpenseValidator : AbstractValidator<Expense>
    {
        public const string DescriptionLengthErrorMessage = "Description length must be 0...128.";

        public ExpenseValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Description).MaximumLength(128).WithMessage(DescriptionLengthErrorMessage);
        }
    }
}