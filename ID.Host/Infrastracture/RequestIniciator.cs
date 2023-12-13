using ID.Core;
using IdentityModel;
using ISDS.ServiceExtender.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace ID.Host.Infrastracture
{
    public class RequestIniciator : SrvUser
    {
        public RequestIniciator()
        {
        }

        public RequestIniciator(IEnumerable<ClaimsIdentity> identities) : base(identities)
        {
        }

        public RequestIniciator(IIdentity identity) : base(identity)
        {
        }

        public RequestIniciator(IPrincipal principal) : base(principal)
        {
        }

        public override string? Id => FindFirst(JwtClaimTypes.Subject)?.Value;
        public override string? Email => FindFirst(JwtClaimTypes.Email)?.Value;
        public override string? Phone => FindFirst(JwtClaimTypes.PhoneNumber)?.Value;
        public override string? FullName => FindFirst(JwtClaimTypes.FamilyName)?.Value + " " + FindFirst(JwtClaimTypes.GivenName)?.Value + " " + FindFirst(JwtClaimTypes.MiddleName)?.Value;
        public override int? OrganizationId => null;
        public override string[]? Role
        {
            get
            {
                var roles = FindAll("role")?.Select(x => x.Value)?.ToArray();
                if (roles == null)
                    return Array.Empty<string>();

                return roles.ToArray();
            }
        }
        public override string? ClientType => FindFirst("client_type")?.Value;
        public override int? ClientOrgId => null;
        public override string? ClientId => FindFirst("client_id")?.Value;
        public override string? ClientName => FindFirst("client_name")?.Value;
        public string? CurrentToken => FindFirst("access_token")?.Value;

        public override bool CheckAccess()
            => IsInRole(IDConstants.Roles.RootAdmin);

        public override bool CheckAccess(object organizationId)
            => true;

        public override string ToString()
        {
            return $"clientId: {ClientId}, sub: {Id}{(Role != null ? $", roles:{string.Join(';', Role)}": "")}";
        }
    }
}
