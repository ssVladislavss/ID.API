using AutoMapper;
using ID.Core.Users;
using ID.Host.Infrastracture.Models.Users;

namespace ID.Host.Infrastracture.Mapping.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserViewModel, CreateUserData>()
                .ForMember(dest => dest.User, x => x.Ignore())
                .ForMember(dest => dest.RoleNames, x => x.Ignore())
                .ForMember(dest => dest.Password, x => x.Ignore())
                .ConstructUsing((data, context) =>
                {
                    UserID createUser = new()
                    {
                        LastName = data.LastName,
                        FirstName = data.FirstName,
                        Email = data.Email,
                        EmailConfirmed = true,
                        SecondName = data.SecondName,
                        UserName = data.Email
                    };

                    return new CreateUserData(createUser, data.RoleNames, data.Password);
                });

            CreateMap<EditUserViewModel, UserID>();
        }
    }
}
