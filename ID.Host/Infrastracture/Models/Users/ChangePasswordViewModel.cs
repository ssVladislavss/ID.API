using System.ComponentModel.DataAnnotations;

namespace ID.Host.Infrastracture.Models.Users
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Поле - UserId - обязательно к заполнению")]
        public string UserId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Поле - CurrentPassword - обязательно к заполнению")]
        public string CurrentPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "Поле - NewPassword - обязательно к заполнению")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
