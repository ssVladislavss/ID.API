using AutoMapper;
using ID.Core.Users;
using ID.Host.Infrastracture.Models.Users;
using System.Security.Claims;

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
                .ForMember(dest => dest.ClientId, x => x.Ignore())
                .ConstructUsing((data, context) =>
                {
                    UserID createUser = new()
                    {
                        LastName = data.LastName,
                        FirstName = data.FirstName,
                        Email = data.Email,
                        EmailConfirmed = false,
                        SecondName = data.SecondName,
                        UserName = data.Email
                    };

                    return new CreateUserData(createUser, data.RoleNames, data.Password, data.ClientId);
                });

            CreateMap<EditUserViewModel, EditUserData>()
                .ForMember(dest => dest.User, x => x.Ignore())
                .ForMember(dest => dest.Claims, x => x.Ignore())
                .ConstructUsing((data, context) =>
                {
                    UserID updateUser = new()
                    {
                        LastName = data.LastName,
                        FirstName = data.FirstName,
                        SecondName = data.SecondName,
                        Id = data.UserId
                    };

                    List<Claim> userClaims = new();
                    List<string> roleNames = new();

                    foreach (var claim in data.Claims)
                        userClaims.Add(new Claim(claim.Type, claim.Value));

                    roleNames.AddRange(data.RoleNames);

                    return new EditUserData(updateUser, userClaims, roleNames);
                });
        }
    }
}
