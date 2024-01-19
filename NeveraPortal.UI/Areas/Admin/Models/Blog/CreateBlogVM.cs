using System.ComponentModel.DataAnnotations;

namespace NeveraPortal.UI.Areas.Admin.Models.AdminUser
{
    public class CreateBlogVM
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public string MainImgPath { get; set; } = string.Empty;

        public string Tags { get; set; } = string.Empty;
    }
}
