using IdentityServer4.Models;

namespace ID.Host.App_Data.Notify.Email.Models
{
    public class UserPasswordResetCompleteHtmlData
    {
        public string Email { get; }
        public string Password { get; }
        public Client? Client { get; }

        public UserPasswordResetCompleteHtmlData(string email, string password, Client? client = null)
        {
            Email = email;
            Password = password;
            Client = client;
        }
    }
}
