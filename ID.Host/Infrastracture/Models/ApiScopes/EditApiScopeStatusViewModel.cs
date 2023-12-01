using System.ComponentModel.DataAnnotations;

namespace ID.Host.Infrastracture.Models.ApiScopes
{
    public class EditApiScopeStatusViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Некорректное занчение поля - Id")]
        public int Id { get; set; }
        public bool Status { get; set; }
    }
}
