using System.ComponentModel.DataAnnotations;

namespace NeveraPortal.UI.Areas.Admin.Models.Country
{
	public class IndexCountryVM
	{
        public int Id { get; set; }
		public string Name { get; set; }
		[DataType(DataType.DateTime)]
		public DateTime AddDate { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime UpdateDate { get; set; }
	}
}
