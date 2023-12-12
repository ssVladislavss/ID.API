using IdentityServer4.Models;

namespace ID.Host.App_Data.Notify.Email.Models
{
    public class UserChangedPasswordHtmlData
    {
        public string Email { get; }
        public string LockLink { get; }
        public Client Client { get; }

        public UserChangedPasswordHtmlData(string email, string lockLink, Client client)
        {
            Email = email;
            LockLink = lockLink;
            Client = client;
        }
    }
}
