using IdentityServer4.Models;

namespace ID.Host.App_Data.Notify.Email.Models
{
    public class UserVerificationCodeHtmlData
    {
        public string Email { get; }
        public string VerificationCode { get; }
        public Client? Client { get; }

        public UserVerificationCodeHtmlData(string email, string verificationCode, Client? client = null)
        {
            Email = email;
            VerificationCode = verificationCode;
            Client = client;
        }
    }
}
