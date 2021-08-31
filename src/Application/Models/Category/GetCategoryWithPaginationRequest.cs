using System;
using Application.Common.Models;

namespace Application.Models.Category
{
    public class GetCategoryWithPaginationRequest : PaginationRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
    }
}