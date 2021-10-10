using Application.Models.Category;
using Application.Models.Common;
using Application.Models.Transaction;
using Domain.Entities;
using Mapster;
using System.Collections.Generic;

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
                { typeof(List<CategoryDto>), typeof(int), typeof(int), typeof(int) });
            TypeAdapterConfig<PaginatedList<Category>, PaginatedList<CategoryDto>>
                .NewConfig()
                .MapToConstructor(categoryCtor);


            //// User

            //TypeAdapterConfig<User, UserDto>
            //    .NewConfig();

            //TypeAdapterConfig<UserDto, User>
            //    .NewConfig();

            //TypeAdapterConfig<CreateUserRequest, User>
            //    .NewConfig();

            //TypeAdapterConfig<List<User>, List<UserDto>>
            //    .NewConfig();

            //var userCtor = typeof(PaginatedList<UserDto>).GetConstructor(new[]
            //    { typeof(List<UserDto>), typeof(int), typeof(int), typeof(int) });
            //TypeAdapterConfig<PaginatedList<User>, PaginatedList<UserDto>>
            //    .NewConfig()
            //    .MapToConstructor(userCtor);


            // Transaction

            TypeAdapterConfig<Transaction, TransactionDto>
                .NewConfig();

            TypeAdapterConfig<TransactionDto, Transaction>
                .NewConfig();

            TypeAdapterConfig<CreateTransactionRequest, Transaction>
                .NewConfig();

            TypeAdapterConfig<List<Transaction>, List<TransactionDto>>
                .NewConfig();

            var transactionCtor = typeof(PaginatedList<TransactionDto>).GetConstructor(new[]
                { typeof(List<TransactionDto>), typeof(int), typeof(int), typeof(int) });
            TypeAdapterConfig<PaginatedList<Transaction>, PaginatedList<TransactionDto>>
                .NewConfig()
                .MapToConstructor(transactionCtor);
        }
    }
}
