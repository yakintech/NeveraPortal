using System.ComponentModel.DataAnnotations;

namespace NeveraPortal.UI.Areas.Admin.Models.AdminUser
{
    public class CreateAdminUserVM
    {
        [Required]
        public string EMail { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required, Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
