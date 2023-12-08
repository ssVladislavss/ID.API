using AutoMapper;
using ID.Core.Roles;
using ID.Host.Infrastracture.Models.Roles;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ID.Host.Infrastracture.Mapping.Profiles
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<CreateRoleViewModel, IdentityRole>()
                .ForMember(dest => dest.Id, x => x.Ignore())
                .ForMember(dest => dest.Name, x => x.Ignore())
                .ForMember(dest => dest.NormalizedName, x => x.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, x => x.Ignore())
                .ConstructUsing((data, context) =>
                {
                    var creatingRole = new IdentityRole(data.RoleName);

                    return creatingRole;
                });

            CreateMap<EditRoleViewModel, EditingRoleData>()
                .ForMember(dest => dest.Role, x => x.Ignore())
                .ForMember(dest => dest.Claims, x => x.Ignore())
                .ConstructUsing((data, context) =>
                {
                    var updatingRole = new IdentityRole(data.RoleName)
                    {
                        Id = data.RoleId
                    };

                    var updatingClaims = new List<Claim>();

                    foreach (var claim in data.Claims)
                        updatingClaims.Add(new Claim(claim.Type, claim.Value));

                    return new EditingRoleData(updatingRole, updatingClaims);
                });
        }
    }
}
