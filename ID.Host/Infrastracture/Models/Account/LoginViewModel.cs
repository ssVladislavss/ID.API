using System.ComponentModel.DataAnnotations;

namespace ID.Host.Infrastracture.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле - UserName - обязательно к заполнению")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Поле - Password - обязательно к заполнению")]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
        [Required(ErrorMessage = "Поле - ReturnUrl - обязательно к заполнению")]
        public string ReturnUrl { get; set; } = string.Empty;
    }
}
