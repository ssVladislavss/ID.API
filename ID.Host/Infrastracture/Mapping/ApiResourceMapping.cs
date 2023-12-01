using AutoMapper;
using ID.Core.ApiResources;
using ID.Host.Infrastracture.Mapping.Profiles;
using ID.Host.Infrastracture.Models.ApiResources;

namespace ID.Host.Infrastracture.Mapping
{
    public static class ApiResourceMapping
    {
        internal static IMapper Mapper { get; }
        static ApiResourceMapping()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile(new ApiResourceMappingProfile())).CreateMapper();
        }

        public static IDApiResource ToModel(this CreateApiResourceViewModel source)
        {
            return Mapper.Map<IDApiResource>(source);
        }

        public static IDApiResource ToModel(this EditApiResourceViewModel source)
        {
            return Mapper.Map<IDApiResource>(source);
        }
    }
}
