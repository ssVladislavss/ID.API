using ID.Core.Clients.Abstractions;
using ID.Core.Clients.Default;
using ID.Core.Clients.Exceptions;
using ID.Core.Clients.Extensions;
using IdentityModel;
using IdentityServer4.Models;
using ISDS.ServiceExtender.Http;

namespace ID.Core.Clients
{
    public partial class ClientService : IClientService
    {
        protected readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        }

        public virtual async Task<Client> AddAsync(Client client, ISrvUser iniciator, CancellationToken token = default)
        {
            var models = await _clientRepository.GetAsync(new ClientSearchFilter()
                                                              .WithName(client.ClientName), token);
            if (models.Any())
                throw new ClientAddException($"AddAsync: client (ClientName - {client.ClientName}) was found");

            bool clientIdGenerated = false;

            do
            {
                client.ClientId = Guid.NewGuid().ToString();

                var model = await _clientRepository.FindAsync(client.ClientId, token);

                if(model == null)
                    clientIdGenerated = true;

            } while (!clientIdGenerated);

            if(client.Claims.Any(x => x.Type == IDConstants.Client.Claims.Types.ClientType))
            {
                var claimType = client.Claims.First(x => x.Type == IDConstants.Client.Claims.Types.ClientType);
                client.Claims.Remove(claimType);
            }

            if (!client.Claims.Any(x => x.Type == IDConstants.Client.Claims.Types.ClientName))
                client.Claims.Add(new ClientClaim(IDConstants.Client.Claims.Types.ClientName, client.ClientName));

            client.Claims.Add(new ClientClaim(IDConstants.Client.Claims.Types.ClientType, IDConstants.Client.Claims.Values.Additional));

            if (!client.ClientSecrets.Any())
            {
                var clientSecretValue = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                var clientSecret = new Secret(clientSecretValue.ToSha256(), clientSecretValue);

                client.ClientSecrets.Add(clientSecret);
            }

            await _clientRepository.AddAsync(client, token);

            return client;
        }
        public virtual async Task EditAsync(Client client, ISrvUser iniciator, CancellationToken token = default)
        {
            var model = await _clientRepository.FindAsync(client.ClientId, token)
                ?? throw new ClientEditException($"EditAsync: client (ClientId - {client.ClientId}) was not found");

            if (DefaultClient.Clients.Any(x => x.ClientId == client.ClientId))
                throw new ClientDefaultException($"EditAsync: client (ClientId - {client.ClientId}) the application is not allowed to be modified");

            model.Set(client);

            await _clientRepository.EditAsync(model, token);
        }
        public virtual async Task EditStatusAsync(string clientId, bool status, ISrvUser iniciator, CancellationToken token = default)
        {
            var client = await _clientRepository.FindAsync(clientId, token)
                ?? throw new ClientEditException($"EditStatusAsync: client (ClientId - {clientId}) was not found");

            if (DefaultClient.Clients.Any(x => x.ClientId == client.ClientId))
                throw new ClientDefaultException($"EditAsync: client (ClientId - {client.ClientId}) the application is not allowed to be modified");

            if (client.Enabled != status)
            {
                client.Enabled = status;

                await _clientRepository.EditAsync(client, token);
            }
        }
        public virtual async Task<Client> FindAsync(string clientId, ISrvUser iniciator, CancellationToken token = default)
        {
            var client = await _clientRepository.FindAsync(clientId, token)
                ?? throw new ClientEditException($"FindAsync: client (ClientId - {clientId}) was not found");

            return client;
        }
        public virtual async Task<IEnumerable<Client>> GetAsync(ISrvUser iniciator, CancellationToken token = default)
        {
            var clients = await _clientRepository.GetAsync(token);

            if(!clients.Any())
                throw new ClientNoContentException($"GetAsync: the clients table does not contain any records");

            return clients;
        }
        public virtual async Task<IEnumerable<Client>> GetAsync(ClientSearchFilter filter, ISrvUser iniciator, CancellationToken token = default)
        {
            var clients = await _clientRepository.GetAsync(filter, token);

            if(!clients.Any())
                throw new ClientNoContentException($"GetAsync: the clients table does not contain any records by filter (Filter - {filter})");

            return clients;
        }
        public virtual async Task RemoveAsync(string clientId, ISrvUser iniciator, CancellationToken token = default)
        {
            if (!iniciator.IsInRole(IDConstants.Roles.RootAdmin))
                throw new ClientRemoveAccessException($"RemoveAsync: user (Iniciator - {iniciator.Id}) does not have access to delete client (ClientId - {clientId})");

            if (DefaultClient.Clients.Any(x => x.ClientId == clientId))
                throw new ClientDefaultException($"RemoveAsync: client (ClientId - {clientId}) is not allowed to be deleted");

            var client = await _clientRepository.FindAsync(clientId, token)
                ?? throw new ClientRemoveException($"RemoveAsync: client (ClientId - {clientId}) was not found");

            await _clientRepository.RemoveAsync(client.ClientId, token);
        }
    }
}
