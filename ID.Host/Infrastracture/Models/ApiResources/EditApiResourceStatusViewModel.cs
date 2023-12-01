using System.ComponentModel.DataAnnotations;

namespace ID.Host.Infrastracture.Models.ApiResources
{
    public class EditApiResourceStatusViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Некорректное значение поля - Id")]
        public int Id { get; set; }
        public bool Status { get; set; }
    }
}
