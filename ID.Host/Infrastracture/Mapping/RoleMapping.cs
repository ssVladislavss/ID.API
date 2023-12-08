using AutoMapper;
using ID.Core.Roles;
using ID.Host.Infrastracture.Mapping.Profiles;
using ID.Host.Infrastracture.Models.Roles;
using Microsoft.AspNetCore.Identity;

namespace ID.Host.Infrastracture.Mapping
{
    public static class RoleMapping
    {
        internal static IMapper Mapper { get; }
        static RoleMapping()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile(new RoleMappingProfile())).CreateMapper();
        }

        public static IdentityRole ToModel(this CreateRoleViewModel source)
        {
            return Mapper.Map<IdentityRole>(source);
        }

        public static EditingRoleData ToModel(this EditRoleViewModel source)
        {
            return Mapper.Map<EditingRoleData>(source);
        }
    }
}
