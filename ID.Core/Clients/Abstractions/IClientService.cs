using IdentityServer4.Models;
using ISDS.ServiceExtender.Http;

namespace ID.Core.Clients.Abstractions
{
    public interface IClientService
    {
        Task<Client> FindAsync(string clientId, ISrvUser iniciator, CancellationToken token = default);
        Task<IEnumerable<Client>> GetAsync(ISrvUser iniciator, CancellationToken token = default);
        Task<IEnumerable<Client>> GetAsync(ClientSearchFilter filter, ISrvUser iniciator, CancellationToken token = default);
        Task<Client> AddAsync(Client client, ISrvUser iniciator, CancellationToken token = default);
        Task EditAsync(Client client, ISrvUser iniciator, CancellationToken token = default);
        Task EditStatusAsync(string clientId, bool status, ISrvUser iniciator, CancellationToken token = default);
        Task RemoveAsync(string clientId, ISrvUser iniciator, CancellationToken token = default);
    }
}
