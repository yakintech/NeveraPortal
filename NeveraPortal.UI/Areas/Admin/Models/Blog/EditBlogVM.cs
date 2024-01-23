using System.ComponentModel.DataAnnotations;

namespace NeveraPortal.UI.Areas.Admin.Models.Blog
{
    public class EditBlogVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public IFormFile? MainImage { get; set; }

        public string? Tags { get; set; }
        public string MainImgPath { get; set; }
    }
}
