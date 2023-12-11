using IdentityServer4.Models;

namespace ID.Host.App_Data.Notify.Email.Models
{
    public class CreatedUserHtmlData
    {
        public string Password { get; }
        public string ConfirmLink { get; }

        public Client Client { get; }

        public CreatedUserHtmlData(string password, string confirmLink, Client client)
        {
            Password = password;
            Client = client;
            ConfirmLink = confirmLink;
        }
    }
}
