using System.ComponentModel.DataAnnotations;

namespace NeveraPortal.UI.Areas.Admin.Models.AdminUser
{
    public class CreateBlogVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        [Required]
        public IFormFile MainImage { get; set; }

        public string? Tags { get; set; } = string.Empty;
    }
}
