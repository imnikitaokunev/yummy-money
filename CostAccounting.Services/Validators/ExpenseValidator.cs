using System;
using CostAccounting.Services.Core;
using CostAccounting.Services.Membership;
using CostAccounting.Services.Models.Expense;
using FluentValidation;

namespace CostAccounting.Services.Validators
{
    public class ExpenseValidator : AbstractValidator<ExpenseModel>
    {
        public const string CategoryIdErrorMessage = "This category doesn't exists.";
        public const string UserIdErrorMessage = "This user doesn't exists.";
        public const string DescriptionLengthErrorMessage = "Description length must be 0...128.";

        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public ExpenseValidator(ICategoryService categoryService, IUserService userService)
        {
            _categoryService = categoryService;
            _userService = userService;

            RuleFor(x => x.CategoryId).Must(ExistInCategoryRepository).WithMessage(CategoryIdErrorMessage);
            RuleFor(x => x.UserId).Must(ExistInUserRepository).WithMessage(UserIdErrorMessage);
            RuleFor(x => x.Amount).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Description).Empty().WithMessage(DescriptionLengthErrorMessage);
        }

        private bool ExistInCategoryRepository(Guid categoryId) => _categoryService.GetById(categoryId) != null;
        private bool ExistInUserRepository(Guid userId) => _userService.GetById(userId) != null;
    }
}