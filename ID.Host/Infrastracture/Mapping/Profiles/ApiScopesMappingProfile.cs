using AutoMapper;
using ID.Core.ApiScopes;
using ID.Host.Infrastracture.Models.ApiScopes;

namespace ID.Host.Infrastracture.Mapping.Profiles
{
    public class ApiScopesMappingProfile : Profile
    {
        public ApiScopesMappingProfile()
        {
            CreateMap<CreateApiScopeViewModel, IDApiScope>();
            CreateMap<EditApiScopeViewModel, IDApiScope>();
        }
    }
}
