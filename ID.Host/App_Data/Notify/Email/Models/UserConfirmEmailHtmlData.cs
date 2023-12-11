using IdentityServer4.Models;

namespace ID.Host.App_Data.Notify.Email.Models
{
    public class UserConfirmEmailHtmlData
    {
        public Client Client { get; }
        public string ConfirmLink { get; }

        public UserConfirmEmailHtmlData(string confirmLink, Client client)
        {
            this.ConfirmLink = confirmLink;
            this.Client = client;
        }
    }
}
