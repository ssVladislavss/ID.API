using Microsoft.AspNetCore.Identity;

namespace ID.Core.Roles.Abstractions
{
    public interface IRoleService
    {
        Task<IEnumerable<ServiceRoleResult>> GetAsync(Iniciator iniciator, CancellationToken token = default);
        Task<IEnumerable<ServiceRoleResult>> GetAsync(RoleSearchFilter filter, Iniciator iniciator, CancellationToken token = default);
        Task<ServiceRoleResult> FindByIdAsync(string roleId, Iniciator iniciator, CancellationToken token = default);
        Task<ServiceRoleResult> CreateAsync(IdentityRole creatingRole, Iniciator iniciator, CancellationToken token = default);
        Task EditAsync(EditingRoleData updatingData, Iniciator inicaitor, CancellationToken token = default);
        Task RemoveAsync(string roleId, Iniciator iniciator, CancellationToken token = default);
    }
}
