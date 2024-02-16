using System.ComponentModel.DataAnnotations;

namespace ID.Host.Infrastracture.Models.Users
{
    public class SetLockoutEnabledViewModel
    {
        [Required(ErrorMessage = "Поле - UserId - обязательно к заполнению")]
        public string UserId { get; set; } = string.Empty;
        public bool Enabled { get; set; }
        public TimeSpan? LockTime { get; set; }
    }
}
