using IdentityServer4.Models;

namespace ID.Core.Users
{
    public class ResetPasswordConfirmResult
    {
        public string NewPassword { get; } = string.Empty;
        public Client? Client { get; }

        public ResetPasswordConfirmResult(string newPassword, Client? client)
        {
            NewPassword = newPassword;
            Client = client;
        }
    }
}
