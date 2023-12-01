using AutoMapper;
using ID.Core.ApiScopes;
using ID.Host.Infrastracture.Mapping.Profiles;
using ID.Host.Infrastracture.Models.ApiScopes;

namespace ID.Host.Infrastracture.Mapping
{
    public static class ApiScopesMapping
    {
        internal static IMapper Mapper { get; }
        static ApiScopesMapping()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile(new ApiScopesMappingProfile())).CreateMapper();
        }

        public static IDApiScope ToModel(this CreateApiScopeViewModel source)
        {
            return Mapper.Map<IDApiScope>(source);
        }

        public static IDApiScope ToModel(this EditApiScopeViewModel source)
        {
            return Mapper.Map<IDApiScope>(source);
        }
    }
}
