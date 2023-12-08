using ID.Core.Roles.Abstractions;
using ID.Core.Roles.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ID.Core.Roles
{
    public class RoleService : IRoleService
    {
        protected readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        public virtual async Task<ServiceRoleResult> CreateAsync(IdentityRole creatingRole, Iniciator iniciator, CancellationToken token = default)
        {
            var currentRole = await _roleManager.FindByNameAsync(creatingRole.Name)
                ?? await _roleManager.FindByIdAsync(creatingRole.Id);

            if (currentRole != null)
                throw new RoleAddException($"CreateAsync: role (RoleName - {creatingRole.Name}) was found");

            var createdResult = await _roleManager.CreateAsync(creatingRole);
            if (!createdResult.Succeeded)
                throw new RoleAddException($"CreateAsync: role (RoleId - {creatingRole.Id}, RoleName - {creatingRole.Name}) error created. " +
                    $"{string.Join(';', createdResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");

            await _roleManager.AddClaimAsync(creatingRole, new Claim(IDConstants.Roles.Claims.Types.RoleType, IDConstants.Roles.Claims.Values.Additional));

            return new ServiceRoleResult(creatingRole);
        }
        public virtual async Task EditAsync(EditingRoleData updatingData, Iniciator inicaitor, CancellationToken token = default)
        {
            var currentRole = await _roleManager.FindByIdAsync(updatingData.Role.Id)
                ?? throw new RoleEditException($"EditAsync: role (RoleId - {updatingData.Role.Id}) was not found");

            currentRole.Name = updatingData.Role.Name;
            currentRole.NormalizedName = updatingData.Role.Name.ToUpper();

            var updatedResult = await _roleManager.UpdateAsync(currentRole);
            if (!updatedResult.Succeeded)
                throw new RoleEditException($"EditAsync: role (RoleId - {updatingData.Role.Id}, RoleName - {updatingData.Role.Name}) error updated. " +
                    $"{string.Join(';', updatedResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");

            var currentRoleClaims = await _roleManager.GetClaimsAsync(currentRole);

            foreach (var currentRoleClaim in currentRoleClaims)
                if(currentRoleClaim.Type != IDConstants.Roles.Claims.Types.RoleType)
                    await _roleManager.RemoveClaimAsync(currentRole, currentRoleClaim);

            foreach (var claim in updatingData.Claims)
            {
                var addClaimResult = await _roleManager.AddClaimAsync(currentRole, claim);
                if (!addClaimResult.Succeeded)
                    throw new RoleAddClaimException($"AddClaimsAsync: claim " +
                        $"(ClaimType - {claim.Type}, ClaimValue - {claim.Value}) error adding to the role (RoleId - {currentRole.Id}). " +
                        $"{string.Join(';', addClaimResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");
            }
        }
        public virtual async Task<ServiceRoleResult> FindByIdAsync(string roleId, Iniciator iniciator, CancellationToken token = default)
        {
            var role = await _roleManager.FindByIdAsync(roleId)
                ?? throw new RoleNotFoundException($"FindByIdAsync: role (RoleId - {roleId}) was not found");

            var roleClaims = await _roleManager.GetClaimsAsync(role);

            return new ServiceRoleResult(role, roleClaims);
        }
        public virtual async Task<IEnumerable<ServiceRoleResult>> GetAsync(Iniciator iniciator, CancellationToken token = default)
        {
            List<ServiceRoleResult> result = new List<ServiceRoleResult>();

            var roles = await _roleManager.Roles.ToListAsync(token);

            if (!roles.Any())
                throw new RoleNoContentException($"GetAsync: the roles table does not contain any records");

            foreach (var role in roles)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                result.Add(new ServiceRoleResult(role, roleClaims));
            }

            return result;
        }
        public virtual async Task<IEnumerable<ServiceRoleResult>> GetAsync(RoleSearchFilter filter, Iniciator iniciator, CancellationToken token = default)
        {
            List<ServiceRoleResult> result = new List<ServiceRoleResult>();

            var query = _roleManager.Roles;

            if(!string.IsNullOrEmpty(filter.Id))
                query = query.Where(x => x.Id == filter.Id);
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(x => x.Name == filter.Name);

            if(!query.Any())
                throw new RoleNoContentException($"GetAsync: the roles table does not contain any records by filter (Filter - {filter})");

            foreach (var role in await query.ToListAsync(token))
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                result.Add(new ServiceRoleResult(role, roleClaims));
            }

            return result;
        }
        public virtual async Task RemoveAsync(string roleId, Iniciator iniciator, CancellationToken token = default)
        {
            var removingRole = await _roleManager.FindByIdAsync(roleId)
                ?? throw new RoleRemoveException($"RemoveAsync: role (RoleId - {roleId}) was not found");

            var removedResult = await _roleManager.DeleteAsync(removingRole);
            if (!removedResult.Succeeded)
                throw new RoleRemoveException($"RemoveAsync: role (RoleId - {removingRole.Id}, RoleName - {removingRole.Name}) error removed. " +
                    $"{string.Join(';', removedResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");
        }
    }
}
