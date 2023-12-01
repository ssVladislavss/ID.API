using Newtonsoft.Json;

namespace ID.Core.ApiScopes
{
    public class ApiScopeSearchFilter
    {
        private int? id;
        private string? name;

        public int? Id => id;
        public string? Name => name;

        public ApiScopeSearchFilter WithId(int id)
        {
            this.id = id;

            return this;
        }

        public ApiScopeSearchFilter WithName(string name)
        {
            this.name = name;

            return this;
        }

        public override string ToString()
            => JsonConvert.SerializeObject(this, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
    }
}
