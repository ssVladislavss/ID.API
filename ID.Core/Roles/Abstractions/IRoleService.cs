using ISDS.ServiceExtender.Http;
using Microsoft.AspNetCore.Identity;

namespace ID.Core.Roles.Abstractions
{
    public interface IRoleService
    {
        Task<IEnumerable<ServiceRoleResult>> GetAsync(ISrvUser iniciator, CancellationToken token = default);
        Task<IEnumerable<ServiceRoleResult>> GetAsync(RoleSearchFilter filter, ISrvUser iniciator, CancellationToken token = default);
        Task<ServiceRoleResult> FindByIdAsync(string roleId, ISrvUser iniciator, CancellationToken token = default);
        Task<ServiceRoleResult> CreateAsync(IdentityRole creatingRole, ISrvUser iniciator, CancellationToken token = default);
        Task EditAsync(EditingRoleData updatingData, ISrvUser inicaitor, CancellationToken token = default);
        Task RemoveAsync(string roleId, ISrvUser iniciator, CancellationToken token = default);
    }
}
