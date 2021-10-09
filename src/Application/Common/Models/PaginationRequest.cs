namespace Application.Common.Models
{
    public abstract class PaginationRequest : Request
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
