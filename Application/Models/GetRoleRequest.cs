using Application.Common.Models;

namespace Application.Models
{
    public class GetRoleRequest : Request
    {
        public string Name { get; set; }
    }
}
