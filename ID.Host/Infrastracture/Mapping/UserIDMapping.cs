using AutoMapper;
using ID.Core.Users;
using ID.Host.Infrastracture.Mapping.Profiles;
using ID.Host.Infrastracture.Models.Users;

namespace ID.Host.Infrastracture.Mapping
{
    public static class UserIDMapping
    {
        internal static IMapper Mapper { get; }
        static UserIDMapping()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile(new UserMappingProfile())).CreateMapper();
        }

        public static CreateUserData ToModel(this CreateUserViewModel source)
        {
            return Mapper.Map<CreateUserData>(source);
        }

        public static UserID ToModel(this EditUserViewModel source)
        {
            return Mapper.Map<UserID>(source);
        }
    }
}
