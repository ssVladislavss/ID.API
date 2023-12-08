using ID.Host.Infrastracture.Models.Claims;
using System.ComponentModel.DataAnnotations;

namespace ID.Host.Infrastracture.Models.Roles
{
    public class EditRoleViewModel
    {
        [Required(ErrorMessage = "Поле - RoleId - обязательно к заполнению")]
        public string RoleId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Поле - RoleName - обязательно к заполнению")]
        public string RoleName { get; set; } = string.Empty;

        public List<ClaimViewModel> Claims { get; set; } = new List<ClaimViewModel>();
    }
}
