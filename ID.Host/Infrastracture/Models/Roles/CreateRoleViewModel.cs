using System.ComponentModel.DataAnnotations;

namespace ID.Host.Infrastracture.Models.Roles
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Поле - RoleName - обязательно к заполнению")]
        public string RoleName { get; set; } = string.Empty;
    }
}
