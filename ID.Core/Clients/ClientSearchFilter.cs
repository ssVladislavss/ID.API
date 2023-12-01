using Newtonsoft.Json;

namespace ID.Core.Clients
{
    public class ClientSearchFilter
    {
        private string? clientId;
        private bool? status;
        private int? id;
        private string? name;

        public string? ClientId => clientId;
        public bool? Status => status;
        public int? Id => id;
        public string? Name => name;

        public ClientSearchFilter WithId(int id)
        {
            this.id = id;

            return this;
        }

        public ClientSearchFilter WithClientId(string clientId)
        {
            this.clientId = clientId;

            return this;
        }

        public ClientSearchFilter WithStatus(bool status)
        {
            this.status = status;

            return this;
        }

        public ClientSearchFilter WithName(string clientName)
        {
            this.name = clientName;

            return this;
        }

        public override string ToString()
            => JsonConvert.SerializeObject(this, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
    }
}
