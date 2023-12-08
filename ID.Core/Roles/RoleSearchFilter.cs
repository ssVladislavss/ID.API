using Newtonsoft.Json;

namespace ID.Core.Roles
{
    public class RoleSearchFilter
    {
        private string? id;
        public string? name;

        public string? Id => id;
        public string? Name => name;

        public RoleSearchFilter WithId(string roleId)
        {
            id = roleId;

            return this;
        }

        public RoleSearchFilter WithName(string name)
        {
            this.name = name;

            return this;
        }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}
