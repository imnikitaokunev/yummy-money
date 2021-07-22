using Application.Common.Models;

namespace Application.Models
{
    public class GetRoleWithPaginationRequest : PaginationRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
