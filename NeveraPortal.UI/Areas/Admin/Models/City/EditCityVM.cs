using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace NeveraPortal.UI.Areas.Admin.Models.City
{
    public class EditCityVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please select a country")]
        public int CountryId { get; set; }

        public List<SelectListItem> Countries { get; set; }

        public EditCityVM()
        {
            Countries = new List<SelectListItem>();
        }
    }
}
