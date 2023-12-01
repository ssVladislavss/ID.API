using System.ComponentModel.DataAnnotations;

namespace ID.Host.Infrastracture.Models.ApiScopes
{
    public class CreateApiScopeViewModel
    {
        [Required(ErrorMessage = "Поле - Name - обязательно к заполнению")]
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public List<string> UserClaims { get; set; } = new List<string>();
        public bool Required { get; set; } = false;
        public bool Emphasize { get; set; } = false;
    }
}
