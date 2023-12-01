using IdentityServer4.Models;
using System.ComponentModel.DataAnnotations;

namespace ID.Host.Infrastracture.Models.ApiResources
{
    public class CreateApiResourceViewModel
    {
        [Required(ErrorMessage = "Поле - Name - обязательно к заполнению")]
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public List<string> UserClaims { get; set; } = new List<string>();
        public List<Secret> ApiSecrets { get; set; } = new List<Secret>();
        public List<string> Scopes { get; set; } = new List<string>();
    }
}
