using System.ComponentModel.DataAnnotations;

namespace NeveraPortal.UI.Areas.Admin.Models.Country
{
    public class CreateCountryVM
    {
        [Required]
        public string Name { get; set; }
    }
}
