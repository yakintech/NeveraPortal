using System.ComponentModel.DataAnnotations;

namespace NeveraPortal.UI.Areas.Admin.Models.Country
{
	public class EditCountryVM
	{

		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
	}
}
