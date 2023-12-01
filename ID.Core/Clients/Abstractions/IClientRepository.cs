using IdentityServer4.Models;

namespace ID.Core.Clients.Abstractions
{
    public interface IClientRepository
    {
        Task<Client?> FindAsync(string clientId, CancellationToken token = default);
        Task<IEnumerable<Client>> GetAsync(CancellationToken token = default);
        Task<IEnumerable<Client>> GetAsync(ClientSearchFilter filter, CancellationToken token = default);
        Task<int> AddAsync(Client client, CancellationToken token = default);
        Task EditAsync(Client client, CancellationToken token = default);
        Task RemoveAsync(string clientId, CancellationToken token = default);
    }
}
