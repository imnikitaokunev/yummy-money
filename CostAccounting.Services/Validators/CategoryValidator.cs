using CostAccounting.Core.Entities.Core;
using CostAccounting.Services.Models.Category;
using FluentValidation;

namespace CostAccounting.Services.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryModel>
    {
        public const string NameLengthErrorMessage = "Name length must be 1...128.";
        public const string DescriptionLengthErrorMessage = "Description length must be 0...128.";

        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(Category.NameLength).WithMessage(NameLengthErrorMessage);
            RuleFor(x => x.Description).MaximumLength(Category.DescriptionLength)
                .WithMessage(DescriptionLengthErrorMessage);
        }
    }
}