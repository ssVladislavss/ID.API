using System.ComponentModel.DataAnnotations;

namespace ID.Host.Infrastracture.Models.Users
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Поле - Email - обязательно к заполнению")]
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; }
        public string? ClientId { get; set; }

        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }

        public List<string> RoleNames { get; set; } = new List<string>();
    }
}
