using System.ComponentModel.DataAnnotations;

namespace NeveraPortal.UI.Areas.Admin.Models.Category
{
    public class IndexCategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime AddDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdateDate { get; set; }
    }
}
