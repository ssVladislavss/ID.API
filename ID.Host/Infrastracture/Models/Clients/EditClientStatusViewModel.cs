using System.ComponentModel.DataAnnotations;

namespace ID.Host.Infrastracture.Models.Clients
{
    public class EditClientStatusViewModel
    {
        [Required(ErrorMessage = "Поле - ClientId - обязательно к заполнению")]
        public string ClientId { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
