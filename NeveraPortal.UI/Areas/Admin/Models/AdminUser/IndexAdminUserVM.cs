using System.ComponentModel.DataAnnotations;

namespace NeveraPortal.UI.Areas.Admin.Models.AdminUser
{
    public class IndexAdminUserVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "EMail")]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime AddDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdateDate { get; set; }

    }
}
