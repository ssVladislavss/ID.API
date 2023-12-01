using Microsoft.AspNetCore.Identity;

namespace ID.Host.Infrastracture.Models.Users
{
    public class UserRoleViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public UserRoleViewModel(IdentityRole role)
        {
            RoleId = role.Id;
            RoleName = role.Name;
        }
    }
}
