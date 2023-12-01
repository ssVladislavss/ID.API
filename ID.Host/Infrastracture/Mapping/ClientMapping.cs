using AutoMapper;
using ID.Host.Infrastracture.Mapping.Profiles;
using ID.Host.Infrastracture.Models.Clients;
using IdentityServer4.Models;

namespace ID.Host.Infrastracture.Mapping
{
    public static class ClientMapping
    {
        internal static IMapper Mapper { get; }
        static ClientMapping()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile(new ClientMappingProfile())).CreateMapper();
        }

        public static Client ToModel(this CreateClientViewModel source)
        {
            return Mapper.Map<Client>(source);
        }

        public static Client ToModel(this EditClientViewModel source)
        {
            return Mapper.Map<Client>(source);
        }
    }
}
