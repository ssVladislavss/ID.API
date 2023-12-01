using AutoMapper;
using ID.Core.ApiResources;
using ID.Host.Infrastracture.Models.ApiResources;

namespace ID.Host.Infrastracture.Mapping.Profiles
{
    public class ApiResourceMappingProfile : Profile
    {
        public ApiResourceMappingProfile()
        {
            CreateMap<CreateApiResourceViewModel, IDApiResource>();
            CreateMap<EditApiResourceViewModel, IDApiResource>();
        }
    }
}
