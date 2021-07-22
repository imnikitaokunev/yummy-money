using FluentValidation;

namespace Application.Models
{
    public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryRequestValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(1).WithMessage("Name length at least greater than or equal to 1.")
                .MaximumLength(128).WithMessage("Name length must be less than 128.");

            RuleFor(x => x.Description).MaximumLength(128)
                .WithMessage("Description length must be less than 128.");
        }
    }
}