using IdentityServer4.Models;

namespace ID.Host.App_Data.Notify.Email.Models
{
    public class UserConfirmResetPasswordHtmlData
    {
        public string Email { get; }
        public string ConfirmLink { get; }
        public Client Client { get; }

        public UserConfirmResetPasswordHtmlData(string email, string confirmlink, Client client)
        {
            Email = email;
            ConfirmLink = confirmlink;
            Client = client;
        }
    }
}
