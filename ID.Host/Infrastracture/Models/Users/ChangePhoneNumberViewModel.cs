using System.ComponentModel.DataAnnotations;

namespace ID.Host.Infrastracture.Models.Users
{
    public class ChangePhoneNumberViewModel
    {
        [Required(ErrorMessage = "Поле - UserId - обязательно к заполнению")]
        public string UserId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Поле - PhoneNumber - обязательно к заполнению")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
