using System.ComponentModel.DataAnnotations;

namespace RopeyDVDSystem.Models
{
    public class ChangePassword
    {
        [Display(Name = "Old Password")]

        [Required (ErrorMessage = "Old Password is required")]
        public string OldPassword { get; set; }


        [Required (ErrorMessage = "New Password is required")]

        [Display(Name = "New Password")]
        public string NewPassword { get; set; }


        [Required (ErrorMessage = "Confirm Password is required")]

        [Compare (otherProperty: "NewPassword", ErrorMessage = "Passwords do not match")]

        [Display(Name = "Confirm New Password")]
        public string ConfirmPassword { get; set; }

    }
}
