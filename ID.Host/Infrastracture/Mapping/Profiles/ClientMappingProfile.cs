using AutoMapper;
using ID.Host.Infrastracture.Models.Clients;
using IdentityServer4.Models;

namespace ID.Host.Infrastracture.Mapping.Profiles
{
    public class ClientMappingProfile : Profile
    {
        public ClientMappingProfile()
        {
            CreateMap<CreateClientViewModel, Client>()
                .ForMember(dest => dest.Claims, x => x.MapFrom(src => src.Claims.Select(c => new ClientClaim(c.Type, c.Value))));
            CreateMap<EditClientViewModel, Client>()
                .ForMember(dest => dest.Claims, x => x.MapFrom(src => src.Claims.Select(c => new ClientClaim(c.Type, c.Value))));
        }
    }
}
