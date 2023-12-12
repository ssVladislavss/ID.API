using System.ComponentModel.DataAnnotations;

namespace ID.Host.Infrastracture.Models.Users
{
    public class VerifyUserCodeViewModel
    {
        [Required(ErrorMessage = "Поле - UserId - обязательно к заполнению")]
        public string UserId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Поле - UserId - обязательно к заполнению")]
        public string Code { get; set; } = string.Empty;
    }
}
