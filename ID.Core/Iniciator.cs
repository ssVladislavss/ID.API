using System.Runtime.Serialization;
using System.Security.Claims;
using System.Security.Principal;

namespace ID.Core
{
    public class Iniciator : ClaimsPrincipal, IPrincipal
    {
        public Iniciator()
        {
        }

        public Iniciator(IEnumerable<ClaimsIdentity> identities) : base(identities)
        {
        }

        public Iniciator(BinaryReader reader) : base(reader)
        {
        }

        public Iniciator(IIdentity identity) : base(identity)
        {
        }

        public Iniciator(IPrincipal principal) : base(principal)
        {
        }

        protected Iniciator(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string? ClientId
            => FindFirst("client_id")?.Value;
        public string? ClientName
            => FindFirst("client_name")?.Value;
        public string? UserId
            => FindFirst("sub")?.Value;
        public string[] Roles
        {
            get
            {
                var roles = FindAll("role")?.Select(x => x.Value)?.ToArray();
                if(roles == null)
                    return Array.Empty<string>();

                return roles.ToArray();
            }
        }
        public string? Email
            => FindFirst("email")?.Value;
    }
}
