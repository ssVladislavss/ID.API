using ISDS.ServiceExtender.Http;

namespace ID.Core.Users.Abstractions
{
    public interface IVerificationService
    {
        Task SendCodeOnEmailAsync(string userId, ISrvUser iniciator, CancellationToken token = default);
        Task SendCodeOnSmsAsync(string userId, ISrvUser iniciator, CancellationToken token = default);
        Task VerifyCodeAsync(string userId, string currentCode, CancellationToken token = default);
    }
}
