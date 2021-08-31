using Application.Common.Models;

namespace Application.Models.Role
{
    public class GetRoleRequest : Request
    {
        public string Name { get; set; }
    }
}
