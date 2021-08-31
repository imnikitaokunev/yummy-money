using Application.Enums;

namespace Application.Common.Models
{
    public abstract class Request
    {
        public string SortBy { get; set; }
        public SortType SortType { get; set; }
    }
}
