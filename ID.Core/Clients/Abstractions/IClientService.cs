using IdentityServer4.Models;

namespace ID.Core.Clients.Abstractions
{
    public interface IClientService
    {
        Task<Client> FindAsync(string clientId, Iniciator iniciator, CancellationToken token = default);
        Task<IEnumerable<Client>> GetAsync(Iniciator iniciator, CancellationToken token = default);
        Task<IEnumerable<Client>> GetAsync(ClientSearchFilter filter, Iniciator iniciator, CancellationToken token = default);
        Task<Client> AddAsync(Client client, Iniciator iniciator, CancellationToken token = default);
        Task EditAsync(Client client, Iniciator iniciator, CancellationToken token = default);
        Task EditStatusAsync(string clientId, bool status, Iniciator iniciator, CancellationToken token = default);
        Task RemoveAsync(string clientId, Iniciator iniciator, CancellationToken token = default);
    }
}
