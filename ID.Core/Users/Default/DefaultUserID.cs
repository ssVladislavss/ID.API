using Microsoft.AspNetCore.Identity;

namespace ID.Core.Users.Default
{
    public class DefaultUserID
    {
        public const string DefaultPassword = "12Qwaszx$";
        public static UserID RootAdmin
            => new()
            {
                Id = "63ebf29e-9150-48ba-a126-5be6eb12ae78",
                Email = "ura.so.v.ru@mail.ru",
                EmailConfirmed = true,
                PhoneNumber = "+79251066154",
                PhoneNumberConfirmed = true,
                LastName = "Урасов",
                FirstName = "Владислав",
                SecondName = "Олегович",
                UserName = "ura.so.v.ru@mail.ru"
            };
        public static IdentityRole RootAdminRole
            => new()
            {
                Id = "a0c64f39-d0f7-461b-9c05-c52bcfb3cb3a",
                Name = IDConstants.Roles.RootAdmin,
                NormalizedName = IDConstants.Roles.RootAdmin.ToUpper(),
            };
    }
}
