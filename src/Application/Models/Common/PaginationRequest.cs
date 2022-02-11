using Application.Enums;

namespace Application.Models.Common
{
    public abstract class PaginationRequest : Request
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string SortBy { get; set; }
        public SortType SortType { get; set; }
    }
}
