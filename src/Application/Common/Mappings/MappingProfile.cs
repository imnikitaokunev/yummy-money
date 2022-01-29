using System.Collections.Generic;
using Application.Models.Category;
using Application.Models.Common;
using Application.Models.Transaction;
using Domain.Entities;
using Mapster;

namespace Application.Common.Mappings;

public class MappingProfile
{
    public static void ApplyMappings()
    {
        var categoryCtor = typeof(PaginatedList<CategoryDto>).GetConstructor(new[]
            { typeof(List<CategoryDto>), typeof(int), typeof(int), typeof(int) });
        TypeAdapterConfig<PaginatedList<Category>, PaginatedList<CategoryDto>>
            .NewConfig()
            .MapToConstructor(categoryCtor);


        var transactionCtor = typeof(PaginatedList<TransactionDto>).GetConstructor(new[]
            { typeof(List<TransactionDto>), typeof(int), typeof(int), typeof(int) });
        TypeAdapterConfig<PaginatedList<Transaction>, PaginatedList<TransactionDto>>
            .NewConfig()
            .MapToConstructor(transactionCtor);
    }
}
