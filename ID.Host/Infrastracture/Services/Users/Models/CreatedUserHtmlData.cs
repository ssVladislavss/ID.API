using IdentityServer4.Models;

namespace ID.Host.Infrastracture.Services.Users.Models
{
    public class CreatedUserHtmlData
    {
        public string Email { get; }
        public string Password { get; }

        public Client Client { get; }

        public CreatedUserHtmlData(string email, string password, Client client)
        {
            this.Email = email;
            this.Password = password;
            this.Client = client;
        }
    }
}
