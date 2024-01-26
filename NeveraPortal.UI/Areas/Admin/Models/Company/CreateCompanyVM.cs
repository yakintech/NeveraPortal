using Microsoft.AspNetCore.Mvc.Rendering;

namespace NeveraPortal.UI.Areas.Admin.Models.Company
{
    public class CreateCompanyVM
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public int? CountryId { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public CreateCompanyVM()
        {
            Countries = new List<SelectListItem>();
        }
    }
}
