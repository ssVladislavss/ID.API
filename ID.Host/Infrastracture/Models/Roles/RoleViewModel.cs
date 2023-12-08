using ID.Core.Roles;
using ID.Host.Infrastracture.Models.Claims;
using Microsoft.AspNetCore.Identity;

namespace ID.Host.Infrastracture.Models.Roles
{
    public class RoleViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<ClaimViewModel> Claims { get; set; } = Enumerable.Empty<ClaimViewModel>();

        public RoleViewModel(IdentityRole role)
        {
            RoleId = role.Id;
            RoleName = role.Name;
        }
        public RoleViewModel(ServiceRoleResult roleResult)
        {
            RoleId = roleResult.Role.Id;
            RoleName = roleResult.Role.Name;
            Claims = roleResult.Claims.Select(x => new ClaimViewModel() { Type = x.Type, Value = x.Value });
        }
    }
}
