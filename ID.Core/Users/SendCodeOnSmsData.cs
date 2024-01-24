using ServiceExtender.Sms.Models;

namespace ID.Core.Users
{
    public class SendCodeOnSmsData
    {
        public string? Sender { get; set; }
        public bool IsTranslit { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public SmsProviderType ProviderType { get; set; }

        public SendCodeOnSmsData(string login, string password, bool isTranslit, SmsProviderType providerType, string? sender = null)
        {
            Sender = sender;
            Login = login;
            Password = password;
            ProviderType = providerType;
            IsTranslit = isTranslit;
        }
    }
}
