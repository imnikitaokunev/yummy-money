namespace Application.Common.Models
{
    public abstract class PaginationRequest : Request
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
