using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace NeveraPortal.UI.Areas.Admin.Models.City
{
	public class CreateCityVM
	{
		[Required]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please select a country")]
		public int CountryId { get; set; }

		public List<SelectListItem> Countries { get; set; }

		public CreateCityVM()
		{
			Countries = new List<SelectListItem>();
		}
	}
}
