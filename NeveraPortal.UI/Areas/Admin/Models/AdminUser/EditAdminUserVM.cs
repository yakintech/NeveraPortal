using System.ComponentModel.DataAnnotations;

namespace NeveraPortal.UI.Areas.Admin.Models.AdminUser
{
    public class EditAdminUserVM
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Enter an email address in valid format")]
        public string EMail { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "The password must be at least {2} characters.", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required, Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
