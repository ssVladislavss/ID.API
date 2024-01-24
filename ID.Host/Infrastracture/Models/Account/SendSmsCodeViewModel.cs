using ServiceExtender.Sms.Models;
using System.ComponentModel.DataAnnotations;

namespace ID.Host.Infrastracture.Models.Account
{
    public class SendSmsCodeViewModel
    {
        [Required(ErrorMessage = "Поле - UserId - обязательно к заполнению")]
        public string UserId { get; set; } = string.Empty;
        public string? Sender { get; set; }
        public bool IsTranslit { get; set; }
        [Required(ErrorMessage = "Поле - Login - обязательно к заполнению")]
        public string Login { get; set; } = string.Empty;
        [Required(ErrorMessage = "Поле - Password - обязательно к заполнению")]
        public string Password { get; set; } = string.Empty;
        [EnumDataType(typeof(SmsProviderType), ErrorMessage = "Некорректное значение поля - ProviderType")]
        public SmsProviderType ProviderType { get; set; }
    }
}
