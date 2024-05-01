using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.ViewModels
{
    public class StaffRegisterViewModel
    {
        [Required, DataType(DataType.Text), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, DataType(DataType.Text), Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required, DataType(DataType.Text), Display(Name = "School")]
        public string School { get; set; }

        [Required, DataType(DataType.Text), Display(Name = "Department")]
        public string Department { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
