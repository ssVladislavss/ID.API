using ID.Host.Infrastracture.Models.Claims;
using System.ComponentModel.DataAnnotations;

namespace ID.Host.Infrastracture.Models.Users
{
    public class EditUserViewModel
    {
        [Required(ErrorMessage = "Поле - UserId - обязательно к заполнению")]
        public string UserId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Поле - LastName - обязательно к заполнению")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Поле - FirstName - обязательно к заполнению")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Поле - SecondName - обязательно к заполнению")]
        public string SecondName { get; set; } = string.Empty;

        public List<string> RoleNames { get; set; } = new List<string>();
        public List<ClaimViewModel> Claims { get; set; } = new List<ClaimViewModel>();
    }
}
