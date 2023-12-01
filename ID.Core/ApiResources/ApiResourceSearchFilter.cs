using Newtonsoft.Json;

namespace ID.Core.ApiResources
{
    public class ApiResourceSearchFilter
    {
        private int? id;
        private string? name;

        public int? Id => id;
        public string? Name => name;

        public ApiResourceSearchFilter WithId(int id)
        {
            this.id = id;

            return this;
        }

        public ApiResourceSearchFilter WithName(string name)
        {
            this.name = name;

            return this;
        }

        public override string ToString()
            => JsonConvert.SerializeObject(this, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
    }
}
