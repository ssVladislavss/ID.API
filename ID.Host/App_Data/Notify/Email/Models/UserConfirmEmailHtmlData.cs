using IdentityServer4.Models;

namespace ID.Host.App_Data.Notify.Email.Models
{
    public class UserConfirmEmailHtmlData
    {
        public string Email { get; }
        public Client Client { get; }
        public string ConfirmLink { get; }

        public UserConfirmEmailHtmlData(string email, string confirmLink, Client client)
        {
            this.Email = email;
            this.ConfirmLink = confirmLink;
            this.Client = client;
        }
    }
}
