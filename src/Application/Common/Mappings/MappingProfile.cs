using System.Collections.Generic;
using Application.Common.Models;
using Application.Models.Category;
using Application.Models.Expense;
using Application.Models.Role;
using Application.Models.User;
using Domain.Entities;
using Mapster;

namespace Application.Common.Mappings
{
    public class MappingProfile
    {
        public static void ApplyMappings()
        {
            // Category

            TypeAdapterConfig<Category, CategoryDto>
                .NewConfig();

            TypeAdapterConfig<CategoryDto, Category>
                .NewConfig();

            TypeAdapterConfig<CreateCategoryRequest, Category>
                .NewConfig();

            TypeAdapterConfig<List<Category>, List<CategoryDto>>
                .NewConfig();

            var categoryCtor = typeof(PaginatedList<CategoryDto>).GetConstructor(new[]
                {typeof(List<CategoryDto>), typeof(int), typeof(int), typeof(int)});
            TypeAdapterConfig<PaginatedList<Category>, PaginatedList<CategoryDto>>
                .NewConfig()
                .MapToConstructor(categoryCtor);


            // Role

            TypeAdapterConfig<Role, RoleDto>
                .NewConfig();

            TypeAdapterConfig<RoleDto, Role>
                .NewConfig();

            TypeAdapterConfig<CreateRoleRequest, Role>
                .NewConfig();

            TypeAdapterConfig<List<Role>, List<RoleDto>>
                .NewConfig();

            var roleCtor = typeof(PaginatedList<RoleDto>).GetConstructor(new[]
                {typeof(List<RoleDto>), typeof(int), typeof(int), typeof(int)});
            TypeAdapterConfig<PaginatedList<Role>, PaginatedList<RoleDto>>
                .NewConfig()
                .MapToConstructor(roleCtor);

            // User

            TypeAdapterConfig<User, UserDto>
                .NewConfig();

            TypeAdapterConfig<UserDto, User>
                .NewConfig();

            TypeAdapterConfig<CreateUserRequest, User>
                .NewConfig();

            TypeAdapterConfig<List<User>, List<UserDto>>
                .NewConfig();

            var userCtor = typeof(PaginatedList<UserDto>).GetConstructor(new[]
                {typeof(List<UserDto>), typeof(int), typeof(int), typeof(int)});
            TypeAdapterConfig<PaginatedList<User>, PaginatedList<UserDto>>
                .NewConfig()
                .MapToConstructor(userCtor);

            // Expense

            TypeAdapterConfig<Expense, ExpenseDto>
                .NewConfig();

            TypeAdapterConfig<Expense, ExpenseWithCategoryDto>
                .NewConfig();

            TypeAdapterConfig<ExpenseDto, Expense>
                .NewConfig();

            TypeAdapterConfig<CreateExpenseRequest, Expense>
                .NewConfig();

            TypeAdapterConfig<List<Expense>, List<ExpenseDto>>
                .NewConfig();

            TypeAdapterConfig<List<Expense>, List<ExpenseWithCategoryDto>>
                .NewConfig();

            var expenseCtor = typeof(PaginatedList<ExpenseDto>).GetConstructor(new[]
                {typeof(List<ExpenseDto>), typeof(int), typeof(int), typeof(int)});
            TypeAdapterConfig<PaginatedList<Expense>, PaginatedList<ExpenseDto>>
                .NewConfig()
                .MapToConstructor(expenseCtor);
        }
    }
}