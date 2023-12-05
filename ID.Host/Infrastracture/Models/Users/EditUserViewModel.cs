using System.ComponentModel.DataAnnotations;

namespace ID.Host.Infrastracture.Models.Users
{
    public class EditUserViewModel
    {
        [Required(ErrorMessage = "Поле - UserId - обязательно к заполнению")]
        public string UserId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Поле - UserId - обязательно к заполнению")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Поле - UserId - обязательно к заполнению")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Поле - UserId - обязательно к заполнению")]
        public string SecondName { get; set; } = string.Empty;
    }
}
